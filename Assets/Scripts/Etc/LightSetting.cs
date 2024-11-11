using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightSetting : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;

    private void Awake()
    {
        volume = GetComponent<Volume>();
    }

    private void Update()
    {
        if (volume.profile.TryGet(out colorAdjustments))
        {
            float sliderValue = GameManager.Instance.Option.GetSlidersValue(3);
            colorAdjustments.postExposure.value = (sliderValue * 4) - 2;

            if (PlayerPrefs.GetInt("SaveScene") == 1)
            {
                colorAdjustments.colorFilter.value = Color.red;
            }
            else
            {
                colorAdjustments.colorFilter.value = Color.white;
            }
        }
    }
}
