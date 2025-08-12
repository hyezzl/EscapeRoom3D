using UnityEngine;
using UnityEngine.UI;

public class PauseMode : MonoBehaviour
{
    [SerializeField] private CanvasGroup tapGroup;
    [SerializeField] private Button resumeBTN;
    [SerializeField] private Button hintBTN;
    [SerializeField] private Button optionBTN;
    [SerializeField] private Button quitBTN;

    [SerializeField] private bool isSelect = false;

    [SerializeField] private OptionPopup op;

    private TogglePause tp;

    private void Awake()
    {
        tp = FindAnyObjectByType<TogglePause>();
        if (tp == null) Debug.Log("PauseMode - Failed to Load TogglePause");
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.OptionSaved>(OnOptionSaved);
        resumeBTN.onClick.AddListener(() => tp.InOutPauseMode());
        optionBTN.onClick.AddListener(() => { 
            op.OnPanel();
            isSelect = true;
            HideBTNs();
        });
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.OptionSaved>(OnOptionSaved);
        resumeBTN.onClick.RemoveAllListeners();
        optionBTN.onClick.RemoveAllListeners();
    }

    public void HideBTNs() {
        tapGroup.alpha = isSelect ? 0f : 1f;
        tapGroup.interactable = !isSelect;
        tapGroup.blocksRaycasts = !isSelect;
    }

    // ¾î¶² ÆË¾÷ÀÌµç ´ÝÇûÀ» °æ¿ì, È£Ãâ
    public void ClosePopup() { 
        isSelect = false;
        HideBTNs();
    }

    public void OnOptionSaved(GameEvents.OptionSaved evt) {
        ClosePopup();
    }


}
