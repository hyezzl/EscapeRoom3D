using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var data = ObjectDatabaseManager.Instance.GetData(10001001);

        Debug.Log($"아이템 : {data.itemName}");
        Debug.Log($"설명 : {data.description}");
        Debug.Log($"독백 : {data.monologue}");
        Debug.Log($"타입 : {data.type}");

        if (data.type == ItemType.Readable)
        {
            foreach (var row in data.dialogData.dialogs) {
                Debug.Log(row);
            }
        }
        else
            Debug.Log("얘는 Readable아님");
    }
}
