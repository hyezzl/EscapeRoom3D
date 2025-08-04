using UnityEngine;

public class ArrowControl : MonoBehaviour
{
    protected Camera mainCam;
    protected PlayerController pc;
    public bool isSelected = false;

    protected virtual void Awake()
    {
        mainCam = Camera.main;
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) Debug.Log("ArrowControl - Failed to Load PlayerController");
    }

    public void RotateArrow() {
        Vector3 pivotPos = mainCam.WorldToScreenPoint(transform.position); // 바늘중심
        Vector2 dir = (Vector2)Input.mousePosition - (Vector2)pivotPos;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // 라디안 > 도

        transform.localRotation = Quaternion.AngleAxis(-angle + 90, Vector3.right);
        //12를 가리킬 때 (0도일때) y에 대한 rotation값이 90
    }
}
