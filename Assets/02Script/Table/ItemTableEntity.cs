using System;


[Serializable]
public class PickableEntity
{
    public int ItemID;
    public string ItemName;
    public string Type;
    public string Description;
    public string IconName;
    public int PairID;
}

[Serializable]
public class InteractableEntity
{
    public int ItemID;
    public string ItemName;
    public string Type;
    public string Monologue;
    public string DeactiveMSG;
    public int PairID;
}

[Serializable]
public class InspectableEntity
{
    public int ItemID;
    public string ItemName;
    public string Type;
    public string Monologue;
}

[Serializable]
public class ReadableEntity
{
    public int ItemID;
    public string ItemName;
    public string Type;
    public string Monologue;
    public string Narrative;
    public string Reply;
}
