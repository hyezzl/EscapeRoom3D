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
            case PlayMode.InventoryMode:
                playerCam.MoveToTopOfPrioritySubqueue(); // 우선순위 높임
                break;

            case PlayMode.ClockControl:
                clockCam.MoveToTopOfPrioritySubqueue();
                break;
        }
    }



}
