using UnityEngine;

public class BGMManager : Singleton<BGMManager>
{
    [SerializeField] private AudioSource bgm;

    private void Awake()
    {
        if (!TryGetComponent<AudioSource>(out bgm)) {
            Debug.Log("BGMManager - Failed to Load AudioSource");
        }
        bgm.playOnAwake = true;
        bgm.loop = true;
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
}
