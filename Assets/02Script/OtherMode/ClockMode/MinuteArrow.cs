using UnityEngine;
using UnityEngine.EventSystems;

public class MinuteArrow : MonoBehaviour, IDragHandler
{
    [SerializeField] private float rotateSpeed = 1f;
    private PlayerController pc;
    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) { Debug.Log("MinuteArrow - Failed to Load PlayerController"); }
    }
    public void OnDrag(PointerEventData eventData)
    {
        // ClockControl 모드 일 때만 동작
        if (pc.CurMode == PlayMode.ClockControl)
        {
            float rotateX = eventData.delta.x * rotateSpeed;
            float rotateY = eventData.delta.y * rotateSpeed;


            transform.Rotate(rotateX + rotateY, 0f, 0f);
        }
    }
}
