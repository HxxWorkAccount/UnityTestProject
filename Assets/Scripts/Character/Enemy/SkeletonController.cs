using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonController : MonoBehaviour, IHurtable
{
	[SerializeField] private EnemyConfigSO enemyConfig;
	[SerializeField] private Detector attackDetector;
	[SerializeField] private Detector rangeDetector;

	private struct TargetInfo
	{
		public Transform targetTran;
		public int instanceID;
		public bool isDefault;
	}

	/* 额外组件 */
	private Animator animator;
	private Pathfinding.AIPath aiPath;

	/* 成员字段 */
	[SerializeField, HideInInspector] private int _health;
	private TargetInfo _targetInfo;
	private GameObject _defaultPos;
	private float _actualAttackInterval = 0f;
	private bool _isDead = false;

	void Start() {
		aiPath = GetComponent<Pathfinding.AIPath>();
		aiPath.maxSpeed = enemyConfig.maxSpeed;
		animator = GetComponent<Animator>();

		_health = enemyConfig.initHealth;
		_defaultPos = new GameObject();
		_defaultPos.transform.position = transform.position;
		_targetInfo.targetTran = _defaultPos.transform;
		_targetInfo.instanceID = _defaultPos.GetInstanceID();
		_targetInfo.isDefault = true;
	}

	void Update() {
		if (_isDead) return;

		/* 是否死亡 */
		if (_health <= 0) {
			animator.SetBool("IsDead", true);
			aiPath.maxSpeed = 0f;
			Destroy(gameObject, 5f);
		}

		/* 更新攻击间隔 */
		if (_actualAttackInterval > 0) _actualAttackInterval -= Time.deltaTime;
		else if (_actualAttackInterval < 0) _actualAttackInterval = 0f;

		/* 更新目标 */
		if (rangeDetector.Targets.Count > 0) {
			if (_targetInfo.isDefault || !rangeDetector.Targets.Contains(_targetInfo.targetTran.gameObject)) {
				foreach (var foo in rangeDetector.Targets) {
					_targetInfo.targetTran = foo.transform;
					_targetInfo.instanceID = _targetInfo.targetTran.GetInstanceID();
					break;
				}
				_targetInfo.isDefault = false;
			}

		} else if (!_targetInfo.isDefault) {
			_targetInfo.targetTran = _defaultPos.transform;
			_targetInfo.isDefault = true;
		}
		aiPath.destination = _targetInfo.targetTran.position;

		/* 是否移动（位移由 A* 完成，这里负责转向和动画） */
		if ((transform.position - _targetInfo.targetTran.position).magnitude < 0.1f) {
			animator.SetBool("IsRun", false);
		} else {
			TurnSide(_targetInfo.targetTran);
			animator.SetBool("IsRun", true);

		}

		/* 是否攻击 */
		if (attackDetector.Targets.Count > 0) {
			if (_actualAttackInterval != 0) return;
			_actualAttackInterval = enemyConfig.attackInterval;
			animator.SetTrigger("AttackTrigger");
		}

	}

	public void Hurt(int value) {
		if (value <= 0) return;
		animator.SetTrigger("TakeDamageTrigger");
		_health -= value;
		if (_health < 0) _health = 0;
	}

	public void AttackFrame() {
		if (attackDetector.Targets.Count == 0) return;
		foreach (var target in attackDetector.Targets) {
			IHurtable foo = target.GetComponent<TopDownCharacterController>();
			foo.Hurt(enemyConfig.attackDamage);
		}
	}

	private void TurnSide(Transform targetTran) {
		if (targetTran.position.x < transform.position.x)
			transform.localScale = new Vector3(-1, 1, 1); // 翻转物体
		else if (targetTran.position.x > transform.position.x)
			transform.localScale = new Vector3(1, 1, 1);
	}

	private void OnGUI() {
		GUILayout.Label("Skeleton target name: " + _targetInfo.targetTran.gameObject.name);
	}
}
