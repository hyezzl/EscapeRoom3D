using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnnounce : MonoBehaviour
{
    [SerializeField] private CanvasGroup group;

    private void Start()
    {
        StartCoroutine(BlinkUI(group));
    }

    IEnumerator BlinkUI(CanvasGroup group) {
        float t = 0f;
        bool fadeIn = true;
        while (true) { 
            t += Time.deltaTime;
            group.alpha = fadeIn ? Mathf.Lerp(0f, 0.8f, t) : Mathf.Lerp(0.8f, 0f, t);
            if (t >= 1f) {
                t = 0f;
                fadeIn = !fadeIn;
            }
            yield return null;
        }
    }
}
