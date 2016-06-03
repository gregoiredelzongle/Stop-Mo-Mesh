using UnityEngine;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GPGre.StopMo{

	/// <summary>
	/// Create new empty Stop-Motion based Animation clip 
	/// </summary>

	[CreateAssetMenu(fileName = "New StopMo AnimationClip", menuName = "StopMo AnimationClip", order = 99)]
	public class StopMoAnimationClip : ScriptableObject {

	    public MeshRenderer[] frames;
	    public int frameRate = 15;

		public void Sort()
		{
			List<MeshRenderer> framesList = frames.ToList ();
			framesList = framesList.OrderBy(go=>go.name).ToList();
			frames = framesList.ToArray ();
		}
	}
}
