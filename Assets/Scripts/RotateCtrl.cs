using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RotateCtrl : MonoBehaviour
{
    private float moveSpeed;

    private void Awake()
    {
        moveSpeed = Random.Range(40, 60);
    }

    private void Update()
    {
        transform.Rotate(0, moveSpeed * Time.deltaTime, 0);
    }
}
