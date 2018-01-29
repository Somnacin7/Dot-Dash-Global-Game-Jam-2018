using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMover : MonoBehaviour {
	public Animator leftHandMotion; 
	public AudioSource leftHandSound;

	public Animator rightHandMotion;

	public enum Direction {None, Right, Left};

	private Direction lastDirection = Direction.None;

	public void moveLeftHand() {
		leftHandMotion.Play ("Left Hand Only");
		leftHandSound.Play ();
	}

	public void stopLeftHand() {
		leftHandMotion.Play ("Idle Up");
		leftHandSound.Stop ();
	}

	public void moveRightHand(Direction dir) {
		if (dir != lastDirection) {
			// stopRightHand ();

			switch (dir) {
			case Direction.Right:
				moveRightHandRight ();
				break;
			case Direction.Left:
				moveRightHandLeft ();
				break;
			}
		}
	}

	void moveRightHandLeft() {
		AnimatorStateInfo animatorStateInfo = rightHandMotion.GetCurrentAnimatorStateInfo (0);

		if (animatorStateInfo.IsName("Idle"))
			rightHandMotion.Play ("Center To Left");
	}

	void moveRightHandRight() {
		AnimatorStateInfo animatorStateInfo = rightHandMotion.GetCurrentAnimatorStateInfo (0);

		if (animatorStateInfo.IsName("Idle"))
			rightHandMotion.Play ("Center To Right");
	}

	public void stopRightHand() {
		AnimatorStateInfo animatorStateInfo = rightHandMotion.GetCurrentAnimatorStateInfo (0);

		if (animatorStateInfo.IsName("Idle Left"))
			rightHandMotion.Play ("Left to Center");
		else if (animatorStateInfo.IsName("Idle Right"))
			rightHandMotion.Play ("Right to Center");
	}
}
