using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	/* 同步数据 */
	[SerializeField] private HealthSO health;
	[SerializeField] private EnergySO energy;

	/* 子对象组件 */
	[SerializeField] private Canvas bloodCanvas;
	[SerializeField] private Canvas energyCanvas;

	/* 辅助字段 */
	private Image[] bloodImages;
	private Image[] energyImages;

	private void Awake() {
		bloodImages = bloodCanvas.GetComponentsInChildren<Image>();
		energyImages = energyCanvas.GetComponentsInChildren<Image>();
	}

	// Update is called once per frame
	private void Update() {
		SetActive(bloodImages, health.Health);
		SetActive(energyImages, energy.Energy);
	}

	private void SetActive(Image[] images, int count) {
		if (count < 0 || count >= images.Length) return;
		int i = 0;
		for (; i < count; ++i) {
			images[i].gameObject.SetActive(true);
		}
		for (; i < images.Length; ++i) {
			images[i].gameObject.SetActive(false);
		}
	}

}
