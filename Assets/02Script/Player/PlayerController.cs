using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Setting")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashRatio = 2.5f;

    private CharacterController controller;
    private IInputHandler inputHandler;

    private void Awake()
    {
        if (!TryGetComponent<CharacterController>(out controller)) {
            Debug.Log("PlayerController - Failed to Load CharacterController");
        }
        if (!TryGetComponent<IInputHandler>(out inputHandler)) {
            Debug.Log("PlayerController - Failed to Load IInputHandler");
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    // 플레이어 이동
    private void HandleMovement() {
        Vector2 dir = inputHandler.GetMovement();
        bool isRunning = inputHandler.Run();
        Vector3 keyboarDir = new Vector3(dir.x, 0f, dir.y).normalized;

        Vector3 moveDir = Camera.main.transform.TransformDirection(keyboarDir).normalized;
        moveDir.y = 0;

        if (!isRunning)
        {
            controller.Move(moveDir * (moveSpeed * Time.deltaTime));
        }
        else {
            controller.Move(moveDir * ((moveSpeed * dashRatio) * Time.deltaTime));
        }
    }

    private void PopupInventory() { 
        
    }

    
}
