using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uibar;
    void Start()
    {
        curValue = startValue;
    }

    void Update()
    {
        //ui업데이트
        uibar.fillAmount = GetPercentage();
    }

    float GetPercentage() //체력을 ui바 계산에 맞춰서 fill
    {
        return curValue / maxValue;
    }

    public void Add(float value) //증가
    {
        curValue = Mathf.Min(curValue+value, maxValue); //둘중에 작은 값을 계속 받아온다
    }

    public void Subtract(float value)//감소
    {
        curValue = Mathf.Max(curValue - value, 0);
    }

}
