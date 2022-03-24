using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 检测类组件，需要把调用优先级设的比较高，否则别人调用时可能还没完成检测
/// </summary>

public class Detector : MonoBehaviour
{
	public enum DetectMode
	{
		circle = 0,
		capsule = 1
	}

	[SerializeField] public string targetTag;
	[SerializeField] public DetectMode detectMode;

	[Header("Detect Parameters")]
	[SerializeField] public Vector2 direction;
	[SerializeField] public float distance = 0f;

	[Header("Circle Detect Parameters")]
	[SerializeField] public float radius;

	[Header("Capsule Detect Parameters")]
	[SerializeField] public CapsuleDirection2D capsuleDirection;
	[SerializeField] public Vector2 size;
	[SerializeField] public float angle = 0f;

	/* 辅助字段*/
	private RaycastHit2D[] _hits;
	private List<GameObject> _goList;

	public List<GameObject> Targets => _goList;


	// Start is called before the first frame update
	void Start() {
		_goList = new List<GameObject>();
	}

	// Update is called once per frame
	void Update() {
		_goList.Clear();

		switch (detectMode) {
			case DetectMode.circle:
				_hits = Physics2D.CircleCastAll(transform.position,
												radius,
												direction,
												distance);
				break;

			case DetectMode.capsule:
				_hits = Physics2D.CapsuleCastAll(transform.position,
												 size,
												 capsuleDirection,
												 angle,
												 direction,
												 distance);
				break;
		}

		foreach (var tmp in _hits) {
			// if (gameObject.name == "RangeDetector") {
			// 	Debug.Log("Collider name: " + tmp.collider.name);
			// 	Debug.Log("Transform name: " + tmp.transform.name);
			// }
			if (tmp.collider.tag == targetTag) {
				_goList.Add(tmp.collider.gameObject);
			}
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;
		if (detectMode == DetectMode.circle)
			DrawCircleGizmos();
		else
			DrawCapsuleGizmos();
	}

	private void DrawCircleGizmos() {
		Gizmos.DrawWireSphere(transform.position, radius);
	}

	private void DrawCapsuleGizmos() {
		if (capsuleDirection == CapsuleDirection2D.Vertical) {
			Vector3 center1 = transform.position, center2 = transform.position;
			center1.y += size.y / 2;
			center2.y -= size.y / 2;
			Gizmos.DrawWireSphere(center1, size.x / 2);
			Gizmos.DrawWireSphere(center2, size.x / 2);
		} else {
			Vector3 center1 = transform.position, center2 = transform.position;
			center1.x += size.x / 2;
			center2.x -= size.x / 2;
			Gizmos.DrawWireSphere(center1, size.y / 2);
			Gizmos.DrawWireSphere(center2, size.y / 2);
		}
	}
}
