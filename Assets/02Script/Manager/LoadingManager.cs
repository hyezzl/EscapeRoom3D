using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> dots;
    private Texture2D cursor2D;

    private void Awake()
    {
        cursor2D = Resources.Load<Texture2D>($"Cursor/Cursor1");
        if (cursor2D == null) Debug.Log("Failed to Load Cursor Image");
        StartCoroutine(dotsEnable());
        StartCoroutine(LoadAsyncScene());
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator dotsEnable() {
        while (true) { 
            for (int i = 0; i < 4; i++)
            {
                dots[i].enabled = true;
                yield return new WaitForSeconds(0.3f);
            }
            // 4개 모두 출력 후 초기화
            foreach (var dot in dots) { 
                dot.enabled = false;
            }
        }
    }

    IEnumerator LoadAsyncScene() {

        yield return null;

        AsyncOperation async = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("NextScene"));
        async.allowSceneActivation = false;
        float exitTime = 0f;

        while (!async.isDone) {
            yield return null;  
            exitTime += Time.deltaTime;

            if (async.progress >= 0.9f && exitTime >= 6f) // 로딩이 완료 + 5초 대기 끝
            {
                async.allowSceneActivation = true;
            }
        }
    }
}
