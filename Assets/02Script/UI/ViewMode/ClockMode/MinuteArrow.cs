using UnityEngine;
using UnityEngine.EventSystems;

public class MinuteArrow : MonoBehaviour, IDragHandler
{
    private PlayerController pc;
    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) { Debug.Log("MinuteArrow - Failed to Load PlayerController"); }
    }
    public void OnDrag(PointerEventData eventData)
    {
        // ClockControll 모드 일 때만 동작
        if (pc.CurMode == PlayMode.ClockControl)
        {

        }
    }
}
