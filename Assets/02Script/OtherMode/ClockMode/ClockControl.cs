using UnityEngine;
using UnityEngine.EventSystems;

// ClockMode
public class ClockControl : MonoBehaviour
{
    [SerializeField] private Transform hourArrow;
    [SerializeField] private Transform minuteArrow;
    [SerializeField] private float rotateSpeed;
    private PlayerController pc;

    // 정답
    //private float hourRot = 

    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) Debug.Log("ClockControl - Failed to Load PlayerController");
    }

    private void Update()
    {
        if (pc.CurMode == PlayMode.ClockControl
            && Input.GetKeyDown(KeyCode.Escape)) {
            // 나가기
            ExitClockMode();
        }
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<PuzzleEvents.ApproachSpecial>(OnSpecial);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<PuzzleEvents.ApproachSpecial>(OnSpecial);
    }

    private void OnSpecial(PuzzleEvents.ApproachSpecial evt) {
        if (evt.evt == EventList.OpenClockMode) {
            //모드 변경
            pc.CurMode = PlayMode.ClockControl;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
        }
    }
    private void ExitClockMode() {
        // 모드종료
        pc.CurMode = PlayMode.InspectMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
    }
}
