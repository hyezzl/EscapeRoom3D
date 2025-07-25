using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] private CinemachineVirtualCamera clockCam;
    // todo : 뷰모드 카메라도 후에 여기에 넣도록!
    private PlayerController pc;

    protected override void DoAwake()
    {
        base.DoAwake();
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) Debug.Log("CameraManager - Failed to Load PlayerController");
    }

    private void Start()
    {
        playerCam.Priority = 11;
        clockCam.Priority = 10;
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.GameModeChange>(OnModeChange);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.GameModeChange>(OnModeChange);
    }

    // 모드에 따른 카메라 변경
    private void OnModeChange(GameEvents.GameModeChange evt) {
        switch (pc.CurMode)
        {
            case PlayMode.InspectMode:
                clockCam.Priority = 10;
                break;

            case PlayMode.ClockControl:
                clockCam.Priority = 12;
                break;
        }
    }

}
