using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public AudioManager audioManager;
    public float volume;
    public Slider volumeSlider;

    public void Initialize(AudioManager audioManager, Slider volumeSlider)
    {

        this.audioManager = audioManager;
        this.volumeSlider = volumeSlider;
        this.SetVolume(volume);
        this.volumeSlider.value = volume;
    }

        public void PlayGame()
    {
        SceneManager.LoadScene("Tetris");
    }

    public void PlayOriginal()
    {
        SceneManager.LoadScene("TetrisOriginal");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void SetVolume (float volume)
    {
        Debug.Log(volume);
        audioManager.setVolume(volume);
        this.volume = volume;
        volume = this.volumeSlider.value;

    }




    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int quality)
    {

        QualitySettings.SetQualityLevel(quality);

    }


}
 