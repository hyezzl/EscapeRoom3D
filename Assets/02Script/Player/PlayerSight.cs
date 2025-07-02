using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    private void Awake()
    {
        if(TryGetComponent<CapsuleCollider>(out CapsuleCollider col)){
            col.isTrigger = true;
            col.radius = 0.1f;
            col.height = 4f;
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
        if (other.CompareTag("Interactive")) { 
            
        }
    }
}
