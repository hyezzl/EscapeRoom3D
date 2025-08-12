using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SoundType
{ 
    Master,
    BGM,
    SFX,
}

public class AudioController : MonoBehaviour
{
    [Header("AudioMixer")]
    [SerializeField] private AudioMixer am;

    [Header("UI Refs")]
    [SerializeField] private List<Slider> sliders;
    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<Image> images;

    private List<bool> isPlaying = new List<bool>{true, true, true};
    private List<float> preVol = new List<float> { 1f, 1f, 1f };

    [Header("Sprite")]
    [SerializeField] private Sprite playIcon;
    [SerializeField] private Sprite muteIcon;


    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            int idx = i;
            sliders[idx].onValueChanged.AddListener(value => SliderValueChanged((SoundType)idx, value));
            buttons[idx].onClick.AddListener(() => SoundMute((SoundType)idx));
        }
    }

    private void OnDisable()
    {
        foreach (var slider in sliders) {
            slider.onValueChanged.RemoveAllListeners();
        }
        foreach (var button in buttons) { 
            button.onClick.RemoveAllListeners();
        }
    }

    public void SliderValueChanged(SoundType type, float value) {
        // 볼륨 텍스트 표시
        int percent = Mathf.RoundToInt(value * 100);
        texts[(int)type].text = percent + " %";

        // AudioMixer 볼륨 조절
        float db; // -80 ~ 0

        if (value == 0) { 
            // 음소거
            images[(int)type].sprite = muteIcon;
            db = -80f;
            isPlaying[(int)type] = false;
        }
        else {
            images[(int)type].sprite = playIcon;
            db = Mathf.Log10(value) * 20f;
        }
        string parameter = type.ToString() + "Vol";
        am.SetFloat(parameter, db);
    }

    public void SoundMute(SoundType type) {

        // 토글
        isPlaying[(int)type] = !isPlaying[(int)type];

        if (isPlaying[(int)type])
        {
            // 재생 (뮤트 해제)
            sliders[(int)type].SetValueWithoutNotify(preVol[(int)type]);
            images[(int)type].sprite = playIcon;

            SliderValueChanged(type, preVol[(int)type]);
        }
        else {
            // 뮤트
            preVol[(int)type] = sliders[(int)type].value; // 캐싱

            sliders[(int)type].SetValueWithoutNotify(0f); // 슬라이더값 강제변경 (onvalueChanged 이벤트 호출X)
            images[(int)type].sprite = muteIcon;
            texts[(int)type].text = "0 %";
            am.SetFloat(type.ToString() + "Vol", -80f);
        }
    }
}
