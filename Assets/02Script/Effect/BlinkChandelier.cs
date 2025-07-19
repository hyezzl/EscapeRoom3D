using System.Collections;
using UnityEngine;

public class BlinkChandelier : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private Transform player;
    private float minDistance = 20f;
    private float medieteIntensity;
    private Coroutine blink = null;
    private bool isBlink = false;

    private void Start()
    {
        medieteIntensity = light.intensity;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        //Debug.Log($"거리 : {distance}");
        if (distance < minDistance)
        {
            if (!isBlink) // 코루틴 아직 시작 전
            {
                blink = StartCoroutine(SometimeBlink());
                isBlink = true;
            }
        }
        else // 플레이어와 멀어지면,
        {
            if (isBlink) { 
                StopCoroutine(blink);
                isBlink = false;
            }
            light.intensity = medieteIntensity;
        }
    }

    IEnumerator SometimeBlink() {

        while (true) {
            float t = 2f;
            float seconds = 0;

            yield return new WaitForSeconds(2f);

            while (t > seconds) { 
                seconds += Time.deltaTime;
                light.intensity = medieteIntensity * Random.Range(0.3f, 1.0f);
                yield return null;
            }
            light.intensity = medieteIntensity;

            float randomSec = Random.Range(8f, 12f);
            yield return new WaitForSeconds(randomSec);
        }
    }
}
