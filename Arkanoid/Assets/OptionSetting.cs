using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSetting : MonoBehaviour
{
    public Slider paddleSlider;
    public Slider ballSlider;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("paddleSpeed", 15f);
        PlayerPrefs.SetFloat("ballSpeed", 15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetPaddleSpeed()
    {
        PlayerPrefs.SetFloat("paddleSpeed", paddleSlider.value);
    }

    public void SetBallSpeed()
    {
        PlayerPrefs.SetFloat("ballSpeed", ballSlider.value);
    }
}
