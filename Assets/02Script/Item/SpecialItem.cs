using UnityEngine;
using cakeslice;


[RequireComponent(typeof(Outline))]
public class SpecialItem : MonoBehaviour, IActionItem
{
    public int GetItemID() { return 0; }
    ItemType IActionItem.GetType() => ItemType.Special;
    
    public void InteractOnClick()
    {
        //Dialog
    }

    public void InteractOnE()
    {
        // 이벤트 발생 + 특정분기
        EventBus.Instance.Publish<PuzzleEvents.ApproachSpecial>(new PuzzleEvents.ApproachSpecial(EventList.OpenClockMode));
    }

}
