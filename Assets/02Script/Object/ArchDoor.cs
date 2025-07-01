using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArchDoor : MonoBehaviour
{
    [SerializeField] private Transform leftPivot;
    [SerializeField] private Transform rightPivot;
    private bool isArchOpen = false;
    private Animator anim;

    private void Awake()
    {
        if (!TryGetComponent<Animator>(out anim)) {
            Debug.Log("ArchDoor - Failed to Load Animator");
        }
    }

    public void OpenArchDoor() {
        //if(!isArchOpen)
            anim.SetTrigger("ArchDoorOpen");
    }

    public void CloseArchDoor() {
        //if (isArchOpen)
            anim.SetTrigger("ArchDoorClose");
    }
}
