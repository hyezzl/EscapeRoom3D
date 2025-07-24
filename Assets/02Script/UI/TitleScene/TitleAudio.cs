using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class TitleAudio : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private string address = "BGM_Title";
    [SerializeField] private Button muteBTN;
    [SerializeField] private Button playBTN;
    private AudioClip clip;
    private bool isMute = false;

    private void Awake()
    {
        //bgm.playOnAwake = true;
        //bgm.loop = true;
        muteBTN.onClick.RemoveAllListeners();
        muteBTN.onClick.AddListener(ToggleMute);
        playBTN.onClick.RemoveAllListeners();
        playBTN.onClick.AddListener(ToggleMute);

        muteBTN.enabled = false;
        muteBTN.image.enabled = false;

        StartCoroutine(LoadClip());
    }

    private void Start()
    {
        BGMManager.Instance.PlayBGM(clip, 1.0f);
    }

    private void ToggleMute() {
        if (!isMute)
        {
            bgm.mute = true;
            isMute = true;

            muteBTN.enabled = true;
            muteBTN.image.enabled = true;

            playBTN.enabled = false;
            playBTN.image.enabled = false;
        }
        else {
            bgm.mute = false;
            isMute = false;

            playBTN.enabled = true;
            playBTN.image.enabled = true;

            muteBTN.enabled = false;
            muteBTN.image.enabled = false;
        }
    }

    IEnumerator LoadClip() {
        AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(address);
        yield return handle;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            clip = handle.Result;
        }
        else {
            Debug.Log("TitleAudio - Failed to Load AudioClip");
        }

    }
}
