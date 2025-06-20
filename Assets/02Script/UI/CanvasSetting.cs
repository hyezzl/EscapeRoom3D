using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : MonoBehaviour
{
    private Camera targetCam;
    private Canvas mainCanvas;
    private CanvasScaler scaler;

    private void Awake()
    {
        targetCam = Camera.main;
        if (!TryGetComponent<Canvas>(out mainCanvas)) {
            Debug.Log("CanvasSetting - Failed to Load Canvas");
        }

        if (mainCanvas.renderMode != RenderMode.ScreenSpaceCamera)
            mainCanvas.renderMode = RenderMode.ScreenSpaceCamera;

        mainCanvas.worldCamera = targetCam;
        mainCanvas.planeDistance = 1f;

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
