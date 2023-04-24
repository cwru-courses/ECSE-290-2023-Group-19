using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public Image ins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ShowInstruction()
    {
        ins.gameObject.SetActive(true);
    }

    public void CloseInstruction()
    {
        ins.gameObject.SetActive(false);
    }
}
