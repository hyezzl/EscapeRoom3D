using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    private GameObject selectedArrow;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        // clockcontrol 모드에서.////////////////////////////////////////
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition; // 화면상의 좌표

            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                if (result.gameObject.CompareTag("Arrow"))
                {
                    //Debug.Log(result.gameObject.name);
                    selectedArrow = result.gameObject;
                    break;
                }
            }
        }
        //selected이면 움직임
        if (Input.GetMouseButton(0)) {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition; // 화면상의 좌표

            if (selectedArrow != null) { 
                Vector3 pivotPos = mainCam.WorldToScreenPoint(selectedArrow.transform.position); // 바늘중심
                Vector2 dir = (Vector2)Input.mousePosition - (Vector2)pivotPos;

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // 라디안 > 도

                selectedArrow.transform.localRotation = Quaternion.AngleAxis(-angle + 90, Vector3.right);
                //12를 가리킬 때 (0도일때) y에 대한 rotation값이 90
            
            }
        
        }

        // selected = false
        if (Input.GetMouseButtonUp(0)) { 
            selectedArrow = null;
            // 값 비교 이벤트
            // ClockControl
        }
    }
}
