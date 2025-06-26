using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var data = ObjectDatabaseManager.Instance.GetData(10001001);

        Debug.Log($"������ : {data.itemName}");
        Debug.Log($"���� : {data.description}");
        Debug.Log($"���� : {data.monologue}");
        Debug.Log($"Ÿ�� : {data.type}");

        if (data.type == ItemType.Readable)
        {
            foreach (var row in data.dialogData.dialogs) {
                Debug.Log(row);
            }
        }
        else
            Debug.Log("��� Readable�ƴ�");
    }
}
