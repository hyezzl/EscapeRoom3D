using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 1.5f;
    [SerializeField] Transform player;
    private float cameraVertical = 0f;
    private PlayerController pc;

    private void Awake()
    {       
        transform.localPosition = new Vector3(0f, 2.5f, 0f);
        player = transform.parent;
        if (player == null) {
            Debug.Log("VirtualCamera - Failed to Load Player");
        }
        if (!player.TryGetComponent<PlayerController>(out pc)) {
            Debug.Log("VirtualCamera - Failed to Load PlayerController");
        }
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.GameModeChange>(_ => ChangeCursor());
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.GameModeChange>(_ => ChangeCursor());
    }

    private void ChangeCursor() {
        switch (pc.CurMode)
        {
            case PlayMode.InspectMode:
                mouseSensitivity = 1.5f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Debug.Log("InspectMode 활성화");
                break;

            case PlayMode.PauseMode:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("PauseMode 활성화");
                break;

            case PlayMode.InventoryMode:
                mouseSensitivity = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("InventoryMode 활성화");
                break;
        }
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
