using cakeslice;
using UnityEngine;

public class Casket : MonoBehaviour
{
    [SerializeField] private InteractableItem item;
    [SerializeField] private GameObject key;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim == null) Debug.Log("Casket - Failed to Load Animator");
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<PuzzleEvents.DoInteract>(OnInteract);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<PuzzleEvents.DoInteract>(OnInteract);
    }

    private void OnInteract(PuzzleEvents.DoInteract evt) {
        if (evt.pairID == item.GetPairID()) {
            // ★★★★★★★퍼즐 해결

            // 애니메이션
            anim.SetTrigger("CasketOpen");

            // destroy

            // EventAfter

            // key활성화 / 비활성화
            key.tag = "Item";
            gameObject.tag = "Deactive";
            gameObject.GetComponent<Outline>().enabled = false;


            EventBus.Instance.Publish<PuzzleEvents.SolvedPuzzle>(new PuzzleEvents.SolvedPuzzle(1004));
        }
    }
}
