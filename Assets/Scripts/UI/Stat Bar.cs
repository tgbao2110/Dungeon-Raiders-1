using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class StatBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI number;


    public void SetMaxStat(int stat)
    {
        slider.maxValue = stat;
        slider.value = stat;
        number.text = stat.ToString();
    }

    public void SetStat(int stat)
    {
        slider.value = stat;
        number.text = stat.ToString();
    }
}
