using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipLight : MonoBehaviour
{
    private Vector3 equipLampPos = new Vector3(0.97f, -1.33f, 1.71f);
    private Vector3 deequipLampPos = new Vector3(0.97f, -3f, 1.71f);
    private bool isEquip = false;

    public void PickupLamp() {
        if (!isEquip) {
            transform.localPosition = Vector3.Lerp(deequipLampPos, equipLampPos, 3f * Time.deltaTime);
            isEquip = true;
        }
    }

    public void PutDownLamp() {
        if (isEquip) {
            transform.localPosition = Vector3.Lerp(equipLampPos, deequipLampPos, 3f * Time.deltaTime);
            isEquip = false;
        }
    }
}
