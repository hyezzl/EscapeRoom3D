using UnityEngine.SceneManagement;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    protected override void DoAwake()
    {
        base.DoAwake();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Title":
                break;
            case "Loading":
                break;
            case "Corrider":
                // 게임의 시작을 알림
                EventBus.Instance.Publish<GameEvents.LoadGameScene>(new GameEvents.LoadGameScene());
                break;
        }
    }
}
