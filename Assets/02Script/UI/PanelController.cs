using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] protected RectTransform targetPanel;
    [SerializeField] protected Vector2 displayPos = Vector2.zero;
    [SerializeField] protected Vector2 hidePos = new Vector2(1800f, 0f);
    [SerializeField] protected float tweenTime = 0.5f;
    protected bool isOpenInventory = false;

    protected virtual void Awake() {
        if (targetPanel != null) {
            targetPanel.anchoredPosition = hidePos;
        }
    }

    public virtual void Display() {
        if (isOpenInventory) return;
        isOpenInventory = true;
        LeanTween.move(targetPanel, displayPos, tweenTime).setEaseInOutQuart();
    }

    public virtual void Hide() {
        if (!isOpenInventory) return;
        isOpenInventory = false;
        LeanTween.move(targetPanel, hidePos, tweenTime).setEaseInOutExpo();
    }

    public virtual void TogglePanel() {
        if (isOpenInventory)
            Hide();
        else
            Display();
    }
}
