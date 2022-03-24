using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
	//let camera follow target
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] public Transform target;
		[SerializeField] public float lerpSpeed = 1.0f;
		[SerializeField, Range(-10, 0)] public float cameraDepth = -10f;

		private Vector3 _targetPos;

		private void Awake() {
			if (target == null) Debug.LogError("Camera no follow target!");
		}

		private void Start() {
			// 初始化摄像机深度
			Vector3 tmpPos = transform.position;
			tmpPos.z = cameraDepth;
			transform.position = tmpPos;
		}

		private void Update() {
			if (target == null) return;

			_targetPos = target.position;
			_targetPos.z = cameraDepth;

			transform.position = Vector3.Lerp(transform.position, _targetPos, lerpSpeed * Time.deltaTime);
		}

	}
}
