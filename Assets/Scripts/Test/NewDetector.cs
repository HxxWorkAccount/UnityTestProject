using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 每帧都做范围检测，放入容器
public class NewDetector : MonoBehaviour
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
			if (tmp.transform.tag == targetTag) {
				Debug.Log("Bingo");
				_goList.Add(tmp.transform.gameObject);
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
