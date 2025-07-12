using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Cinemachine : NoiseSetting")]
    [SerializeField] private NoiseSettings myCameraShake;
    private PlayerController pc;
    private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;


    private void Awake()
    {
        if (!TryGetComponent<PlayerController>(out pc)) {
            Debug.Log("CameraShake - Failed to Load PlayerController");
        }
        cam = GetComponentInChildren<CinemachineVirtualCamera>();
        if (cam == null)
        {
            Debug.Log("PlayerController - Failed to Load CinemachineVirtualCamera");
        }
        // 노이즈 프로파일 적용
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (noise == null)
        {
            Debug.Log("PlayerController - Failed to Load NoiseSetting");
        }
    }

    private void Update()
    {
        switch (pc.PlayerState) {
            case PlayerState.Standing:
                noise.m_NoiseProfile = null;
                break;
            case PlayerState.Walking:
                SetCameraNoise(myCameraShake, 0.3f, 0.6f);
                break;
            case PlayerState.Running:
                SetCameraNoise(myCameraShake, 0.9f, 0.9f);
                break;
            case PlayerState.Crouching:
                SetCameraNoise(myCameraShake, 0.1f, 0.1f);
                break;
        }
    }

    private void SetCameraNoise(NoiseSettings profile, float amp, float Freq)
    {
        if (noise != null)
        {
            noise.m_NoiseProfile = profile;
            noise.m_AmplitudeGain = amp;
            noise.m_FrequencyGain = Freq;
        }
    }
}
