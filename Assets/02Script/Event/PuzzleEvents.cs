using UnityEngine;

public enum EventList
{
    OpenClockMode,
}

public class PuzzleEvents : MonoBehaviour
{
    public struct SolvedPuzzle {
        public int puzzleID;

        public SolvedPuzzle(int puzzleID) {
            this.puzzleID = puzzleID;
        }
    }

    public struct ApproachSpecial {
        public EventList evt;

        public ApproachSpecial(EventList evt) {
            this.evt = evt;
        }
    }

    public struct DoInteract
    {
        public int pairID;
        public GameObject target;

        public DoInteract(int pairID, GameObject target) { 
            this.pairID = pairID;
            this.target = target;
        }
    }
}
