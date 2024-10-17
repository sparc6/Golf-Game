using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] Slider timeSlider;
    [SerializeField] Text speedText;

    // Update is called once per frame
    void Start()
    {
        Time.timeScale = 1;
    }

    public void TimeSlider()
    {
        Time.timeScale = timeSlider.value;
        speedText.text = "x"+timeSlider.value.ToString("F0");
    }
}
