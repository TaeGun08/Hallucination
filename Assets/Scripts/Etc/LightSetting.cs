using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LightSetting : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private Color color;

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

            if (PlayerPrefs.GetInt("SaveScene") == 1 && SceneManager.GetActiveScene().name == "MapScene")
            {
                color.r = 0.5f;
                color.g = 0.2f;
                color.b = 0.2f;
                colorAdjustments.colorFilter.value = color;
            }
            else
            {
                color.r = 1f;
                color.g = 1f;
                color.b = 1f;
                colorAdjustments.colorFilter.value = color;
            }
        }
    }
}
