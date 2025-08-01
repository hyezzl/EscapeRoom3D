using UnityEngine;

public class ClockCamera : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("ClockMode È°¼ºÈ­");
    }
}
