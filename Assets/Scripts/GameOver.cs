using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{ 

    public TextMeshProUGUI current;
    public TextMeshProUGUI high;



    // public void Awake()
    // {
    //     this.current.text = "Lines Cleared: ";
    //     this.high.text = "High Score: ";
    // }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Tetris");
    }

    public void PlayOriginal()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TetrisOriginal");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}
