using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{ 
    MainCam,
    PlayerCam,
    ClockCam,
    DoorCam,
}

// Corrider Scene
public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCam;
    [SerializeField] private CinemachineVirtualCamera clockCam;
    [SerializeField] private CinemachineVirtualCamera doorCam;
    [SerializeField] private List<CinemachineVirtualCamera> cams;

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
        EventBus.Instance.Subscribe<GameEvents.ChangeCam>(OnCamChanged);
        EventBus.Instance.Subscribe<GameEvents.PlayTimeline>(OnPlayTimeline);
        EventBus.Instance.Subscribe<GameEvents.EndTimeline>(OnEndTimeline);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.ChangeCam>(OnCamChanged);
        EventBus.Instance.Unsubscribe<GameEvents.PlayTimeline>(OnPlayTimeline);
        EventBus.Instance.Unsubscribe<GameEvents.EndTimeline>(OnEndTimeline);
    }

    // 모드에 따른 카메라 변경
    private void OnCamChanged(GameEvents.ChangeCam evt) {
        //switch (pc.CurMode)
        //{
        //    case PlayMode.InspectMode:
        //        SelectCam(playerCam);

        //        EventBus.Instance.Publish<GameEvents.ChangeCam>(new GameEvents.ChangeCam(CameraType.PlayerCam));
        //        break;

        //    case PlayMode.ClockControl:
        //        SelectCam(clockCam);

        //        EventBus.Instance.Publish<GameEvents.ChangeCam>(new GameEvents.ChangeCam(CameraType.ClockCam));
        //        break;
        //}
        switch (evt.type) {
            case CameraType.PlayerCam:
                SelectCam(playerCam);
                EventBus.Instance.Publish<GameEvents.ChangeLight>(new GameEvents.ChangeLight(CameraType.PlayerCam));
                break;

            case CameraType.ClockCam:
                SelectCam(clockCam);
                EventBus.Instance.Publish<GameEvents.ChangeLight>(new GameEvents.ChangeLight(CameraType.ClockCam));
                break;

            case CameraType.DoorCam:
                break;
        }
    }

    private void OnPlayTimeline(GameEvents.PlayTimeline evt)
    {
        // todo :: 고쳐야함! (타임라인 id)
        // doorcam On
        SelectCam(doorCam);

        EventBus.Instance.Publish<GameEvents.ChangeCam>(new GameEvents.ChangeCam(CameraType.DoorCam));
    }

    private void OnEndTimeline(GameEvents.EndTimeline evt)
    {
        //// doorcam Off
        SelectCam(playerCam);
        EventBus.Instance.Publish<GameEvents.ChangeCam>(new GameEvents.ChangeCam(CameraType.PlayerCam));
    }

    private void SelectCam(CinemachineVirtualCamera onCam) {
        //모두 끈 후
        foreach (var cam in cams) { 
            cam.gameObject.SetActive(false);
        }
        onCam.gameObject.SetActive(true);
    }

}
