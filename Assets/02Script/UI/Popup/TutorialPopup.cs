using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopup : PanelController
{
    [Header("UI Refs")]
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private CanvasGroup group; // Announce
    [SerializeField] private RectTransform content;
    [SerializeField] private float scrollSpeed = 20f;
    [SerializeField] private Button exitBTN;

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

        scroll.scrollSensitivity = scrollSpeed;
    }


    private void Update()
    {
        if (!isPanelOpen) return;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            // 창닫기
            ClosePanel();
        }
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.StartTutorial>(OnTutorial);
        scroll.onValueChanged.AddListener(OnScrollChanged);
        exitBTN.onClick.AddListener(ClosePanel);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.StartTutorial>(OnTutorial);
        scroll.onValueChanged.RemoveListener(OnScrollChanged);
        exitBTN.onClick.RemoveListener(ClosePanel);
    }

    private void OnTutorial(UIEvents.StartTutorial evt) {
        Display();

        // Narrative Mode 실행
        pc.CurMode = PlayMode.NarrativeMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
    }

    // 스크롤 변화값 반영
    private void OnScrollChanged(Vector2 pos)
    {
        if (scroll.verticalNormalizedPosition <= 0.01f && !standbyInput)
        {
            StartCoroutine(Delay());
        }
    }

    // 패널 닫기
    private void ClosePanel()
    {
        Hide();

        if (blinkCor != null)
            StopCoroutine(blinkCor);

        // 모드 변경
        pc.CurMode = PlayMode.InspectMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());

    }
    
    IEnumerator Delay()
    {
        if (standbyInput) yield break;

        yield return new WaitForSeconds(1f);
        blinkCor = StartCoroutine(blink.BlinkAnnounceMSG(group));
        standbyInput = true;
    }
}
