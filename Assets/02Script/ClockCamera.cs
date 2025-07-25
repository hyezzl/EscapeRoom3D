using UnityEngine;

public class ClockCamera : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 1.5f;

    private void Awake()
    {
        mouseSensitivity = 1.5f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("ClockMode È°¼ºÈ­");
    }
}
