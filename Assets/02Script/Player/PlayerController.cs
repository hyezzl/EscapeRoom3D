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

    [Header("Cinemachine : NoiseSetting")]
    [SerializeField] private NoiseSettings myCameraShake;

    private CharacterController controller;
    private IInputHandler inputHandler;
    //
    private Transform eyeHeight;
    private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;
    private Vector3 defaultCamHeight = new Vector3(0f, 2.5f, 0f);
    private Vector3 crouchCamHeight = new Vector3(0f, 1.7f, 0f);
    private float gravity = -9.8f;
    private Vector3 velocity;
    private Vector3 verticalDir;

    private void Awake()
    {
        if (!TryGetComponent<CharacterController>(out controller)) {
            Debug.Log("PlayerController - Failed to Load CharacterController");
        }
        if (!TryGetComponent<IInputHandler>(out inputHandler)) {
            Debug.Log("PlayerController - Failed to Load IInputHandler");
        }
        //
        eyeHeight = transform.GetChild(0);
        if (eyeHeight == null) {
            Debug.Log("PlayerController - Failed to Load Children Transform");
        }
        cam = GetComponentInChildren<CinemachineVirtualCamera>();
        if (cam == null) {
            Debug.Log("PlayerController - Failed to Load CinemachineVirtualCamera");
        }
        // 노이즈 프로파일 적용
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (noise == null) {
            Debug.Log("PlayerController - Failed to Load NoiseSetting");
        }
        
    }

    private void Update()
    {
        HandleMovement();
        ApplyGravity();
    }

    // 플레이어 이동
    private void HandleMovement() {
        Vector2 dir = inputHandler.GetMovement();
        bool isRunning = inputHandler.Run();
        bool isCrouching = inputHandler.Crouch();

        Vector3 keyboardDir = new Vector3(dir.x, 0f, dir.y).normalized;

        Vector3 horizonDir = Camera.main.transform.TransformDirection(keyboardDir);
        horizonDir.y = 0;

        // 중력까지 포함한 최종 Dir
        Vector3 moveDir = horizonDir.normalized + verticalDir;

        // 카메라 높이 (보간)
        Vector3 targetPosition = isCrouching ? crouchCamHeight : defaultCamHeight;
        eyeHeight.localPosition = Vector3.Lerp(eyeHeight.localPosition,
                                                targetPosition,
                                                6f * Time.deltaTime);


        if (dir.sqrMagnitude < 0.01f && !isRunning && !isCrouching) // Standing
        {
            noise.m_NoiseProfile = null;
        }
        else if (!isRunning && !isCrouching) // Walking
        {
            SetCameraNoise(myCameraShake, 1f, 1f);
            
            controller.Move(moveDir * (moveSpeed * Time.deltaTime));
        }
        else if (isCrouching) // Crouching
        {
            // 앉기 + 수평이동
            if (dir.sqrMagnitude > 0.01f) { 
                SetCameraNoise(myCameraShake, 0.7f, 0.7f);
            }

            controller.Move(moveDir * (moveSpeed * crouchRatio * Time.deltaTime));
        }
        else if (dir.sqrMagnitude > 0.01f && isRunning && !isCrouching)  // Running
        {
            SetCameraNoise(myCameraShake, 3f, 2.5f);

            controller.Move(moveDir * ((moveSpeed * dashRatio) * Time.deltaTime));
        }
    }


    private void SetCameraNoise(NoiseSettings profile, float amp, float Freq) {
        if (noise != null) {
            noise.m_NoiseProfile = profile;
            noise.m_AmplitudeGain = amp;
            noise.m_FrequencyGain = Freq;
        }
    }

    // 중력 구현
    private void ApplyGravity() {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0f) { 
            velocity.y = -1f;
        }
        velocity.y += gravity * Time.deltaTime;
        verticalDir = Vector3.up * velocity.y;
    }

    
}
