using UnityEngine;

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
        if (pc.CurMode != PlayMode.PauseMode)
        {
            preMode = pc.CurMode; // Ä³½Ì
            pc.CurMode = PlayMode.PauseMode;
            EventBus.Instance.Publish<GameEvents.GameModeChange>(new GameEvents.GameModeChange());
            pausePopup.SetActive(true);
        }
        else {
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
}
