using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownCharacterController : MonoBehaviour, IHurtable
{
	/* SO字段 */
	[SerializeField] private GameObject projectilePerfab;
	[SerializeField] private Detector attackDetector;
	[SerializeField] private EnergyConfigSO energyConfig;
	[SerializeField] private EnergySO energy;
	[SerializeField] private HealthConfigSO healthConfig;
	[SerializeField] private HealthSO health;
	[SerializeField] private InputSO input;
	[SerializeField] private float speed; // 移动速度
	[SerializeField] private float fireInterval; // 攻击间隔
	[SerializeField] private int damage;

	/* 自身组件 */
	private Animator animator;
	private Rigidbody2D rd;

	/* 辅助字段 */
	private bool _isDead = false;
	private bool _isLockTurnSide = false;
	private float _fireInterval = 0f;
	private float _turnSideInterval = 0f;

	private void Awake() {
		animator = GetComponent<Animator>();
		rd = GetComponent<Rigidbody2D>();
		_fireInterval = 0;

		energy.SetMaxEnergy(energyConfig.maxEnergy);
		energy.SetEnergy(energyConfig.initEnergy);
		health.SetMaxHealth(healthConfig.maxHealth);
		health.SetHealth(healthConfig.initHealth);
	}

	private void Start() {
	}

	private void Update() {
		if (_isDead) {
			return;
		}

		/* 是否死亡 */
		if (health.Health <= 0) {
			_isDead = true;
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			animator.SetBool("IsDead", true);
			Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + 3f);
		}

		/* 更新间隔 */
		if (_fireInterval > 0) _fireInterval -= Time.deltaTime;
		else _fireInterval = 0f;

		if (_turnSideInterval > 0) _turnSideInterval -= Time.deltaTime;
		else {
			_isLockTurnSide = false;
			_turnSideInterval = 0f;
		}

		/* 是否移动 */
		if (input.Move != Vector2.zero) {
			animator.SetBool("IsRun", true);
			rd.velocity = input.Move * speed;
		} else {
			animator.SetBool("IsRun", false);
			rd.velocity = Vector2.zero;
		}
		/* 是否轻击 */
		if (input.Attack1 && _fireInterval == 0) {
			_fireInterval = fireInterval;
			animator.SetTrigger("AttackTrigger");

			_isLockTurnSide = true;
		}
		/* 是否重击 */
		if (input.Attack2 && _fireInterval == 0) {
			_fireInterval = fireInterval;
			animator.SetTrigger("StormTrigger");
			_isLockTurnSide = true;
		}
		/* 是否翻滚 */
		if (input.Dodge) {
			animator.SetTrigger("DodgeTrigger");
			_isLockTurnSide = true;
		}
	}

	void LateUpdate() {
		/* 是否 TurnSide */
		if (_isLockTurnSide == false && _turnSideInterval == 0) {
			if (input.Move.x < 0) TurnSide(true);
			else if (input.Move.x > 0) TurnSide(false);
		} else {
			_isLockTurnSide = true;
			_turnSideInterval = 1f; // 获得当前动画剩余时间太麻烦，直接锁 1 秒
			if (input.MousePos.x < Screen.width / 2) TurnSide(true);
			else TurnSide(false);
		}
	}

	public void Hurt(int value) {
		if (value <= 0) return;
		animator.SetTrigger("TakeDamageTrigger");
		health.Hurt(value);
	}

	public void AttackFrame() {
		if (attackDetector.Targets.Count == 0) return;
		foreach (var target in attackDetector.Targets) {
			IHurtable foo = target.GetComponent<SkeletonController>();
			foo.Hurt(damage);
		}
	}
	public void StormFrame() {
		if (energy.Energy > 0) {
			energy.LoseEnergy(1);
			Instantiate(projectilePerfab, transform.position, new Quaternion());
		}
		if (attackDetector.Targets.Count == 0) return;
		foreach (var target in attackDetector.Targets) {
			IHurtable foo = target.GetComponent<SkeletonController>();
			foo.Hurt(damage * 2);
		}
	}

	public void DodgeFrame() {
		// TODO
		Debug.Log("Dodge");
	}

	private void TurnSide(bool isTurnLeft) {
		if (isTurnLeft)
			transform.localScale = new Vector3(-1, 1, 1); // 翻转物体
		else
			transform.localScale = new Vector3(1, 1, 1); // 默认向右
	}
}
