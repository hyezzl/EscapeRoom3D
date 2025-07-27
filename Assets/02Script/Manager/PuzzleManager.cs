using System.Collections.Generic;
using UnityEngine;
using static PuzzleEvents;


public class PuzzleManager : Singleton<PuzzleManager>
{
    // 완료된 퍼즐 리스트
    public static Dictionary<int, SolvedPuzzle> completePuzzle;

    protected override void DoAwake()
    {
        base.DoAwake();
        completePuzzle = new Dictionary<int, SolvedPuzzle>();
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<PuzzleEvents.SolvedPuzzle>(AddCompletePuzzle);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<PuzzleEvents.SolvedPuzzle>(AddCompletePuzzle);
    }

    // 완료한 퍼즐 static Dictionary에 저장
    private void AddCompletePuzzle(PuzzleEvents.SolvedPuzzle evt) {
        Debug.Log($"{evt.puzzleID}번 퍼즐이 완료!");

        if (!completePuzzle.ContainsKey(evt.puzzleID)) {
            completePuzzle[evt.puzzleID] = evt;
        }
        Debug.Log($"현재 완료한 퍼즐 개수 : {completePuzzle.Count}");
    }
}
