using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinuteArrow : MonoBehaviour//, IDragHandler, IEndDragHandler
{
    private ClockControl cc;
    private PlayerController pc;
    private Camera mainCam;
    private bool isSelected = false;

    public float CurMinute => transform.localEulerAngles.x;

    private void Awake()
    {
        mainCam = Camera.main;

        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) { Debug.Log("MinuteArrow - Failed to Load PlayerController"); }

        cc = GetComponentInParent<ClockControl>();
        if (cc == null) Debug.Log("MinuteArrow - Failed to Load ClockControl");
    }

    // 보류
    //public void OnDrag(PointerEventData eventData)
    //{
    //    // ClockControl 모드 일 때만 동작
    //    if (pc.CurMode == PlayMode.ClockControl)
    //    {
    //        // 드래그 방해 레이어 무시
    //        Ray ray = mainCam.ScreenPointToRay(eventData.position);
    //        RaycastHit hit;
    //        int ignoreMask = (-1) - (1 << LayerMask.NameToLayer("Player"));
    //        //Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4f, ignoreMask);

    //        Physics.Raycast(ray, out hit, 4f, ignoreMask);
    //        Vector3 dragPos = hit.point; // 레이어 제외 후 닿은 지점

    //        // 마우스 따라가기
    //        Vector3 pivotPos = mainCam.WorldToScreenPoint(transform.position); // 바늘중심
    //        //Vector2 dir = (Vector2)eventData.position - (Vector2)pivotPos;
    //        Vector2 dir = (Vector2)mainCam.WorldToScreenPoint(dragPos) - (Vector2)pivotPos;

    //        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // 라디안 > 도

    //        transform.localRotation = Quaternion.AngleAxis(-angle + 90, Vector3.right);
    //        // 12를 가리킬 때 (0도일때) y에 대한 rotation값이 90
    //    }
    //}
    //void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    //{
    //    // 드래그 끝날 때마다, 값이 맞는지 확인
    //    //Debug.Log($"minute : {transform.localEulerAngles.x}");
    //    cc.CheckAnswer();
    //}

    //===========================

    private void Update()
    {
        if (pc.CurMode == PlayMode.ClockControl || Input.GetMouseButtonDown(0)) {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition; // 화면상의 좌표
    
            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(pointerData, results);
    
            foreach (var result in results) {
                Debug.Log(result.gameObject.name);
            }
        }
    }
}
