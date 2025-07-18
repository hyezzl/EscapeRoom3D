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
    PauseMode, // 멈춤
    InventoryMode, // 인벤토리
    NarrativeMode, 
    DialogMode, // Dialog창 떠있을 때
    ViewMode, // 아이템 자세히 보기
}


public class PlayerController : MonoBehaviour
{
    public PlayMode CurMode { get; set; } = PlayMode.InspectMode;
    public PlayerState CurState { get; set; }

    // Temporary
    [SerializeField] private GameObject archDoor;
    private ArchDoor arch;
    private bool temp = false;


    private void Awake()
    {
        //Temporary
        arch = archDoor.GetComponent<ArchDoor>();
        if (arch == null)
            Debug.Log("임시파일 참조 오류");

    }

    private void Update()
    {
        TestFunction();
    }


    // 문 임시 Test용
    private void TestFunction() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!temp)
            {
                arch.OpenArchDoor();
                temp = true;
            }
            else {
                arch.CloseArchDoor();
                temp = false;
            }
        }
    }
}
