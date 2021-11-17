using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI displaySliderValue;

    private float sliderValue;

    private void Update()
    {
        sliderValue = AudioManager.volume;
        slider.value = sliderValue;
        displaySliderValue.text = ((int)((sliderValue) * 100)).ToString() + "%";

    }
}