using System.Collections.Generic;
using UnityEngine;

public class CotbedDrawer : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    private OpenableItem openable;


    private void Awake()
    {
        if (!TryGetComponent<OpenableItem>(out openable))
            Debug.Log("DressingDrawer - Failed to Load OpenableItem");
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

    private void OnSolved(PuzzleEvents.SolvedPuzzle evt)
    {
        // 시계 퍼즐 해결한 직후
        if (evt.puzzleID == 1002)
        {
            Debug.Log("Cotbed Drawer is Open!");
            gameObject.tag = "Item"; // 상호작용 가능
        }
    }

    private void OnOpenDrawer(GameEvents.DrawerUnlock evt)
    {
        foreach (var obj in objects)
        {
            obj.tag = "Item";
        }

        EventBus.Instance.Publish<UIEvents.EventAfter>(new UIEvents.EventAfter("E006"));
    }
}