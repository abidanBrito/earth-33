using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ParticleSystemController))]
public class ParticleSystemControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		ParticleSystemController psCtrl = (ParticleSystemController)target;
		if(GUILayout.Button("Refresh"))
		{
			psCtrl.UpdateParticleSystem();
		}
	}
}
#endif
