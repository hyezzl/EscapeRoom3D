using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrativePopup : PanelController
{
    [Header("UI Refs")]
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup group; // Announce
    [SerializeField] private RectTransform content;

    private bool standbyInput = false;
    private PlayerController pc;
    private Coroutine blinkCor;
    private BlinkAnnounce blink;

    protected override void Awake()
    {
        base.Awake();
        if (!TryGetComponent<BlinkAnnounce>(out blink))
        {
            Debug.Log("NarrativePopup - Failed to Load BlinkAnnounce");
        }
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null)
            Debug.Log("NarrativePopup - Failed to Load PlayerController");
    }

    private void Start()
    {
        // 텍스트 설정
        text.textWrappingMode = TextWrappingModes.Normal;
        text.overflowMode = TextOverflowModes.ScrollRect;
    }

    private void Update()
    {
        if (!isPanelOpen) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ||
            Input.GetKeyDown(KeyCode.Escape))
        {
            // 창닫기
            ClosePanel();
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
        Display();

        // Narrative Mode 실행
        pc.CurMode = PlayMode.NarrativeMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());

        // Narrative 출력
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
        if (data != null) { 
            text.text = data.narrative;
        }
    }

    // 패널 닫기
    private void ClosePanel() {
        Hide();

        if (blinkCor != null)
            StopCoroutine(blinkCor);

        // 모드 변경
        pc.CurMode = PlayMode.DialogMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());

        // Reply 호출
        EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, false));
    }


    IEnumerator Delay() {
        if (standbyInput) yield break;

        yield return new WaitForSeconds(1f);
        blinkCor = StartCoroutine(blink.BlinkAnnounceMSG(group));
        standbyInput = true;
    }
}
