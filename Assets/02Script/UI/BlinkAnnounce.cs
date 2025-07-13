using System.Collections;
using UnityEngine;

public class BlinkAnnounce : MonoBehaviour
{

    public IEnumerator BlinkAnnounceMSG(CanvasGroup group) {
        yield return new WaitForSeconds(0.5f);
        float t = 0f;
        bool fadeIn = true;
        while (true)
        {
            t += Time.deltaTime * 0.9f;
            group.alpha = fadeIn ? Mathf.Lerp(0f, 0.5f, t) : Mathf.Lerp(0.5f, 0f, t);
            if (t >= 1f)
            {
                t = 0f;
                fadeIn = !fadeIn;
            }
            yield return null;
        }
    }
}
