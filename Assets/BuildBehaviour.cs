using UnityEngine;
using System.Collections;
using Buildings;
using G;

public class BuildBehaviour : StateMachineBehaviour {
	
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		BuildAnimator m_Animator = animator.GetComponentInParent<BuildAnimator> ();
		if (m_Animator)
		{
			m_Animator.OnStateEnter (ref animator,ref stateInfo,ref layerIndex);
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//		BuildAnimator m_Animator = animator.GetComponentInParent<BuildAnimator> ();
//		if (m_Animator)
//		{
//		}
//	}

	 //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		BuildAnimator m_Animator = animator.GetComponentInParent<BuildAnimator> ();
		if (m_Animator)
		{
			m_Animator.OnStateExit (ref animator,ref stateInfo, ref layerIndex);
		}
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
