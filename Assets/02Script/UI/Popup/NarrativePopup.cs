using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativePopup : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private CanvasGroup background;
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private TextMeshProUGUI text;

    private bool isDisplay = false;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.OpenNarrativePopup>(OnOpenNarrative);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.OpenNarrativePopup>(OnOpenNarrative);
    }

    public void OnOpenNarrative(UIEvents.OpenNarrativePopup evt) { 
        
    }
}
