using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum OptionContent
{ 
    GraphicContent = 0,
    AudioContent = 1,
    ControlContent = 2,
    Empty = 3,
}

public class OptionPopup : MonoBehaviour
{
    [SerializeField] private List<Button> BTNList;
    [SerializeField] private List<GameObject> contentList;

    private void Awake()
    {
        ChangeContent((int)OptionContent.GraphicContent);
    }

    private void OnEnable()
    {
        for (int i = 0; i < 4; i++) {
            int i2 = i;
            BTNList[i2].onClick.AddListener(() => ChangeContent((OptionContent)i2));
        }
    }
    /// <summary>
    /// ////////////////////////////////////
    /// </summary>
    private void OnDisable()
    {
        foreach (var BTN in BTNList) {
            BTN.onClick.RemoveAllListeners();
        }
        //for (int i = 0; i < 4; i++)
        //{
        //    BTNList[i].onClick.RemoveListener(() => ChangeContent((OptionContent)i));
        //}
        //BTN.onClick.RemoveListener(() => ChangeContent((OptionContent)i));
    }

    private void ChangeContent(OptionContent type) {
        foreach (var content in contentList) { 
            content.gameObject.SetActive(false);
        }
        contentList[(int)type].gameObject.SetActive(true);
    }
}
