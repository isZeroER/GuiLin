using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MicPhonePanel : MonoBehaviour
{
    public string theAnswer;
    public InputField answerText;
    public Button answerButton;
    public MicPhone micPhone;
    public Text wrongTip;

    private void Awake()
    {
        answerButton.onClick.AddListener(CheckAnswer);
    }

    private void CheckAnswer()
    {
        if (answerText.text != theAnswer)
        {
            answerText.text = "";
            StartCoroutine(CoWrongTip());
        }
        else
        {
            micPhone.AnswerRight();
            gameObject.SetActive(false);
        }
    }

    IEnumerator CoWrongTip()
    {
        wrongTip.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        wrongTip.gameObject.SetActive(false);
    }
}