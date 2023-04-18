using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;
    public KeyCode switchKey = KeyCode.Tab;
    private GameObject shopMenu;

    private int currentCameraIndex = 0;

    private void Start()
    {
        shopMenu = GameObject.Find("Shop");
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            cameras[currentCameraIndex].enabled = false;
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            cameras[currentCameraIndex].enabled = true;

            if (shopMenu.activeSelf == true)
            {
                shopMenu.SetActive(false);
            }
            else
            {
                shopMenu.SetActive(true);
            }
        }
    }
}
