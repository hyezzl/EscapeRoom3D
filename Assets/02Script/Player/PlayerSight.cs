using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerSight : MonoBehaviour
{
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
            if (!other.TryGetComponent<IActionItem>(out IActionItem item))
                Debug.Log("ItemType 분기 실패!");
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
}
