using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public AudioSource[] audioSources;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BeInteracted();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            NotBeInteracted();
        }
    }

    private void NotBeInteracted()
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

    protected virtual void BeInteracted()
    {
        foreach (var audioSource in audioSources)
        {
            audioSource.Play();
        }
    }
}
