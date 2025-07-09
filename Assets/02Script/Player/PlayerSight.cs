using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    public List<IActionItem> overlapItems = new();

    private void Awake()
    {
        if (TryGetComponent<CapsuleCollider>(out CapsuleCollider col))
        {
            col.isTrigger = true;
            col.radius = 0.2f;
            col.height = 5f;
            col.direction = 2; // Z축 방향 배치
            col.center = new Vector3(0, 0, col.height / 2);
        }
        if (TryGetComponent<Rigidbody>(out Rigidbody rig))
        {
            rig.useGravity = false;
            rig.isKinematic = true;
        }
    }

    // 아이템 획득 시, 해당 아이템 overlapItems에서 제외
    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.GetItem>(ExceptItem);
    }

    private void OnDisable() { 
        EventBus.Instance.Unsubscribe<GameEvents.GetItem>(ExceptItem);

        // 비활성화 시 초기화
        overlapItems.Clear();
        ItemManager.CurrentItem = null;
    }

    private void ExceptItem(GameEvents.GetItem evt) {
        overlapItems.Remove(evt.item);

        // 재 오버랩
        StartCoroutine(ReOverlap());
    }


    private void OnTriggerEnter(Collider other)
    {
        // 태그로 1차 분리
        if (other.CompareTag("Item"))
        {
            // 윤곽선 표시
            if (other.TryGetComponent<Outline>(out Outline outline))
            {
                outline.enabled = true;
            }

            if (other.TryGetComponent<IActionItem>(out IActionItem item))
            {
                overlapItems.Add(item);
            }

            ItemManager.CurrentItem = GetClosestItem(); // 오버랩

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
            // 오버랩된 값 초기화
            ItemManager.CurrentItem = null;
        }
    }

    // 시야에 가장 가까운 오브젝트 반환
    // X . 시야엔 여러개가 들어와도 정확히 바라보고있지 않으면 레이캐스트의 결과값이 없을수도 있으므로.
    //public IActionItem GetClosestItem2() {
    //    Ray ray = new Ray(transform.position, transform.forward);
    //    if (Physics.Raycast(ray, out RaycastHit hit, 5f)) {
    //        if (hit.collider.TryGetComponent<IActionItem>(out IActionItem item)) {
    //            return item;
    //        }
    //    }
    //    return null;
    //}

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
            foreach (var item in overlapItems)
            {
                var converseItem = item as MonoBehaviour;
                if (converseItem != null)
                {
                    Vector3 worldPos = converseItem.transform.position;
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

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

    private IEnumerator ReOverlap() { 
        yield return null; // destroy 후 1프레임 대기
        ItemManager.CurrentItem = GetClosestItem();
    }

}