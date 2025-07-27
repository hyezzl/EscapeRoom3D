using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogPopup : MonoBehaviour
{
    // Interactable(C : Monologue, E : DeactiveMSG)
    // Inspectable (C : Monologue)
    // Readable (C : Monologue / Narrative End : Reply)

    [Header("UI Refs")]
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup group;

    Tweener typing;
    Sequence seq;
    private string sentence;
    private float typingSpeed = 0.05f;
    private string imopenableMSG = "열리지 않는다.";

    private PlayerController pc;
    private Coroutine blinkCor;
    private Animator anim;
    private BlinkAnnounce blink;
    public bool isDisplay = false;
    private bool isTyping = false;
    private bool standbyInput = false;

    private void Awake()
    {
        if (!TryGetComponent<Animator>(out anim))
        {
            Debug.Log("DialogPopup - Failed to Load Animator");
        }
        if (!TryGetComponent<BlinkAnnounce>(out blink))
        {
            Debug.Log("DialogPopup - Failed to Load BlinkAnnounce");
        }
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null)
            Debug.Log("NarrativePopup - Failed to Load PlayerController");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            // Space 혹은 클릭 입력들어오면
            if (isTyping) // 타이핑중
            {
                SkipDialog();
            }
            else if (standbyInput) // 타이핑 끝난 후 입력 대기상태
            {
                // 경고문구 삭제
                if(blinkCor != null) StopCoroutine(blinkCor);
                group.alpha = 0f;

                StartCoroutine(OnclosePanel());
            }
        }
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.OpenDialogPopup>(OnOpenDialog);
        EventBus.Instance.Subscribe<UIEvents.EventAfter>(EventAfterPlay);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.OpenDialogPopup>(OnOpenDialog);
        EventBus.Instance.Unsubscribe<UIEvents.EventAfter>(EventAfterPlay);
    }

    public void OnOpenDialog(UIEvents.OpenDialogPopup evt)
    {
        if (isDisplay) return; // 전 Dialog Popup이 닫히지않으면 진입금지
        if (evt.item == null) return; 

        // 모드변경
        pc.CurMode = PlayMode.DialogMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());

        isDisplay = true;
        background.enabled = true;
        text.enabled = true;
        anim.SetTrigger("OnOpenDialog");

        // 분기
        switch (evt.item.GetType())
        {
            case ItemType.Interactable:
                InteractableData intData = ItemDatabaseManager.Instance.GetInteractable(evt.item.GetItemID());
                if (evt.isClick)
                {
                    TypeDialog(intData.monologue);
                }
                else
                {
                    TypeDialog(intData.deactiveMSG);
                }
                break;

            case ItemType.Inspectable:
                InspectableData insData = ItemDatabaseManager.Instance.GetInspectable(evt.item.GetItemID());
                if (evt.isClick)
                {
                    TypeDialog(insData.monologue);
                }
                break;

            case ItemType.Readable:
                ReadableData reaData = ItemDatabaseManager.Instance.GetReadable(evt.item.GetItemID());
                if (evt.isClick)
                {
                    TypeDialog(reaData.monologue);
                }
                else //  Reply
                {
                    TypeDialog(reaData.reply);
                }
                break;

            case ItemType.Special:
                SpecialData speData = ItemDatabaseManager.Instance.GetSpecial(evt.item.GetItemID());
                if (evt.isClick) {
                    TypeDialog(speData.monologue);
                }
                break;

            case ItemType.Imopenable:
                TypeDialog(imopenableMSG);
                break;
        };
    }

    private void EventAfterPlay(UIEvents.EventAfter evt) {
        if (isDisplay) return; // 전 Dialog Popup이 닫히지않으면 진입금지

        // 모드변경
        pc.CurMode = PlayMode.DialogMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());

        isDisplay = true;
        background.enabled = true;
        text.enabled = true;
        anim.SetTrigger("OnOpenDialog");

        EventAfterData diaData = ItemDatabaseManager.Instance.GetEventAfter(evt.eventID);

        TypeDialog(diaData.dialog);
    }



    private void TypeDialog(string sentence) {
        if (typing != null && typing.IsActive()) typing.Kill();
        if (seq != null && seq.IsActive()) seq.Kill();
        if (blinkCor != null) StopCoroutine(blinkCor);

        this.sentence = sentence; // 캐싱
        float duration = sentence.Length * typingSpeed;
        isTyping = true;
        seq = DOTween.Sequence();
        typing = text.DOText(sentence, duration).SetEase(Ease.Linear);

        seq.Append(typing)
            .AppendCallback(() =>
            {
                isTyping = false;
                standbyInput = true;
                blinkCor = StartCoroutine(blink.BlinkAnnounceMSG(group));
            });
    }

    // 스킵 시 바로 출력
    private void SkipDialog() {
        if (isTyping && seq.IsActive()) seq.Kill();
        text.text = sentence;
        
        isTyping = false;
        standbyInput = true;
        if (blinkCor != null) StopCoroutine(blinkCor);
        blinkCor = StartCoroutine(blink.BlinkAnnounceMSG(group));
    }

    // 패널 닫기
    IEnumerator OnclosePanel()
    {
        anim.SetTrigger("OnCloseDialog");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        //모드 변경
        pc.CurMode = PlayMode.InspectMode;
        EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());

        // 값 초기화
        if (blinkCor != null) StopCoroutine(blinkCor);
        if (typing != null && typing.IsActive()) typing.Kill();
        if (seq != null && seq.IsActive()) seq.Kill();
        text.text = "";

        background.enabled = false;
        text.enabled = false;

        standbyInput = false;
        isDisplay = false;
    }
}
