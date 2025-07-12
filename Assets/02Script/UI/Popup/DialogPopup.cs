using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogPopup : MonoBehaviour
{
    // Interactable(C : Monologue, E : DeactiveMSG)
    // Inspectable (C : Monologue)
    // Readable (C : Monologue / Narrative End : Reply)

    [Header("UI Refs")]
    [SerializeField] private RectTransform targetPanel;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup group;
 
    private bool isDisplay = false;
    private Coroutine curCoroutine;
    private Coroutine blink;
    private Animator anim;
    private bool isTyping = false;
    private bool isSkip = false;
    private bool standbyInput = false;

    private void Awake()
    {
        if (!TryGetComponent<Animator>(out anim)) {
            Debug.Log("DialogPopup - Failed to Load Animator");
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            // Space 혹은 클릭 입력들어오면
            if (isTyping) // 타이핑중
            {
                isSkip = true;
            }
            else if (standbyInput) // 코루틴 끝난 후 입력 대기상태
            {
                // 경고문구 삭제
                StopCoroutine(blink);
                group.alpha = 0f;

                Next();
            }
        }
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.OpenDialogPopup>(OnOpenDialog);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.OpenDialogPopup>(OnOpenDialog);
    }

    public void OnOpenDialog(UIEvents.OpenDialogPopup evt) 
    {
        if (curCoroutine != null) return;
        if (isDisplay) return; // 전 Dialog Popup이 닫히지않으면 진입금지

        isDisplay = true;
        background.enabled = true;
        text.enabled = true;
        anim.SetTrigger("OnOpenDialog");
        
        // 분기
        switch (evt.item.GetType()) {
            case ItemType.Interactable:
                InteractableData intData = ItemDatabaseManager.Instance.GetInteractable(evt.item.GetItemID());
                if (evt.isClick)
                {
                    curCoroutine = StartCoroutine(TypeDialog(intData.monologue));
                }
                else {
                    curCoroutine = StartCoroutine(TypeDialog(intData.deactiveMSG));
                }
                break;

            case ItemType.Inspectable:
                InspectableData insData = ItemDatabaseManager.Instance.GetInspectable(evt.item.GetItemID());
                if (evt.isClick) {
                    curCoroutine = StartCoroutine(TypeDialog(insData.monologue));
                }
                break;

            case ItemType.Readable:
                ReadableData reaData = ItemDatabaseManager.Instance.GetReadable(evt.item.GetItemID());
                if (evt.isClick) {
                    curCoroutine = StartCoroutine(TypeDialog(reaData.monologue));
                }
                //text.text = reaData.reply;
                break;
        };
        // 사용자 입력 대기상태
        standbyInput = true;
        blink = StartCoroutine(BlinkUI(group));
    }

    public void Next() {
        if (curCoroutine != null) return;
        StartCoroutine(OnclosePanel());
    }

    IEnumerator TypeDialog(string sentence) {
        isTyping = true;
        isSkip = false;
        text.text = "";

        foreach (var letter in sentence) {
            if (isSkip) {
                text.text = sentence;
                break;
            }
            text.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        isTyping = false;
        curCoroutine = null;
    }

    IEnumerator OnclosePanel() {
        anim.SetTrigger("OnCloseDialog");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        background.enabled = false;
        text.enabled = false;

        standbyInput = false;
        isDisplay = false;
    }

    IEnumerator BlinkUI(CanvasGroup group)
    {
        yield return new WaitForSeconds(1f);
        float t = 0f;
        bool fadeIn = true;
        while (true)
        {
            t += Time.deltaTime;
            group.alpha = fadeIn ? Mathf.Lerp(0f, 0.8f, t) : Mathf.Lerp(0.8f, 0f, t);
            if (t >= 1f)
            {
                t = 0f;
                fadeIn = !fadeIn;
            }
            yield return null;
        }
    }
}
