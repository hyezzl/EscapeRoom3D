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
    private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;
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
    }

    // 플레이어 이동
    private void HandleMovement() {
        Vector2 dir = inputHandler.GetMovement();
        bool isRunning = inputHandler.Run();
        bool isCrouching = inputHandler.Crouch();

        Vector3 keyboardDir = new Vector3(dir.x, 0f, dir.y).normalized;

        Vector3 moveDir = Camera.main.transform.TransformDirection(keyboardDir);
        moveDir.y = 0;

        if (dir.sqrMagnitude < 0.01f && !isRunning && !isCrouching) // Standing
        {
            noise.m_NoiseProfile = null;
        }
        else if (!isRunning && !isCrouching) // Walking
        {
            SetCameraNoise(myCameraShake, 1f, 1f);
            
            controller.Move(moveDir.normalized * (moveSpeed * Time.deltaTime));
        }
        else if (dir.sqrMagnitude > 0.01f && isCrouching) // Crouching
        {
            SetCameraNoise(myCameraShake, 0.7f, 0.7f);

            controller.Move(moveDir.normalized * (moveSpeed * crouchRatio * Time.deltaTime));
        }
        else if (dir.sqrMagnitude > 0.01f && isRunning && !isCrouching)  // Running
        {
            SetCameraNoise(myCameraShake, 3f, 2.5f);

            controller.Move(moveDir.normalized * ((moveSpeed * dashRatio) * Time.deltaTime));
        }
    }


    private void SetCameraNoise(NoiseSettings profile, float amp, float Freq) {
        if (noise != null) {
            noise.m_NoiseProfile = profile;
            noise.m_AmplitudeGain = amp;
            noise.m_FrequencyGain = Freq;
        }
    }

    
}
