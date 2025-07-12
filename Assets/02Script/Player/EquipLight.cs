using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipLight : MonoBehaviour
{
    [SerializeField] private Transform cam; // 메인카메라
    private Vector3 equipLampPos = new Vector3(0.97f, -1.33f, 1.71f);
    private Vector3 deequipLampPos = new Vector3(0.97f, -3f, 1.71f);
    private Coroutine lampCor;
    private Light light;
    private bool isEquip = true; // 임시.

    //Temp
    private Vector3 lampOffset = new Vector3(0.23f, -0.32f, 0.42f);
    private Vector3 lastCamPos;
    [SerializeField] private float shakeRatio = 0.1f;

    private void Awake()
    {
        light = GetComponentInChildren<Light>();
        if (light == null) {
            Debug.Log("EquipLight - Failed to Load Light");
        }
    }

    //private void Start()
    //{
    //    transform.localPosition = lampOffset;
    //    lastCamPos = cam.position;
    //}

    //private void LateUpdate()
    //{
    //    Vector3 noiseDelta = cam.position - lastCamPos;
    //    transform.localPosition = lampOffset + cam.InverseTransformDirection(noiseDelta) * shakeRatio;
    //    lastCamPos = cam.position;
    //}

    public void PickupLamp() {
        if (!isEquip) {
            if (lampCor != null) { 
                StopCoroutine(lampCor);
            } 
            lampCor = StartCoroutine(MoveLamp(deequipLampPos, equipLampPos));
            isEquip = true;
            light.intensity = 2f;
        }
    }

    public void PutDownLamp() {
        if (isEquip) {
            if (lampCor != null) { 
                StopCoroutine (lampCor);
            }
            lampCor = StartCoroutine(MoveLamp(equipLampPos, deequipLampPos));
            isEquip = false;
            light.intensity = 0f;
        }
    }

    private IEnumerator MoveLamp(Vector3 start, Vector3 end) {
        float t = 0;
        float duration = 0.4f;
        while (t < 1f) {
            t += Time.deltaTime / duration;
            transform.localPosition = Vector3.Lerp(start, end, t);
            yield return null;
        }
        transform.localPosition = end;
    }
}
