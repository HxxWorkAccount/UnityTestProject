using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;
using System.Text.RegularExpressions;

// [CanEditMultipleObjects]
// [CustomEditor(typeof(ComponentTest), true)]
public class EditorTest : Editor
{
	// public sealed override void OnInspectorGUI() {
	// 	EditorGUILayout.Separator();
	// 	EditorGUILayout.LabelField("Label", EditorStyles.boldLabel);
	// 	EditorGUI.indentLevel++;
	// 	EditorGUILayout.HelpBox("Help Box Messagees", MessageType.None);
	// 	EditorGUI.indentLevel--;
	// }
}
