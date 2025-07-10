using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPopup : MonoBehaviour
{
    [SerializeField] private RectTransform targetPanel;
    private bool isDisplay = false;
    // Interactable(Monologue,DeactiveMSG)
    // Inspectable (Monologue)
    // Readable (Monologue / Reply)

    private void Awake()
    {
        if (targetPanel != null) { }
    }

    private void OnEnable()
    {
        
    }
}
