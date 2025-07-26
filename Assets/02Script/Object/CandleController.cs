using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    private bool isSolved = false;

    private List<bool> firelist = new List<bool> { false, false, false, false, false, false, false };

    public void Set(int index, bool isFire)
    {
        if (index < 0 || index > 6) return;
        firelist[index] = isFire;
    }

    public void CheckAnswer() {
        // ´ä (index 0°ú 4¸¸ false / ¿Ü ¸ðµÎ true)
        Debug.Log(firelist);
        if (firelist.Count(x => !x) == 2 && !firelist[0] && !firelist[4]) {
            Debug.Log("ÃÐºÒ ÆÛÁñ ¿Ï·á!");
            isSolved = true;
            // Ã¶ÄÀ ¿­¸®´Â ¼Ò¸®

            // EventAfter
            EventBus.Instance.Publish<UIEvents.EventAfter>(new UIEvents.EventAfter("E004"));

            EventBus.Instance.Publish<PuzzleEvents.SolvedPuzzle>(new PuzzleEvents.SolvedPuzzle(1001));
        }
    }
}
