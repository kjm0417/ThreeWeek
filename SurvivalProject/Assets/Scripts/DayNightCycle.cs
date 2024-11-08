using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)] 
    public float time;//0.5일 때 12시 각도 90
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon; // Vector 90 0 0

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]

    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;
    void Start()
    {
        timeRate = 1.0f/ fullDayLength;
        time = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        time = (time +timeRate * Time.deltaTime)%1.0f;

        UpdateLighting(sun, sunColor, sunIntensity); 
        UpdateLighting(moon, moonColor, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient gradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time); //보간되는 값 받아옴

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;//정오시간 계산 연산 
        lightSource.color = gradient.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if(lightSource.intensity ==0&& go.activeInHierarchy)
        {
            go.SetActive(false);    
        }
        else if(lightSource.intensity > 0 && !go.activeInHierarchy) 
        {
            go.SetActive(true);
        }
    }
}
