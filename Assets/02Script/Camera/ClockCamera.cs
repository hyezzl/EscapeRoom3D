using UnityEngine;

public class ClockCamera : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Instance.Subscribe<PuzzleEvents.ApproachSpecial>(OnSpecial);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<PuzzleEvents.ApproachSpecial>(OnSpecial);
    }
    private void OnSpecial(PuzzleEvents.ApproachSpecial evt)
    {
        if (evt.evt == EventList.OpenClockMode)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("ClockMode È°¼ºÈ­");
        }
    }

}
