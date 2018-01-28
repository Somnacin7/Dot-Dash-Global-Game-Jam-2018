using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencyListener : MonoBehaviour {
	private const double LOWEST_FREQUENCY = -100;
	private const double HIGHEST_FREQUENCY = 100;
	private const double FREQUENCY_ADJUSTMENT = (HIGHEST_FREQUENCY - LOWEST_FREQUENCY) / 15;
	private const double FREQUENCY_ADJUSTMENT_PER_SECOND = (HIGHEST_FREQUENCY - LOWEST_FREQUENCY) / 3;
	private double targetFrequency = 0;
	private double frequency = 0;

	//public Button frequencyHigherButton;
	//public Button frequencyLowerButton;

	// Use this for initialization
	void Start () {
		//frequencyHigherButton.onClick.AddListener(onFrequencyAdjustHigher);
		//frequencyLowerButton.onClick.AddListener(onFrequencyAdjustLower);
	}

	void UpdateTargetFrequency() {
		bool increaseDecreaseDirection = (Random.value > 0.5f);
		double adjustment = (increaseDecreaseDirection ? 1 : -1) * FREQUENCY_ADJUSTMENT_PER_SECOND * Time.deltaTime;

		targetFrequency += adjustment;

		if (targetFrequency < LOWEST_FREQUENCY)
			targetFrequency = LOWEST_FREQUENCY;
		else if (targetFrequency > HIGHEST_FREQUENCY)
			targetFrequency = HIGHEST_FREQUENCY;
	}

	// Update is called once per frame
	void UpdateCurrentFrequency () {
		if (Input.GetKey (KeyCode.M))
			onFrequencyAdjustLower();
		else if (Input.GetKey (KeyCode.K))
			onFrequencyAdjustHigher();
	}

	void Update() {
		UpdateTargetFrequency ();
		UpdateCurrentFrequency ();

		Debug.Log ("Target: " + targetFrequency);
		Debug.Log ("Current: " + frequency);
	}

	void onFrequencyAdjustHigher() {
		frequency += FREQUENCY_ADJUSTMENT * Time.deltaTime;

		if (frequency > HIGHEST_FREQUENCY)
			frequency = HIGHEST_FREQUENCY;
	}

	void onFrequencyAdjustLower() {
		frequency -= FREQUENCY_ADJUSTMENT * Time.deltaTime;

		if (frequency < LOWEST_FREQUENCY)
			frequency = LOWEST_FREQUENCY;
	}
}
