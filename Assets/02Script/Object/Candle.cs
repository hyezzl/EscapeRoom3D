using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] private GameObject child;
    [SerializeField] private int uniqueNum = 0;
    private CandleController cc;
    private InteractableItem item;
    private MeshRenderer fire;
    private bool isOn = false;


    private void Awake()
    {
        if (!TryGetComponent<InteractableItem>(out item)) {
            Debug.Log("Candle - Failed to Load InteractableItem");
        }
        if (child != null)
            fire = child.GetComponent<MeshRenderer>();
        cc = GetComponentInParent<CandleController>();
        if (cc == null) Debug.Log("Candle - Failed to Load CandleController");
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<PuzzleEvents.DoInteract>(OnInteract);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<PuzzleEvents.DoInteract>(OnInteract);
    }
    private void OnInteract(PuzzleEvents.DoInteract evt) {
        if (evt.pairID == item.GetPairID()) // pairID가 같으면
        {
            // 불 토글 (상호작용하는 촛불만)
            if (fire != null && evt.target == this.gameObject) 
            {
                //fire.enabled = !fire.enabled;
                if (!isOn)
                {
                    fire.enabled = true;
                    isOn = true;
                    cc.Set(uniqueNum, true);
                    cc.CheckAnswer(); // 토글할때마다 답인지 체크
                }
                else { 
                    fire.enabled = false;
                    isOn = false;
                    cc.Set(uniqueNum, false);
                    cc.CheckAnswer();
                }
            }
        }
    }
}
