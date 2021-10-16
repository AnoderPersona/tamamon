using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barras : MonoBehaviour
{
    public Slider slider;
    public void setmax(int max){
        slider.maxValue = max;
        slider.value = max;
    }
    public void setbar (float cantidad){
        slider.value = cantidad; 
    }

    
}
