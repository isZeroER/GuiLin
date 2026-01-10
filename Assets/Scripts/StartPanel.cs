using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartPanel : MonoBehaviour
{
    [Header("界面")] 
    public VideoPlayer vp;
    // public GameObject nanDuPanel;
    public GameObject videoPlayerPanel;
    
    [Header("按钮")]
    public Button startGameBtn;
    public Button settingBtn;
    public Button returnBtn;
    public Button exitBtn;

    public Slider volumeSlider;
    private void Awake()
    {
        startGameBtn.onClick.AddListener(StartGame); // 开始游戏之后，选择关卡
        settingBtn.onClick.AddListener(() => SetSetting(true));
        returnBtn.onClick.AddListener(() => SetSetting(false));
        exitBtn.onClick.AddListener(ExitGame);
        volumeSlider?.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        GameCtrl.volume = volume;
    }
    
    private void SetSetting(bool flag)
    {
        videoPlayerPanel.SetActive(flag);
        if (flag)
        {
            vp.Play();
        }
        else
        {
            vp.Stop();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
