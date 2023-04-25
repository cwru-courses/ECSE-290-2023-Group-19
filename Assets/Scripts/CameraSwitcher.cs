using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;
    public KeyCode switchKey = KeyCode.Tab;
    private GameObject shopMenu;
    private GameObject player;

    private int currentCameraIndex = 0;

    private void Start()
    {
        shopMenu = GameObject.Find("Shop");
        player = GameObject.Find("Knight");
        player.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            cameras[currentCameraIndex].enabled = false;
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            cameras[currentCameraIndex].enabled = true;

            if (shopMenu.activeSelf == true && player.activeSelf == false)
            {
                shopMenu.SetActive(false);
                player.SetActive(true);
                
            }
            else
            {
                shopMenu.SetActive(true);
                player.SetActive(false);
            }
        }
    }
}
