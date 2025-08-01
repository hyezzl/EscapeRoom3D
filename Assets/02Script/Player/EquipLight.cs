using System.Collections;
using UnityEngine;

public class EquipLight : MonoBehaviour
{
    [SerializeField] private Transform cam; // 메인카메라
    [SerializeField] private MeshRenderer mesh; // 본인메쉬

    private Vector3 equipLampPos = new Vector3(0.23f, -0.32f, 0.42f);
    private Vector3 deequipLampPos = new Vector3(0.23f, -0.6f, 0.42f);
    private Coroutine lampCor;
    private Light light;
    private bool isEquip = true; // 임시.

    private void Awake()
    {
        light = GetComponentInChildren<Light>();
        if (light == null) {
            Debug.Log("EquipLight - Failed to Load Light");
        }
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.ChangeLight>(OnChangeLight);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.ChangeLight>(OnChangeLight);
    }

    private void OnChangeLight(GameEvents.ChangeLight evt) {
        // 플레이어캠 -> other
        Debug.Log($"미리보기:{evt.type}");
        if (evt.type != CameraType.PlayerCam)
        {
            mesh.enabled = false; // 안보이도록
            Debug.Log($"전환:{evt.type}");
        }
        else // other -> 플레이어캠 
        {
            mesh.enabled = true;
            Debug.Log($"돌아옴:{evt.type}");
        }
    }


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
