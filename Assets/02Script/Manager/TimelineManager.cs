using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector openDoor;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.PlayTimeline>(OnPlayTimeline);
        openDoor.stopped += OnTimelineEnded; // 타임라인 끝나면
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.PlayTimeline>(OnPlayTimeline);
        openDoor.stopped -= OnTimelineEnded;
    }

    private void OnPlayTimeline(GameEvents.PlayTimeline evt) {
        if (evt.timelineID == null) return;

        switch (evt.timelineID) {
            case "T001":
                openDoor.Play();
                break;
        }
    }

    private void OnTimelineEnded(PlayableDirector dir) {
        // 타임라인 끝 (T001)
        EventBus.Instance.Publish<GameEvents.EndTimeline>(new GameEvents.EndTimeline("T001"));
    }


}
