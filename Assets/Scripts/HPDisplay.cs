using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPDisplay : MonoBehaviour
{
    public TextMeshProUGUI hpTextDisplay;
    private void Start()
    {
        hpTextDisplay.text = "Remaining HP = " + Player.HP.ToString();
    }

    //private void OnEnable()
    //{
    //    Player.OnHPChange += UpdateHP;
    //}

    //private void OnDisable()
    //{
    //    Player.OnHPChange -= UpdateHP;
    //}

    private void Update()
    {
        hpTextDisplay.text = "Remaining HP = " + Player.HP.ToString();
    }
}
