using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArchDoor : MonoBehaviour
{
    [SerializeField] private Transform leftPivot;
    [SerializeField] private Transform rightPivot;
    [SerializeField] private GameObject leftSide;
    [SerializeField] private GameObject rightSide;
    [SerializeField] private int uniquePairID = 100;

    private bool isArchOpen = false;
    private Animator anim;
    private bool isSubcribed = false; // 중복 이벤트 방지

    private void Awake()
    {
        if (!TryGetComponent<Animator>(out anim)) {
            Debug.Log("ArchDoor - Failed to Load Animator");
        }
    }

    private void OnEnable()
    {
        if (!isSubcribed) { 
            EventBus.Instance.Subscribe<GameEvents.KnockDoor>(Onknock);
            isSubcribed = true;
        }
    }
    private void OnDisable()
    {
        if (isSubcribed) { 
            EventBus.Instance.Unsubscribe<GameEvents.KnockDoor>(Onknock);
            isSubcribed = false;
        }
    }

    private void Onknock(GameEvents.KnockDoor evt) {
        ////todo : 3개의 문 중 1개 특정하기 !!!!!!!!!
        if (uniquePairID == evt.pairID) {
            // 문 열림
            EventBus.Instance.Publish<PuzzleEvents.SolvedPuzzle>(new PuzzleEvents.SolvedPuzzle(1000));

            // 문열리는 사운드

            // 비활성화
            leftSide.tag = "Deactive";
            rightSide.tag = "Deactive";

            // 타임라인 이벤트
            EventBus.Instance.Publish<GameEvents.PlayTimeline>(new GameEvents.PlayTimeline("T001"));

            anim.SetTrigger("ForceOpen"); // 강제열기
        }
    }


    public void OpenArchDoor() {
        if(!isArchOpen)
            anim.SetTrigger("ArchDoorOpen");
    }

    public void CloseArchDoor() {
        if (isArchOpen)
            anim.SetTrigger("ArchDoorClose");
    }
}
