using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchDoor2 : MonoBehaviour
{
    [SerializeField] private Transform leftPivot;
    [SerializeField] private Transform rightPivot;

    private float leftOpenAngle = -110;
    private float rightOpenAngle = 110;
    private float openSpeed = 100f;
    private bool isOpen = false;
    private Quaternion leftCloseRotation;
    private Quaternion rightCloseRotation;
    private Quaternion leftOpenRotation;
    private Quaternion rightOpenRotation;


    private void Awake()
    {
        leftCloseRotation = leftPivot.rotation;
        leftOpenRotation = Quaternion.Euler(leftPivot.eulerAngles + new Vector3(0f, leftOpenAngle, 0f));
        rightCloseRotation = rightPivot.rotation;
        rightOpenRotation = Quaternion.Euler(rightPivot.eulerAngles + new Vector3(0f, rightOpenAngle, 0f));

    }

    private void Update()
    {
        //if (isOpen)
        //{
        //    //leftPivot.rotation = leftOpenRotation;
        //    //rightPivot.rotation = rightOpenRotation;
        //    leftPivot.rotation = Quaternion.Lerp(leftCloseRotation, leftOpenRotation, openSpeed * Time.deltaTime);
        //    rightPivot.rotation = Quaternion.Lerp(rightCloseRotation, rightOpenRotation, openSpeed * Time.deltaTime);
        //}
        //else {
        //    leftPivot.rotation = leftCloseRotation;
        //    rightPivot.rotation = rightCloseRotation;
        //}
    }

    public void ToggleArchDoor2() { 
        isOpen = !isOpen;
    }
}
