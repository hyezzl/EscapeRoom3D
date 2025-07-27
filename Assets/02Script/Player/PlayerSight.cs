using cakeslice;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    [SerializeField] private CanvasGroup hotspot;
    [SerializeField] private PlayerController pc;
    public List<IActionItem> overlapItems = new();

    private Camera mainCam;

    private void Awake()
    {
        if (TryGetComponent<CapsuleCollider>(out CapsuleCollider col))
        {
            col.isTrigger = true;
            col.radius = 0.1f;
            col.height = 3.5f;
            col.direction = 2; // Z축 방향 배치
            col.center = new Vector3(0, 0, col.height / 2);
        }
        if (TryGetComponent<Rigidbody>(out Rigidbody rig))
        {
            rig.useGravity = false;
            rig.isKinematic = true;
        }
        mainCam = Camera.main;
    }

    private void Start()
    {
        // E 핫스팟
        hotspot.alpha = 0f;
        hotspot.interactable = false;
        hotspot.blocksRaycasts = false;
    }

    // 아이템 획득 시, 해당 아이템 overlapItems에서 제외
    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.GetItem>(ExceptItem);
        EventBus.Instance.Subscribe<GameEvents.GameModeChange>(Init1);
        EventBus.Instance.Subscribe<PuzzleEvents.SolvedPuzzle>(Init2);
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.GetItem>(ExceptItem);
        EventBus.Instance.Unsubscribe<GameEvents.GameModeChange>(Init1);
        EventBus.Instance.Unsubscribe<PuzzleEvents.SolvedPuzzle>(Init2);

        // 비활성화 시 초기화
        overlapItems.Clear();
        ItemManager.CurrentItem = null;
    }

    private void ExceptItem(GameEvents.GetItem evt)
    {
        overlapItems.Remove(evt.item);
    }

    private void Init1(GameEvents.GameModeChange evt)
    {
        InitSight();
        Rearrange();
    }

    private void Init2(PuzzleEvents.SolvedPuzzle evt)
    {
        InitSight();
        Rearrange();
    }

    private void Update()
    {
        if (overlapItems.Count > 0)
        {
            // 매프레임 갱신
            var closest = GetClosestItem();

            // OutLine
            foreach (var item in overlapItems)
            {
                MonoBehaviour it = item as MonoBehaviour; // 안전성
                if (it != null && it.TryGetComponent<Outline>(out Outline outline))
                {
                    // 내가 현재 바라보는 아이템이면 on
                    outline.enabled = (item == closest);
                }
            }

            // CurrentItem 갱신
            if (ItemManager.CurrentItem != closest)
            {
                ItemManager.CurrentItem = closest;
            }

            // 핫스팟 갱신
            if (closest != null &&
                pc.CurMode == PlayMode.InspectMode &&
                (closest.GetType() == ItemType.Readable ||
                 closest.GetType() == ItemType.Interactable))
            {
                hotspot.alpha = 1f;
            }
            else
            {
                hotspot.alpha = 0f;
            }
        }
        else
        {
            ItemManager.CurrentItem = null;
            hotspot.alpha = 0f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // 태그로 1차 분리
        if (other.CompareTag("Item"))
        {
            // 오버랩 리스트 추가
            if (other.TryGetComponent<IActionItem>(out IActionItem item))
            {
                if (!overlapItems.Contains(item))
                {
                    overlapItems.Add(item);
                }
            }
            Debug.Log($"시야 리스트 내 {overlapItems.Count}개");
            Debug.Log($"선정된 아이템 : {GetClosestItem()}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            // 윤곽선 삭제
            if (other.TryGetComponent<Outline>(out Outline outline))
            {
                outline.enabled = false;
            }
            if (other.TryGetComponent<IActionItem>(out IActionItem item))
            {
                overlapItems.Remove(item);
            }
        }
    }

    // 바라보는 오브젝트들 중 가장가까운 오브젝트
    public IActionItem GetClosestItem()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        float minDistance = float.MaxValue;
        IActionItem closestItem = null;

        if (overlapItems.Count == 1)
        {
            return overlapItems[0];
        }
        else if (overlapItems.Count == 0)
        {
            //Debug.Log("PlayerSight - GetClosestItem : 감지된 오브젝트가 없습니다");
            return null;
        }
        else
        {
            Ray ray = mainCam.ScreenPointToRay(center);
            if (Physics.Raycast(ray, out RaycastHit hit, 4f))
            {
                // 레이에 맞은 콜라이더가 IActionItem인지 확인
                IActionItem item = hit.collider.GetComponent<IActionItem>();
                if (item != null && overlapItems.Contains(item))
                {
                    return item;
                }
            }

            // raycast로 감지된게 없다면, 기존 방식
            foreach (var item in overlapItems)
            {
                var converseItem = item as MonoBehaviour;
                if (converseItem != null)
                {
                    Vector3 worldPos = converseItem.transform.position;
                    Vector3 screenPos = mainCam.WorldToScreenPoint(worldPos);

                    float distance = Vector2.Distance(new Vector2(screenPos.x, screenPos.y), center);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestItem = item;
                    }
                }
            }
            return closestItem;
        }
    }

    public void InitSight()
    {
        // 바라보는 아이템 초기화
        overlapItems.Clear();
        ItemManager.CurrentItem = null;

        // 핫스팟 삭제
        hotspot.alpha = 0f;
    }

    public void Rearrange()
    {
        Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 4f))
        {
            var item = hit.collider.GetComponent<IActionItem>();
            if (item != null)
            {
                overlapItems.Add(item); // 강제 등록
            }
        }

        // 있으면,
        if (overlapItems.Count > 0)
        {
            var closest = GetClosestItem();
            ItemManager.CurrentItem = closest;
            foreach (var item in overlapItems)
            {
                MonoBehaviour it = item as MonoBehaviour;
                if (it != null && it.TryGetComponent<Outline>(out Outline outline))
                {
                    outline.enabled = (item == closest);
                }
            }

            // 핫스팟 처리
            if (closest != null &&
                pc.CurMode == PlayMode.InspectMode &&
                (closest.GetType() == ItemType.Readable || closest.GetType() == ItemType.Interactable))
            {
                hotspot.alpha = 1f;
            }
            else
            {
                hotspot.alpha = 0f;
            }
        }
        else
        {
            ItemManager.CurrentItem = null;
            hotspot.alpha = 0f;
        }

    }
}