using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public Button loginBtn;
    public InputField username;
    public InputField password;
    public GameObject loginPanel;
    public GameObject startPanel;
    public GameObject wrongTip;
    [Header("设置账号密码")]
    public string usernameString = "admin";
    public string passwordString = "123";

    private void Awake()
    {
        loginBtn.onClick.AddListener(CheckLogin);
        loginPanel.SetActive(false);
    }

    private void CheckLogin()
    {
        if (username.text == usernameString)
        {
            if (password.text == passwordString)
            {
                startPanel.SetActive(true);
                loginPanel.SetActive(false);
                return;
            }
        }

        StartCoroutine(CoWrong());
    }

    IEnumerator CoWrong()
    {
        wrongTip.SetActive(true);
        yield return new WaitForSeconds(2f);
        wrongTip.SetActive(false);
    }
}
