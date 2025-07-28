using cakeslice;
using DG.Tweening;
using UnityEngine;

public enum OpenableType
{ 
    basic,
    special, // 오브젝트가 안에 존재하는 drawer
}

[RequireComponent(typeof(Outline))]
public class OpenableItem : MonoBehaviour, IActionItem
{
    [SerializeField] private float closeZ;
    [SerializeField] private float openZ;
    [SerializeField] private OpenableType type;
    public bool isOpen = false;

    private bool firstOpen = true; // 처음열릴 때, 이벤트 한번 발생 (보류)

    ItemType IActionItem.GetType() => ItemType.Openable;

    public int GetItemID() { return 0; }

    private void Start()
    {
        if (TryGetComponent<Outline>(out Outline outline))
        {
            outline.color = 2;
        }
        // 위치 초기화
        Vector3 pos = transform.localPosition;
        pos.z = closeZ;
        transform.localPosition = pos;
    }


    public void InteractOnClick() {
        Toggle();
    }
    public void InteractOnE() {
        Toggle();
    }

    public void Toggle() {
        if (!isOpen)
        {
            Open();
            isOpen = true;
            if (type == OpenableType.special && firstOpen) {
                EventBus.Instance.Publish<GameEvents.DrawerUnlock>(new GameEvents.DrawerUnlock());
                Debug.Log("처음열림!!!");
            }
            firstOpen = false;
        }
        else {
            Close();
            isOpen = false;
        }
    }
    public void Open() {
        Tween open = transform.DOLocalMoveZ(openZ, 0.6f).SetEase(Ease.OutCubic);
    }

    public void Close() {
        Tween close = transform.DOLocalMoveZ(closeZ, 0.6f).SetEase(Ease.OutCubic);
    }
    
}
