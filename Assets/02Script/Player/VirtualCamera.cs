using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 1.5f;
    public Transform player;
    public float cameraVertical = 0f;

    private void Awake()
    {
        transform.localPosition = new Vector3(0f, 1.7f, 0f);
        player = transform.parent;
        if (player == null) {
            Debug.Log("VirtualCamera - Failed to Load Player");
        }
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Y값은 카메라에 적용
        cameraVertical -= inputY;
        cameraVertical = Mathf.Clamp(cameraVertical, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVertical;

        // X값은 플레이어 자체 회전
        player.Rotate(Vector3.up * inputX);
    }
}
