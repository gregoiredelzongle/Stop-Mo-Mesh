using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

namespace GPGre.StopMo{

	/// <summary>
	/// Generate new Stop-Motion based animation clip from selection.
	/// </summary>
	public static class GenerateStopMoFromSelection {

		[MenuItem ("StopMo/GenerateStopMoFromSelection")]
		public static void GenerateStopMo(){

			List<MeshRenderer> selectedRenderers = new List<MeshRenderer> ();

			foreach (GameObject selectedGameObject in Selection.gameObjects) {	
				MeshRenderer renderer = selectedGameObject.GetComponent<MeshRenderer> ();
				selectedRenderers.Add (renderer as MeshRenderer);
			}

			if (selectedRenderers.Count == 0) {
				Debug.Log ("None Renderers selected");
				return;
			}

			StopMoAnimationClip obj = ScriptableObject.CreateInstance<StopMoAnimationClip>();
			obj.frames = selectedRenderers.ToArray ();
			obj.Sort ();

			string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath ("Assets/NewStopMoMeshAnimation.asset");
			AssetDatabase.CreateAsset (obj, assetPathAndName);

			AssetDatabase.SaveAssets ();
			EditorUtility.FocusProjectWindow ();
			Selection.activeObject = obj;
			}

	}
}
