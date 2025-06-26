using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectEntity
{
    public int ItemID;
    public string ItemName;
    public string Description;
    public string Monologue;
    public string IconName;
    public string Type;
    public int PairID;
    public int DialogID;
}


[Serializable]
public class DialogueEntity 
{
    public int DialogID;
    public int Order;
    public string Text;
}
