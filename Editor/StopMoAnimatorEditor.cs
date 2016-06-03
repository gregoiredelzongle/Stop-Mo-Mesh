using UnityEngine;
using UnityEditor;

using GPGre.StopMo;

[CustomEditor(typeof(StopMoAnimator))]
public class StopMoMeshAnimatorEditor : Editor {

    private StopMoAnimator stopMoAnimator;

    private int frameSelection = 0;
    private int lastFrameSelection = 0;

	public override void OnInspectorGUI()
    {
        stopMoAnimator = target as StopMoAnimator;

        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(Application.isPlaying);
		if(!stopMoAnimator.IsEmpty() && stopMoAnimator.stopMoAnimationClip.frames.Length > 0)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("StopMo Preview :");
			lastFrameSelection = EditorGUILayout.IntSlider(lastFrameSelection, 0, stopMoAnimator.stopMoAnimationClip.frames.Length - 1);
            EditorGUILayout.EndHorizontal();
			if (EditorGUI.EndChangeCheck() && lastFrameSelection != frameSelection)
            {
                frameSelection = lastFrameSelection;
				stopMoAnimator.SwitchMesh(stopMoAnimator.stopMoAnimationClip.frames[frameSelection]);
            }
        }
        EditorGUI.EndDisabledGroup();
    }


}
