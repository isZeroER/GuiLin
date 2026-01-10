using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NanDuPanel : MonoBehaviour
{
    public Button easyBtn;
    public Button normalBtn;
    public Button hardBtn;

    private void Awake()
    {
        easyBtn.onClick.AddListener(() => SetNanDu(0));
        normalBtn.onClick.AddListener(() => SetNanDu(1));
        hardBtn.onClick.AddListener(() => SetNanDu(2));
    }
  
    private void SetNanDu(int nandu)
    {
        GameCtrl.nanDu = nandu;
        StartCoroutine(LoadMainSceneAsync());
    }

    public GameObject loadingPanel;
    public Slider loadingSlider;   // 进度条
    public Text loadingText;       // 可选：百分比文字

    IEnumerator LoadMainSceneAsync()
    {
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        // 开始异步加载
        AsyncOperation op = SceneManager.LoadSceneAsync("Main");

        // 先不自动切换场景
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            // progress 最大只到 0.9
            float progress = Mathf.Clamp01(op.progress / 0.9f);

            // 更新 UI
            if (loadingSlider != null)
                loadingSlider.value = progress;

            if (loadingText != null)
                loadingText.text = $"{(int)(progress * 100)}%";

            // 加载完成，允许切换
            if (progress >= 1f)
            {
                yield return new WaitForSeconds(0.3f); // 给一点缓冲更顺
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
