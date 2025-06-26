using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DialogueData
{
    public int dialogID;
    public List<string> dialogs;
}


public class DialogueDataLoader
{
    private Dictionary<int, DialogueData> dialogDict = new();

    public DialogueDataLoader(List<DialogueEntity> table) {
        foreach (var row in table) {
            if (!dialogDict.ContainsKey(row.DialogID)) {
                // 키값이 없을 때
                dialogDict[row.DialogID] = new DialogueData
                {
                    dialogID = row.DialogID,
                    dialogs = new List<string>()
                };
            }
            // List 크기 조절
            while (dialogDict[row.DialogID].dialogs.Count <= row.Order)
            {
                dialogDict[row.DialogID].dialogs.Add("");
            }

            dialogDict[row.DialogID].dialogs[row.Order] = row.Text;
        }
    }

    public DialogueData Get(int dialogID) {
        dialogDict.TryGetValue(dialogID, out DialogueData data);
        return data;
    }
}
