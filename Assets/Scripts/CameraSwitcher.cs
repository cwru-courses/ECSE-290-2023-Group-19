using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;
    public KeyCode switchKey = KeyCode.Tab;

    private int currentCameraIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            cameras[currentCameraIndex].enabled = false;
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            cameras[currentCameraIndex].enabled = true;
        }
    }
}
