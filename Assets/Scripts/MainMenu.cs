using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
        Application.Quit();
    }
}
 