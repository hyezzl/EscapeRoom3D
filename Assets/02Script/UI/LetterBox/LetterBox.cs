using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    private void Awake()
    {
        if (TryGetComponent<Camera>(out Camera mainCam)) {
            float targetAspect = 16.0f / 9.0f;
            float screenAspect = (float)Screen.width / Screen.height;

            float ratio = screenAspect / targetAspect;

            Rect rect = mainCam.rect;

            if (ratio < 1) // 가로 < 세로 
            {
                rect.width = 1f;
                rect.height = ratio;
                rect.x = 0f;
                rect.y = (1f - ratio) / 2f;
            }
            else
            {
                float ratio2 = 1f / ratio;
                rect.width = ratio2;
                rect.height = 1f;
                rect.x = (1f - ratio2) / 2f;
                rect.y = 0f;
            }
            mainCam.rect = rect;
        }
        else
            Debug.Log("LetterBox - Failed to Load Camera(Main)");
    }
}
