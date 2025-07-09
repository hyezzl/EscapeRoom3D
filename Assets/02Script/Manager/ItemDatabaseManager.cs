using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabaseManager : Singleton<ItemDatabaseManager> 
{
    private PickableLoader pickableLoader;
    private InteractableLoader interactableLoader;
    private InspectableLoader inspectorLoader;
    private ReadableLoader readableLoader;


    public PickableData GetPickable(int itemID) => pickableLoader.Get(itemID);
    public InteractableData GetInteractable(int itemID) => interactableLoader.Get(itemID);
    public InspectableData GetInspectable(int itemID) => inspectorLoader.Get(itemID);
    public ReadableData GetReadable(int itemID) => readableLoader.Get(itemID);

    protected override void DoAwake()
    {
        base.DoAwake();
        LoadTable();
    }

    private void LoadTable() {
        ItemTable dataTable = Resources.Load<ItemTable>("Table/ItemTable");

        pickableLoader = new PickableLoader(dataTable.Pickable);
        interactableLoader = new InteractableLoader(dataTable.Interactable);
        inspectorLoader = new InspectableLoader(dataTable.Inspectable);
        readableLoader = new ReadableLoader(dataTable.Readable);
    }
}
