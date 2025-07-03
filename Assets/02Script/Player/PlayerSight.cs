using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerSight : MonoBehaviour
{
    public List<IActionItem> overlapItems = new();

    private void Awake()
    {
        if(TryGetComponent<CapsuleCollider>(out CapsuleCollider col)){
            col.isTrigger = true;
            col.radius = 0.2f;
            col.height = 5f;
            col.direction = 2; // Z축 방향 배치
            col.center = new Vector3(0, 0, col.height / 2);
        }
        if (TryGetComponent<Rigidbody>(out Rigidbody rig)) {
            rig.useGravity = false;
            rig.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 태그로 1차 분리
        if (other.CompareTag("Item")) {
            // 윤곽선 표시
            if (other.TryGetComponent<Outline>(out Outline outline)) {
                outline.enabled = true;
            }

            // itemType 분기
            if (other.TryGetComponent<IActionItem>(out IActionItem item)) { 
                overlapItems.Add(item);
            }

            switch (item.GetType())
            {
                case ItemType.Pickable:
                    Debug.Log("Pickable");
                    // 오버랩
                    break;

                case ItemType.Interactable:
                    Debug.Log("Interactable");
                    break;

                case ItemType.Readable:
                    Debug.Log("Readable");
                    break;

                case ItemType.Openable:
                    Debug.Log("Openable");
                    break;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item")) {
            // 윤곽선 삭제
            if (other.TryGetComponent<Outline>(out Outline outline))
            {
                outline.enabled = false;
            }

            // 오버랩된 값 -1로 되돌리기
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

    //public IActionItem GetClosestItem() { 
    
    //}
}
