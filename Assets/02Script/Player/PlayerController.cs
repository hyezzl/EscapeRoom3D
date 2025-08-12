using UnityEngine;

public enum PlayerState
{ 
    Standing,
    Walking,
    Running,
    Crouching
}

public enum PlayMode
{ 
    InspectMode, // 1인칭 커서고정
    PauseMode, // 멈춤(옵션 / 힌트)
    InventoryMode, // 인벤토리
    NarrativeMode, 
    DialogMode, // Dialog창 떠있을 때
    ViewMode, // 아이템 자세히 보기

    ClockControl, // 시계 조작
}


public class PlayerController : MonoBehaviour
{
    
    public PlayMode CurMode { get; set; } = PlayMode.InspectMode;
    
    public PlayerState CurState { get; set; }

}
