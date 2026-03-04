using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class NewLoadingTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private List<LoadingTextsEntity> loadingTexts;

    private void Start()
    {
        // JSON 파일 로드
        TextAsset jsonText = Resources.Load<TextAsset>("LoadingTexts");  // Resources/LoadingTexts.json
        if (jsonText != null)
        {
            LoadingTexts data = JsonUtility.FromJson<LoadingTexts>(jsonText.text);
            loadingTexts = data.loadingTexts;
            StartCoroutine(PlayLoadingTexts());
        }
        else
        {
            Debug.LogError("LoadingScene: Failed to Load LoadingTexts.json");
        }
    }

    IEnumerator PlayLoadingTexts()
    {
        yield return null;

        int cnt = loadingTexts.Count;
        List<int> idxs = Enumerable.Range(0, cnt).ToList();

        while (idxs.Count > 0)
        {
            int randomIdx = Random.Range(0, idxs.Count);

            // 텍스트 출력
            text.text = loadingTexts[idxs[randomIdx]].LoadingTexts;

            // DoTween 페이드 애니메이션 (기존 그대로)
            Color endColor = text.color;
            endColor.a = 0f;
            text.color = endColor;

            yield return text.DOFade(1f, 0.8f).WaitForCompletion();
            yield return new WaitForSeconds(1.8f);
            yield return text.DOFade(0f, 0.8f).WaitForCompletion();

            idxs.RemoveAt(randomIdx);
        }
        // 무한 재생
        StartCoroutine(PlayLoadingTexts());
    }
}