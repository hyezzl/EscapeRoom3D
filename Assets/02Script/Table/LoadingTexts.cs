using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Table")]
public class LoadingTexts : ScriptableObject
{
	public List<LoadingTextsEntity> loadingTexts; // Replace 'EntityType' to an actual type that is serializable.
}

//[System.Serializable]
//public class LoadingTexts
//{
//    public List<LoadingTextsEntity> loadingTexts;
//}

[System.Serializable]
public class LoadingTextsEntity
{
    public string LoadingTexts;
}
