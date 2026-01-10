using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 非组件单例
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : new()
{
    private static T m_instance;
    //锁，用于保证线程安全
    private static object mutex = new object();
    static bool isApplicationQuitting = false;

    private static readonly HashSet<Type> allSingletonTypes = new HashSet<Type>();
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetAllSingletons()
    {
        lock (mutex)
        {
            m_instance = default;
            isApplicationQuitting = false;
        }
    } 
    

    public static T Instance
    {
        get
        {
            if (isApplicationQuitting)
            {
                return default;
            }
            
            if (m_instance == null)
            {
                lock (mutex)
                {
                    //防止 等待线程 等待一段时间后，之前正在进行的线程 已经创建好了新的单例
                    if (m_instance == null)
                    {
                        try
                        {
                            m_instance = new T();
                            allSingletonTypes.Add(typeof(T));
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"[Singleton<{typeof(T).Name}>] 创建实例失败: {e}");
                            throw;
                        }
                    }
                }
            }

            return m_instance;
        }
    }

    static void Dispose()
    {
        lock (mutex)
        {
            if (m_instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
            m_instance = default;
        }
    }

    static void Reset()
    {
        lock (mutex)
        {
            m_instance = default;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void OnApplicationQuit()
    {
        isApplicationQuitting = true;
        CleanupAllSingletons();
    }
    private static void CleanupAllSingletons()
    {
        lock (mutex)
        {
            foreach (var type in allSingletonTypes)
            {
                Debug.Log($"[Singleton] 清理 {type.Name}");
                
                // 这里可以通过反射调用每个单例的Dispose方法
                // 但需要类型有公共的静态Dispose方法
            }
            
            allSingletonTypes.Clear();
        }
    }
}

/// <summary>
/// 脚本组件单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class UnitySingleton<T> : MonoBehaviour where T : UnitySingleton<T>
{
    private static T m_Instance;

    public static T Instance => m_Instance;
    private static bool applicationIsQuitting = false;

    //确保外界不能创建
    protected UnitySingleton(){}    
    protected virtual void Awake()
    {
        if (m_Instance == null)
            m_Instance = (T)this;
        else 
            Destroy(gameObject);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
        m_Instance = null;
        applicationIsQuitting = false;
    }
    
    private void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }

    private void OnDestroy() 
    {
        if (m_Instance == this)
            m_Instance = null;
    }
}
