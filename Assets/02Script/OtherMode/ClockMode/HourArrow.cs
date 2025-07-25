using UnityEngine;
using UnityEngine.EventSystems;

public class HourArrow : MonoBehaviour, IDragHandler
{
    [SerializeField] private float rotateSpeed = 1f;
    private PlayerController pc;
    private Camera mainCam;
    private void Awake()
    {
        mainCam = Camera.main;
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) { Debug.Log("HourArrow - Failed to Load PlayerController"); }
    }
    public void OnDrag(PointerEventData eventData)
    {
        // ClockControl 모드 일 때만 동작
        if (pc.CurMode == PlayMode.ClockControl) {
            // 어색함
            //float rotateX = eventData.delta.x * rotateSpeed;
            //float rotateY = eventData.delta.y * rotateSpeed;
            //transform.Rotate(rotateX, 0f, 0f, Space.Self);

            // 마우스 따라가기
        }
    }
}