using UnityEngine;

public class LighterDrawer : MonoBehaviour
{
    [SerializeField] private GameObject lighter;



    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.DrawerUnlock>(OnOpenDrawer);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.DrawerUnlock>(OnOpenDrawer);
    }

    private void OnOpenDrawer(GameEvents.DrawerUnlock evt) {
        if (lighter != null && evt.drawerID == "D001") { 
            lighter.tag = "Item";
        }
    }
}

