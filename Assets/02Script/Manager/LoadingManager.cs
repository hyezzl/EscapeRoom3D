using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Image loadingBar;

    private void Awake()
    {
        //StartCoroutine()
    }

    IEnumerator LoadScene() {
        yield return new WaitForSeconds(2f);
    }
}
