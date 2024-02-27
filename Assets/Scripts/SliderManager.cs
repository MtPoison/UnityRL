using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public List<Slider> sliders = new List<Slider>();

    public Slider staminaSlider;

    private void Start()
    {
        SliderInitialization();
    }

    private void SliderInitialization()
    {
        sliders.AddRange(FindObjectsOfType<Slider>());
    }

    private IEnumerator SmoothChangeSliderValue(Slider slider, float targetValue)
    {
        float startValue = slider.value;
        float sleepTime = 0.0f;

        while (sleepTime < targetValue)
        {
            sleepTime += Time.deltaTime;
            float t = Mathf.Clamp01(sleepTime / targetValue);
            slider.value = Mathf.Lerp(startValue, targetValue, t);
            yield return null;
        }

        slider.value = targetValue;
    }

    public void SetSlidersValue(string tag, float targetValue)
    {
        foreach (Slider slider in sliders)
        {
            if (slider.CompareTag(tag))
            {
                StartCoroutine(SmoothChangeSliderValue(slider, targetValue));
            }
        }
    }

    public float GetSlidersValue(string tag)
    {
        foreach (Slider slider in sliders)
        {
            if (slider.CompareTag(tag))
            {
                Debug.Log(slider.value);
                return slider.value;
            }
        }
        return 0;
    }

}
