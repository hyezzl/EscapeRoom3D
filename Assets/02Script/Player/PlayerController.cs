using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlayerState
{ 
    Standing,
    Walking,
    Running,
    Crouching
}

public class PlayerController : MonoBehaviour
{
    [Header("Basic Setting")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashRatio = 2.5f;
    [SerializeField] private float crouchRatio = 0.4f;

    private CharacterController controller;
    private IInputHandler inputHandler;
    private CinemachineVirtualCamera cam;
    private Vector3 defaultCamHeight = new Vector3(0f, 1.7f, 0f);
    private Vector3 crouchCamHeight = new Vector3(0f, 1.0f, 0f);

    private void Awake()
    {
        if (!TryGetComponent<CharacterController>(out controller)) {
            Debug.Log("PlayerController - Failed to Load CharacterController");
        }
        if (!TryGetComponent<IInputHandler>(out inputHandler)) {
            Debug.Log("PlayerController - Failed to Load IInputHandler");
        }
        //cam = GetComponentInChildren<CinemachineVirtualCamera>();
        //if (cam == null) {
        //    Debug.Log("PlayerController - Failed to Load CinemachineVirtualCamera");
        //}
        //cam = GetComponentInChildren<GameObject>();
        //if (cam == null)
        //{
        //    Debug.Log("PlayerController - Failed to Load Children's Transform");
        //}
        //else {
        //    Debug.Log($"{cam.gameObject.name}");
        //}
        
    }

    private void Update()
    {
        HandleMovement();
    }

    // 플레이어 이동
    private void HandleMovement() {
        Vector2 dir = inputHandler.GetMovement();
        bool isRunning = inputHandler.Run();
        bool isCrouching = inputHandler.Crouch();

        Vector3 keyboardDir = new Vector3(dir.x, 0f, dir.y).normalized;

        Vector3 moveDir = Camera.main.transform.TransformDirection(keyboardDir);
        moveDir.y = 0;

        if (!isRunning && !isCrouching)
        {
            cam.transform.localPosition = defaultCamHeight;
            controller.Move(moveDir.normalized * (moveSpeed * Time.deltaTime));
        }
        else if (isCrouching) {
            //카메라 낮아짐
            cam.transform.localPosition = crouchCamHeight;
            Debug.Log(cam.transform.localPosition);
            controller.Move(moveDir.normalized * (moveSpeed * crouchRatio * Time.deltaTime));
        }
        else if (isRunning && !isCrouching) {
            cam.transform.localPosition = defaultCamHeight;
            controller.Move(moveDir.normalized * ((moveSpeed * dashRatio) * Time.deltaTime));
        }
    }


    private void PopupInventory() { 
        
    }

    
}
