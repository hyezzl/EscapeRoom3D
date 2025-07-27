using Cinemachine;
using UnityEngine;

public enum CameraType
{ 
    MainCam,
    PlayerCam,
    ClockCam,
    DoorCam,
}


public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] private CinemachineVirtualCamera clockCam;
    [SerializeField] private CinemachineVirtualCamera doorCam;

    // todo : 뷰모드 카메라도 후에 여기에 넣도록!
    private PlayerController pc;
    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) Debug.Log("CameraManager - Failed to Load PlayerController");
    }   

    private void Start()
    {
        playerCam.Priority = 11;
        clockCam.Priority = 10;
        doorCam.Priority = 10;
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.GameModeChange>(OnModeChange);
        EventBus.Instance.Subscribe<GameEvents.PlayTimeline>(OnPlayTimeline);
        EventBus.Instance.Subscribe<GameEvents.EndTimeline>(OnEndTimeline);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.GameModeChange>(OnModeChange);
        EventBus.Instance.Unsubscribe<GameEvents.PlayTimeline>(OnPlayTimeline);
        EventBus.Instance.Unsubscribe<GameEvents.EndTimeline>(OnEndTimeline);
    }

    // 모드에 따른 카메라 변경
    private void OnModeChange(GameEvents.GameModeChange evt) {
        switch (pc.CurMode)
        {
            case PlayMode.InspectMode:
                clockCam.Priority = 10;

                EventBus.Instance.Publish<GameEvents.ChangeCam>(new GameEvents.ChangeCam(CameraType.PlayerCam));
                break;

            case PlayMode.ClockControl:
                clockCam.Priority = 12;

                EventBus.Instance.Publish<GameEvents.ChangeCam>(new GameEvents.ChangeCam(CameraType.ClockCam));
                break;
        }
    }

    private void OnPlayTimeline(GameEvents.PlayTimeline evt)
    {
        // todo :: 고쳐야함! (타임라인 id)
        // doorcam On
        doorCam.Priority = 100;

        EventBus.Instance.Publish<GameEvents.ChangeCam>(new GameEvents.ChangeCam(CameraType.DoorCam));
    }

    private void OnEndTimeline(GameEvents.EndTimeline evt)
    {
        //// doorcam Off
        doorCam.Priority = 9;
        EventBus.Instance.Publish<GameEvents.ChangeCam>(new GameEvents.ChangeCam(CameraType.PlayerCam));
    }

}
