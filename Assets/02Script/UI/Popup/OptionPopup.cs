using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public enum OptionContent
{ 
    GraphicContent = 0,
    AudioContent = 1,
    ControlContent = 2,
    Empty = 3,
}

public class OptionPopup : PanelController
{
    [Header("Tab List")]
    [SerializeField] private List<Button> BTNList;
    [SerializeField] private List<GameObject> contentList;

    [Header("UI Refs")]
    [SerializeField] private Button saveBTN;

    private PlayerController pc;
    private TogglePause tp;

    protected override void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
        if (pc == null) Debug.Log("OptionPopup - Failed to Load PlayerController");

        tp = FindAnyObjectByType<TogglePause>();
        if (tp == null) Debug.Log("OptionPopup - Failed to Load TogglePause");
    }

    private void Start()
    {
        targetPanel.gameObject.SetActive(false);
        ChangeContent((int)OptionContent.GraphicContent);
    }

    private void OnEnable()
    {
        // Tab BTNList
        for (int i = 0; i < 4; i++) {
            int i2 = i;
            BTNList[i2].onClick.AddListener(() => ChangeContent((OptionContent)i2));
        }
        saveBTN.onClick.AddListener(OffPanel);
    }

    private void OnDisable()
    {
        foreach (var BTN in BTNList) {
            BTN.onClick.RemoveAllListeners();
        }
        saveBTN.onClick.RemoveListener(OffPanel);
    }

    private void ChangeContent(OptionContent type) {
        foreach (var content in contentList) { 
            content.gameObject.SetActive(false);
        }
        contentList[(int)type].gameObject.SetActive(true);
    }

    public override void OffPanel() { 
        // 옵션 저장
        EventBus.Instance.Publish<GameEvents.OptionSaved>(new GameEvents.OptionSaved());

        // 패널 닫기 + 초기화 
        base.OffPanel();
        tp.ExitPause();
    }
}
