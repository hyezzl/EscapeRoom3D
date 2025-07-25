using cakeslice;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OpenableItem : MonoBehaviour, IActionItem
{
    [SerializeField] private float closeZ;
    [SerializeField] private float openZ;
    [SerializeField] private bool isOpen = false;
    private Animator anim;

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
        }
        else {
            Close();
            isOpen = false;
        }
    }
    public void Open() {
        Tween open = transform.DOLocalMoveZ(closeZ, 0.6f).SetEase(Ease.OutCubic);
    }

    public void Close() {
        Tween close = transform.DOLocalMoveZ(openZ, 0.6f).SetEase(Ease.OutCubic);
    }
    
}
