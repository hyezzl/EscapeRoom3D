using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabaseManager : Singleton<ItemDatabaseManager> {
    private ObjectDataLoader dataLoader;
    public ObjectData GetData(int itemID) => dataLoader.Get(itemID);

    protected override void DoAwake()
    {
        base.DoAwake();
        LoadTable();
    }

    private void LoadTable() {
        EscapeTable dataTable = Resources.Load<EscapeTable>("Table/EscapeTable");

        DialogueDataLoader dialogLoader = new DialogueDataLoader(dataTable.Dialogue);
        dataLoader = new ObjectDataLoader(dataTable.Object, dialogLoader);
    }
}
