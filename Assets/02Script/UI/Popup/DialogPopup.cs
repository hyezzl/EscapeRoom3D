using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogPopup : MonoBehaviour
{
    // Interactable(Monologue,DeactiveMSG)
    // Inspectable (Monologue)
    // Readable (Monologue / Reply)

    [Header("UI Refs")]
    [SerializeField] private RectTransform targetPanel;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;

    private bool isDisplay = false;
    private Color fullColor = new Color(255, 255, 255, 255);
    private Color displayColor = new Color(200, 200, 200, 200);
    private Color hideColor = new Color(0, 0, 0, 0);

    private void Awake()
    {
        if (targetPanel != null) { }
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.OpenDialogPopup>(OnOpenDialog);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.OpenDialogPopup>(OnOpenDialog);
    }

    public void OnOpenDialog(UIEvents.OpenDialogPopup evt) {
        background.color = displayColor;
        text.color = fullColor;
        
        // 분기
        switch (evt.item.GetType()) {
            case ItemType.Interactable:

                break;
            case ItemType.Inspectable:
                break;
            case ItemType.Readable:
                break;
        };
        
    }
    // space로 다음대화 next
}
