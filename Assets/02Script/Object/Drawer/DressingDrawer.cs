using UnityEngine;

public class DressingDrawer : MonoBehaviour
{
    [SerializeField] private GameObject innerBook;

    private OpenableItem openable;
    private ImopenableItem imopenable;


    private void Awake()
    {
        if (!TryGetComponent<OpenableItem>(out openable))
            Debug.Log("DressingDrawer - Failed to Load OpenableItem");
        if (!TryGetComponent<ImopenableItem>(out imopenable))
            Debug.Log("DressingDrawer - Failed to Load imopenableItem");

        // 처음에는 열 수 없음
        imopenable.enabled = true;
        openable.enabled = false;
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<PuzzleEvents.SolvedPuzzle>(OnSolved);
        EventBus.Instance.Subscribe<GameEvents.DrawerUnlock>(OnOpenDrawer);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<PuzzleEvents.SolvedPuzzle>(OnSolved);
        EventBus.Instance.Unsubscribe<GameEvents.DrawerUnlock>(OnOpenDrawer);
    }

    private void OnSolved(PuzzleEvents.SolvedPuzzle evt) {
        // 촛불 퍼즐 직후
        if (evt.puzzleID == 1001) {
            Debug.Log("DressingDrawer is Open!");

            // 열 수 있음
            Destroy(imopenable); // 삭제
            openable.enabled = true;
        }
    }

    private void OnOpenDrawer(GameEvents.DrawerUnlock evt) {
        innerBook.tag = "Item";
    }
}
