using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{

    public AudioManager audioManager;
    public float volume;
    public void Initialize(AudioManager audioManager, float volume)
    {
        this.audioManager = audioManager;
        this.SetVolume(volume);
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
    }


    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


}
 