using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Detector))]
public class DetectorEditor : Editor
{
	private Detector _detector;

	private void OnEnable() {
		_detector = target as Detector;
	}

	public override void OnInspectorGUI() {
		EditorGUILayout.BeginVertical();

		_detector.targetTag = EditorGUILayout.TextField("Target Tag", _detector.targetTag);
		_detector.detectMode = (Detector.DetectMode)EditorGUILayout.EnumPopup("Detect Mode", _detector.detectMode);

		EditorGUILayout.Space();

		EditorGUILayout.LabelField("Detect Parameters", EditorStyles.boldLabel);

		_detector.direction = EditorGUILayout.Vector2Field("Direction", _detector.direction);
		_detector.distance = EditorGUILayout.FloatField("Distance", _detector.distance);

		EditorGUILayout.Space();

		switch (_detector.detectMode) {
			case Detector.DetectMode.circle:
				EditorGUILayout.LabelField("Circle Detect Parameters", EditorStyles.boldLabel);
				_detector.radius = EditorGUILayout.FloatField("Radius", _detector.radius);
				break;

			case Detector.DetectMode.capsule:
				EditorGUILayout.LabelField("Capsule Detect Parameters", EditorStyles.boldLabel);
				_detector.capsuleDirection = (CapsuleDirection2D)EditorGUILayout.EnumPopup("Capsule Direction", _detector.capsuleDirection);
				_detector.size = EditorGUILayout.Vector2Field("Size", _detector.size);
				_detector.angle = EditorGUILayout.FloatField("Angle", _detector.angle);
				break;
		}

		EditorGUILayout.EndVertical();
	}
}
