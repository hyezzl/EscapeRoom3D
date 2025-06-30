using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchDoor : MonoBehaviour
{
    [SerializeField] private Transform leftPivot;
    [SerializeField] private Transform rightPivot;
    private bool isOpen = false;
    private Animator anim;

    private void Awake()
    {
        if (!TryGetComponent<Animator>(out anim)) {
            Debug.Log("ArchDoor - Failed to Load Animator");
        }
    }

    public void OpenArchDoor() {
        anim.SetTrigger("ArchDoorOpen");
    }

    public void CloseArchDoor() {
        anim.SetTrigger("ArchDoorClose");
    }
}
