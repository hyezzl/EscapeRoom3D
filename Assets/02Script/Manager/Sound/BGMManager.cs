using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class BGMManager : Singleton<BGMManager>
{
    [SerializeField] public AudioSource bgm;

    private AudioClip bgm_Main;
    private AudioClip bgm_Title;

    protected override void DoAwake()
    {
        if (!TryGetComponent<AudioSource>(out bgm)) {
            Debug.Log("BGMManager - Failed to Load AudioSource");
        }
        bgm.playOnAwake = true;
        bgm.loop = true;
    }

    private async Task Start()
    {
        AsyncOperationHandle<AudioClip> handleM = Addressables.LoadAssetAsync<AudioClip>("BGM_Main");
        AsyncOperationHandle<AudioClip> handleT = Addressables.LoadAssetAsync<AudioClip>("BGM_Title");

        // 비동기작업 완료 대기
        await handleM.Task;
        await handleT.Task;

        LoadClip(ref bgm_Main, handleM);
        if (bgm_Main == null) Debug.Log("BGMManager - Failed to Load MainBGM");

        LoadClip(ref bgm_Title, handleT);
        if (bgm_Title == null) Debug.Log("BGMManager - Failed to Load TitleBGM");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 각 씬마다 다른 오디오클립 설정
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "TitleScene" && bgm_Title != null) { 
        }
        switch (scene.name) {
            case "Title":
                PlayBGM(bgm_Title, 1.0f);
                break;
            case "Loading":
                SetBGMVolume(0.4f);
                break;
            case "Corrider":
                PlayBGM(bgm_Main, 1.0f);
                break;
        }
    }

    public void PlayBGM(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null) return;
        // 클립 변경 후 재생
        if (bgm.clip != clip)
        {
            bgm.clip = clip;
        }
        bgm.volume = volume;
        if (!bgm.isPlaying)
        {
            bgm.Play();
            bgm.loop = true;
        }
    }   

    public void SetBGMVolume(float ratio) { 
        bgm.volume = ratio;
    }

    private void LoadClip(ref AudioClip clip, AsyncOperationHandle handle) {

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            clip = handle.Result as AudioClip;
        }
        else {
            Debug.Log("BGMManager - Failed to Load Addressable AudioClip");
        }
    }
}
