using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/Table")]
public class LoadingTexts : ScriptableObject
{
	public List<LoadingTextsEntity> loadingTexts; // Replace 'EntityType' to an actual type that is serializable.
}
