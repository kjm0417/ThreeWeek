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
        //ui������Ʈ
        uibar.fillAmount = GetPercentage();
    }

    float GetPercentage() //ü���� ui�� ��꿡 ���缭 fill
    {
        return curValue / maxValue;
    }

    public void Add(float value) //����
    {
        curValue = Mathf.Min(curValue+value, maxValue); //���߿� ���� ���� ��� �޾ƿ´�
    }

    public void Subtract(float value)//����
    {
        curValue = Mathf.Max(curValue - value, 0);
    }

}
