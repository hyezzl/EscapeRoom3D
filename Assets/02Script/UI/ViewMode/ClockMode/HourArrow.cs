using UnityEngine;
using UnityEngine.EventSystems;

public class HourArrow : MonoBehaviour, IDragHandler
{
    private PlayerController pc;
    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) { Debug.Log("HourArrow - Failed to Load PlayerController"); }
    }
    public void OnDrag(PointerEventData eventData)
    {
        // ClockControll 모드 일 때만 동작
        if (pc.CurMode == PlayMode.ClockControl) {
            float rotateX = eventData.delta.x;
            float rotateY = eventData.delta.y;

        }
    }
}