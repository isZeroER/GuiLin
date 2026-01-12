using System.Collections;
using UnityEngine;

public class MicPhone : IInteract
{
    public AudioSource audio;
    public GameObject hehuaDeng;
    public Light light;
    public override void Interact()
    {
        audio.Play();
        UIManager.Instance.OpenUI("micPhone");
    }

    public void AnswerRight()
    {
        light.intensity = 1;
        StartCoroutine(CoShowOut());
    }

    IEnumerator CoShowOut()
    {
        float duration = 1.5f;
        float moveSpeed = 1.5f;
        float endTime = Time.time + duration;
        while (Time.time < endTime)
        {
            hehuaDeng.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }
}