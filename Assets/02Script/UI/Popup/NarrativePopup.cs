using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativePopup : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private CanvasGroup background;
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup group;

    private bool isDisplay = false;
    private bool standbyInput = false;
    private PlayerController pc;
    private Coroutine blinkCor;
    private Animator anim;
    private BlinkAnnounce blink;

    private void Awake()
    {
        if (!TryGetComponent<Animator>(out anim)) {
            Debug.Log("NarrativePopup - Failed to Load Animator");
        }
        if (!TryGetComponent<BlinkAnnounce>(out blink))
        {
            Debug.Log("NarrativePopup - Failed to Load BlinkAnnounce");
        }
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null)
            Debug.Log("NarrativePopup - Failed to Load PlayerController");
    }

    private void Update()
    {
        if (!isDisplay) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // 창닫기
            StartCoroutine(OnclosePanel());
        }
        
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.OpenNarrativePopup>(OnOpenNarrative);
        scroll.onValueChanged.AddListener(OnScrollChanged);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.OpenNarrativePopup>(OnOpenNarrative);
        scroll.onValueChanged.RemoveListener(OnScrollChanged);
    }

    public void OnOpenNarrative(UIEvents.OpenNarrativePopup evt) { 
        isDisplay = true;
        background.enabled = true;
        scroll.enabled = true;
        text.enabled = true;
        anim.SetTrigger("OnOpenNarrative");

        // Narrative Mode 실행
        pc.CurMode = PlayMode.NarrativeMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());

        if (evt.item.GetType() != ItemType.Readable) return;

        ReadableData data = ItemDatabaseManager.Instance.GetReadable(evt.item.GetItemID());
        if (data != null) {
            ShowNarrative(data);
        }
    }

    // 스크롤 변화값 반영
    private void OnScrollChanged(Vector2 pos) {
        if (scroll.verticalNormalizedPosition <= 0.01f && !standbyInput) {
            StartCoroutine(Delay());
        }
    }

    public void ShowNarrative(ReadableData data) {
        text.text = data.narrative;
    }

    IEnumerator OnclosePanel() {
        anim.SetTrigger("OnCloseNarrative");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        StopCoroutine(blinkCor);

        // Inspector Mode
        pc.CurMode = PlayMode.InspectMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());

        Debug.Log("Narrative 종료");
        background.enabled = false;
        text.enabled = false;
        isDisplay = false;
        group.alpha = 0f;

    }

    IEnumerator Delay() {
        if (standbyInput) yield break;

        yield return new WaitForSeconds(3f);
        blinkCor = StartCoroutine(blink.BlinkAnnounceMSG(group));
        standbyInput = true;
    }
}
