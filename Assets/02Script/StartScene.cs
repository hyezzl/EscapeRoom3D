using System.Collections;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private DialogPopup dialog;
    [SerializeField] private TutorialPopup tutorial;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.LoadGameScene>(OnLoadGame);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.LoadGameScene>(OnLoadGame);
    }

    private void OnLoadGame(GameEvents.LoadGameScene evt) {
        // 덜컥 문이 닫히는 소리

        StartCoroutine(PlayInOrder());
        //EventBus.Instance.Publish<UIEvents.EventAfter>(new UIEvents.EventAfter("E001"));


    }

    IEnumerator PlayInOrder() { 
        yield return null;

        EventBus.Instance.Publish<UIEvents.EventAfter>(new UIEvents.EventAfter("E001"));

        // 다이얼로그 끝나면!
        yield return new WaitWhile(() => dialog.isDisplay);

        EventBus.Instance.Publish<UIEvents.StartTutorial>(new UIEvents.StartTutorial());

        // 튜토리얼 끝나면!
        yield return new WaitWhile(() => tutorial.isPanelOpen);

        EventBus.Instance.Publish<UIEvents.EventAfter>(new UIEvents.EventAfter("E002"));
    }
}
