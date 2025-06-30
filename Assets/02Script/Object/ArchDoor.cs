using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchDoor : MonoBehaviour
{
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;

    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool isOpen = false;
}
