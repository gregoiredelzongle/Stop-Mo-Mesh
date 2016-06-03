using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.IO;
using System.Collections.Generic;

namespace GPGre.Utilities
{
	public static class PathUtilities {

		#if UNITY_EDITOR
		public static T[] GetAssetsAtPath<T>(string assetPath) where T : Object{

			string folderPath = FullPath (assetPath);
			string[] objsPath = Directory.GetFiles (folderPath);

			List<T> assets = new List<T> ();

			foreach (string objPath in objsPath) {
				if(objPath.EndsWith(".meta"))
					continue;
				T asset = AssetDatabase.LoadAssetAtPath<T> (Assetpath(objPath));
				if (asset != null)
					assets.Add (asset);



			}
			return assets.ToArray ();

		}
		#endif

		public static string FullPath(string assetPath)
		{
			return Application.dataPath.Substring (0, Application.dataPath.Length - 6) + assetPath;
		}

		public static string Assetpath(string fullPath)
		{
			return fullPath.Substring (Application.dataPath.Length - 6);
		}

		public static string LastFolderName(string path)
		{
			return Path.GetFileName (path);
		}
	}
}
