using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : MonoBehaviour
{
    //main
    [SerializeField] private Camera targetCam;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RenderMode rm;
    [SerializeField] private float planeDistance = 0.35f;

    private CanvasScaler scaler;

    private void Awake()
    {
        if (canvas.renderMode != rm)
            canvas.renderMode = rm;

        canvas.worldCamera = targetCam;
        canvas.planeDistance = planeDistance;

        if (!TryGetComponent<CanvasScaler>(out scaler))
        {
            Debug.Log("CanvasSetting - Failed to Load CanvasScaler");
        }
        else {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
        }   
    }
}
