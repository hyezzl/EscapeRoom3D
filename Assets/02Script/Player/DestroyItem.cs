using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    private GameObject usedItem;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<PuzzleEvents.SolvedPuzzle>(DestroyUsedItem);

    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<PuzzleEvents.SolvedPuzzle>(DestroyUsedItem);

    }

    private void DestroyUsedItem(PuzzleEvents.SolvedPuzzle evt) {
        // 자식이 있을때만 실행
        if (transform.childCount > 0) { 
            usedItem = gameObject.transform.GetChild(0).gameObject;
            if (usedItem != null) { 
                Destroy(usedItem);
            }
        }
    }
}
