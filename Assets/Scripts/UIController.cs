using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Image play;
    //public Image pause;
    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        play.gameObject.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        play.gameObject.SetActive(false);
    }
}
