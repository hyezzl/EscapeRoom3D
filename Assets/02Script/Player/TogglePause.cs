using UnityEngine;

// PauseMode 전체 제어

public class TogglePause : MonoBehaviour
{
    private PlayerController pc;
    private IInputHandler inputHandler;
    private PlayMode preMode;
    [SerializeField] private GameObject pausePopup;

    private void Awake()
    {
        if (!TryGetComponent<PlayerController>(out pc))
        {
            Debug.Log("PauseMode - Failed to Load PlayerController");
        }
        if (!TryGetComponent<IInputHandler>(out inputHandler))
        {
            Debug.Log("PauseMode - Failed to Load IInputHandler");
        }
    }

    private void Update()
    {
        TogglePauseMode();
    }

    public void InOutPauseMode() {
        if (pc.CurMode != PlayMode.PauseMode) // PauseMode 진입
        {
            preMode = pc.CurMode; // 캐싱
            pc.CurMode = PlayMode.PauseMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
            pausePopup.SetActive(true);
        }
        else { // Pause모드 OUT
            pc.CurMode = preMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
            pausePopup.SetActive(false);
        }
    }

    public void TogglePauseMode() {
        if (inputHandler.Escape()) {
            InOutPauseMode();
        }
    }

    // PauseMode Out
    public void ExitPause() {
        if (pc.CurMode == PlayMode.PauseMode) {
            pc.CurMode = preMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
            pausePopup.SetActive(false);
        }
    }
}
