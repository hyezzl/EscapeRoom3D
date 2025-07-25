using UnityEngine;

public class ItemDatabaseManager : Singleton<ItemDatabaseManager> 
{
    private PickableLoader pickableLoader;
    private InteractableLoader interactableLoader;
    private InspectableLoader inspectorLoader;
    private ReadableLoader readableLoader;
    private SpecialLoader specialLoader;

    private EventAfterLoader eventafterLoader;


    public PickableData GetPickable(int itemID) => pickableLoader.Get(itemID);
    public InteractableData GetInteractable(int itemID) => interactableLoader.Get(itemID);
    public InspectableData GetInspectable(int itemID) => inspectorLoader.Get(itemID);
    public ReadableData GetReadable(int itemID) => readableLoader.Get(itemID);

    public SpecialData GetSpecial(int itemID) => specialLoader.Get(itemID);

    public EventAfterData GetEventAfter(string eventID) => eventafterLoader.Get(eventID);

    protected override void DoAwake()
    {
        base.DoAwake();
        LoadTable();
    }

    private void LoadTable() {
        ItemTable dataTable = Resources.Load<ItemTable>("Table/ItemTable");
        EventAfter eventafterTable = Resources.Load<EventAfter>("Table/EventAfter");

        pickableLoader = new PickableLoader(dataTable.Pickable);
        interactableLoader = new InteractableLoader(dataTable.Interactable);
        inspectorLoader = new InspectableLoader(dataTable.Inspectable);
        readableLoader = new ReadableLoader(dataTable.Readable);
        specialLoader = new SpecialLoader(dataTable.Special);

        eventafterLoader = new EventAfterLoader(eventafterTable.eventAfter);
    }
}
