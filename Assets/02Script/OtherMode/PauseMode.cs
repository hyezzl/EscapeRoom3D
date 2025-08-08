using UnityEngine;
using UnityEngine.UI;

public class PauseMode : MonoBehaviour
{
    [SerializeField] private Button resumeBTN;
    [SerializeField] private Button hintBTN;
    [SerializeField] private Button optionBTN;
    [SerializeField] private Button quitBTN;

    private TogglePause tp;

    private void Awake()
    {
        tp = FindAnyObjectByType<TogglePause>();
        if (tp == null) Debug.Log("PauseMode - Failed to Load TogglePause");
    }

    private void OnEnable()
    {
        resumeBTN.onClick.AddListener(() => tp.InOutPauseMode());
    }
    /// <summary>
    /// /////////////////////////////////
    /// </summary>
    private void OnDisable()
    {
        resumeBTN.onClick.RemoveAllListeners();
        //resumeBTN.onClick.RemoveListener(() => tp.InOutPauseMode());
    }


}
