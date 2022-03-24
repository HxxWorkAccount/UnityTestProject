using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
	[SerializeField] private InputSO input;
	[SerializeField] private float initSpeed;
	[SerializeField] private float destroyTime;

	private Rigidbody2D rd;

	private bool _isCollide = false;
	private Vector2 _movement;

	// Start is called before the first frame update
	void Start() {
		rd = GetComponent<Rigidbody2D>();
		_movement = new Vector2(input.MousePos.x - Screen.width / 2, input.MousePos.y - Screen.height / 2);
		_movement = _movement.normalized * initSpeed;
		rd.AddForce(_movement);
		Destroy(gameObject, destroyTime);
	}
}
