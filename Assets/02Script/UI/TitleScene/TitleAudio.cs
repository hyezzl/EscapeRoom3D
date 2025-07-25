using UnityEngine;
using UnityEngine.UI;

public class TitleAudio : MonoBehaviour
{
    [SerializeField] private Button muteBTN;
    [SerializeField] private Button playBTN;
    [SerializeField] private AudioSource bgm;
    private bool isMute = false;

    private void Awake()
    {
        muteBTN.onClick.RemoveAllListeners();
        muteBTN.onClick.AddListener(ToggleMute);
        playBTN.onClick.RemoveAllListeners();
        playBTN.onClick.AddListener(ToggleMute);

        muteBTN.enabled = false;
        muteBTN.image.enabled = false;
    }

    private void ToggleMute() {
        if (bgm == null) return;

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
}
