using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var data = ItemDatabaseManager.Instance.GetData(30001001);

        Debug.Log($"아이템 : {data.itemName}");
        Debug.Log($"설명 : {data.description}");
        Debug.Log($"독백 : {data.monologue}");
        Debug.Log($"타입 : {data.type}");

        if (data.type == ItemType.Readable)
        {
            Debug.Log($"로그 : {data.dialog}");
        }
        else
            Debug.Log("얘는 Readable아님");
    }
}
