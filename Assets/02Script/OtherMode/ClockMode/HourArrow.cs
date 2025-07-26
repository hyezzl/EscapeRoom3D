using UnityEngine;
using UnityEngine.EventSystems;

public class HourArrow : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private ClockControl cc;
    private PlayerController pc;
    private Camera mainCam;

    public float CurHour => transform.localEulerAngles.x;

    private void Awake()
    {
        mainCam = Camera.main;

        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) { Debug.Log("HourArrow - Failed to Load PlayerController"); }

        cc = GetComponentInParent<ClockControl>();
        if (cc == null) Debug.Log("HourArrow - Failed to Load ClockControl");

    }
    public void OnDrag(PointerEventData eventData)
    {
        // ClockControl 모드 일 때만 동작
        if (pc.CurMode == PlayMode.ClockControl) {
            // 마우스 따라가기
            Vector3 pivotPos = mainCam.WorldToScreenPoint(transform.position); // 바늘중심
            Vector2 dir = (Vector2)eventData.position - (Vector2)pivotPos;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // 라디안 > 도

            transform.localRotation = Quaternion.AngleAxis(-angle + 90, Vector3.right);
            // 12를 가리킬 때 (0도일때) y에 대한 rotation값이 90)
        } 
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        // 드래그 끝날 때마다, 값이 맞는지 확인
        //Debug.Log($"hour : {transform.localEulerAngles.x}");
        cc.CheckAnswer();
    }
}