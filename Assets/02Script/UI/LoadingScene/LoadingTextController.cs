using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadingTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private string address = "LoadingTexts"; // SO객체 addressable 주소

    private List<LoadingTextsEntity> loadingTexts;

    private void Start()
    {
        Addressables.LoadAssetAsync<LoadingTexts>(address).Completed += OnLoad;
    }

    private void OnLoad(AsyncOperationHandle<LoadingTexts> handle) {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            LoadingTexts data = handle.Result;
            loadingTexts = data.loadingTexts;

            // 텍스트 표기
            StartCoroutine(PlayLoadingTexts());
        }
        else {
            Debug.Log("LoadingScene : LoadingTextController - Failed to Load LoadingTexts");
        }
    }

    IEnumerator PlayLoadingTexts() { 
        yield return null;

        int cnt = loadingTexts.Count;
        List<int> idxs = Enumerable.Range(0,cnt).ToList();

        while (idxs.Count > 0) {
            int randomIdx = Random.Range(0, idxs.Count);
            
            // 텍스트 출력
            text.text = loadingTexts[idxs[randomIdx]].LoadingTexts;

            // DoTween
            Color endColor = text.color;
            endColor.a = 0f;
            text.color = endColor;

            // 0.6초동안 페이드인
            yield return text.DOFade(1f, 0.8f).WaitForCompletion();
            // 1.2초동안  Play
            yield return new WaitForSeconds(1.8f);
            // 0.6초동안 페이드아웃
            yield return text.DOFade(0f, 0.8f).WaitForCompletion();

            // 본 텍스트 삭제
            idxs.RemoveAt(randomIdx);
        }
        // 재귀
        StartCoroutine(PlayLoadingTexts());
    }


}
