using System.Collections;
using Cinemachine;
using UnityEngine;

// ClockMode
public class ClockControl : MonoBehaviour
{
    [SerializeField] private HourArrow hourArrow;
    [SerializeField] private MinuteArrow minuteArrow;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private GameObject obj;
    private PlayerController pc;


    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) Debug.Log("ClockControl - Failed to Load PlayerController");
    }

    private void Update()
    {
        if (pc.CurMode == PlayMode.ClockControl
            && Input.GetKeyDown(KeyCode.Escape)) {
            // 모드 나가기
            StartCoroutine(ExitClockMode());
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
    private IEnumerator ExitClockMode() {
        yield return null;
        // 모드종료
        pc.CurMode = PlayMode.InspectMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
    }

    // 정답 체크 (외부 호출)
    public void CheckAnswer()
    {
        float hour = NormalizeAngle(hourArrow.CurHour);
        float minute = NormalizeAngle(minuteArrow.CurMinute);

        if (Answer(hour, minute))
        {
            // ★★★★★★ 퍼즐해결
            EventBus.Instance.Publish<PuzzleEvents.SolvedPuzzle>(new PuzzleEvents.SolvedPuzzle(1002));

            // 비활성화
            obj.tag = "Deactive"; // 태그변경으로 비활성화
            hourArrow.enabled = false;
            minuteArrow.enabled = false;

             // EventAfter
            EventBus.Instance.Publish<UIEvents.EventAfter>(new UIEvents.EventAfter("E005"));

            // 강제 모드 종료
            StartCoroutine(ExitClockMode());
        }
    }
    private float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0f) angle += 360f;
        return angle;
    }


    // 정답체크 (후에 조정!!!!!!!!!!)
    private bool Answer(float hour, float minute)
    {
        return IsCorrect(hour, 0f) && (minute >= 85f && minute <= 90f);
    }

    private bool IsCorrect(float current, float target, float margin = 3f)
    {
        float delta = Mathf.Abs(NormalizeAngle(current) - NormalizeAngle(target));
        return delta <= margin || delta >= 360f - margin;
    }
}
