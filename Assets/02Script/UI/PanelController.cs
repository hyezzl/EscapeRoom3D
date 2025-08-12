using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] protected RectTransform targetPanel;
    [SerializeField] protected Vector2 displayPos = Vector2.zero;
    [SerializeField] protected Vector2 hidePos = new Vector2(1800f, 0f);
    [SerializeField] protected float tweenTime = 0.5f;
    public bool isPanelOpen = false;

    protected virtual void Awake() {
        if (targetPanel != null) {
            targetPanel.anchoredPosition = hidePos;
        }
    }

    // tween
    public virtual void Display() {
        if (isPanelOpen) return;
        isPanelOpen = true;
        LeanTween.move(targetPanel, displayPos, tweenTime).setEaseInOutQuart();
    }

    public virtual void Hide() {
        if (!isPanelOpen) return;
        isPanelOpen = false;
        LeanTween.move(targetPanel, hidePos, tweenTime).setEaseInOutExpo();
    }

    public virtual void TogglePanel() {
        if (isPanelOpen)
            Hide();
        else
            Display();
    }


    // blink
    public virtual void OnPanel() {
        if (isPanelOpen) return;
        isPanelOpen = true;
        targetPanel.gameObject.SetActive(true);
    }

    public virtual void OffPanel() {
        if (!isPanelOpen) return;
        isPanelOpen = false;
        targetPanel.gameObject.SetActive(false);
    }
}
