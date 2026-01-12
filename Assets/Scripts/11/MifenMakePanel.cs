using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MifenMakePanel : MonoBehaviour
{
    public Button suandoujiaoBtn;

    private void Awake()
    {
        suandoujiaoBtn.onClick.AddListener(SuanDouJiao);
    }

    private void SuanDouJiao()
    {
        
    }
}
