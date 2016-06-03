using UnityEngine;
using UnityEditor;

using System.Collections.Generic;
using System.IO;

using GPGre.Utilities;

public static class GenerateLODGroup {

	// Create A LODGroup Prefab from a folder selection

	public const int lodAmount = 3;

	[MenuItem ("StopMo/GenerateLODGroup")]
	public static void GenLODGroup(){
		
		var path = "";
		var obj = Selection.activeObject;

		if (obj == null)
			path = "Assets";
		else
			path = AssetDatabase.GetAssetPath (obj.GetInstanceID ());

		if (path.Length > 0) {
			if (Directory.Exists (path)) {
				MeshRenderer[] lods = LODGroup(path);
				if (lods.Length == 0) {
					Debug.Log ("Select a valid LOD Folder");
				} else {
					string name = PathUtilities.LastFolderName (path);
					CreateLODGroup (name,lods, path);
				}
			}
			else
				Debug.Log ("Select a valid LOD Folder");
			
		} else {
			Debug.Log ("Not in assets folder");
		}

		/*

		foreach (GameObject selectedGameObject in Selection.gameObjects) {	
			MeshRenderer renderer = selectedGameObject.GetComponent<MeshRenderer> ();
			selectedRenderers.Add (renderer as MeshRenderer);
		}

		if (selectedRenderers.Count == 0) {
			Debug.Log ("None Renderers selected");
			return;
		}

		StopMoMesh obj = ScriptableObject.CreateInstance<StopMoMesh>();
		obj.frames = selectedRenderers.ToArray ();
		obj.Sort ();

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath ("Assets/NewStopMoMeshAnimation.asset");
		AssetDatabase.CreateAsset (obj, assetPathAndName);

		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = obj;*/
	}

	public static MeshRenderer[] LODGroup(string folderPath)
	{
		MeshRenderer[] lods = PathUtilities.GetAssetsAtPath<MeshRenderer>(folderPath);
		return lods;
	}

	public static void CreateLODGroup(string name, MeshRenderer[] lodsRenderer, string folderPath)
	{
		GameObject prefab = new GameObject ();
		LOD[] lods = new LOD[lodsRenderer.Length];
		LODGroup lodGroup = prefab.AddComponent<LODGroup> (); 

		prefab.name = name;
		int i = 0;
		foreach (MeshRenderer lodRenderer in lodsRenderer) {
			lodsRenderer[i] = MeshRenderer.Instantiate (lodRenderer);
			lodsRenderer[i].transform.parent = prefab.transform;
			lodsRenderer[i].name = lodRenderer.name;
			lods [i] = new LOD (lodGroup.GetLODs () [i].screenRelativeTransitionHeight, new MeshRenderer[]{lodsRenderer[i]});
			i++;
			}
		lodGroup.SetLODs(lods);
		prefab.isStatic = true;
		PrefabUtility.CreatePrefab (folderPath + "/" + name + ".prefab", prefab );
		GameObject.DestroyImmediate (prefab);

	}




}

