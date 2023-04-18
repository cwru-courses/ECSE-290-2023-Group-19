using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Image pause;

    
    //public Image pause;
    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pause.gameObject.SetActive(false);
    }

    public void Continue()
    {
        Time.timeScale = 1;
        pause.gameObject.SetActive(true);
    }
}
