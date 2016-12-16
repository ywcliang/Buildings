using G;
using UnityEngine;

public interface UnitBehaviour {
	//when unit touched by screen.
	void onTouch(ref TapGesture e);

//	//unit name
//	string unitName { get;set;}
//	//unit type
//	unitType unitType { get;set;}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	void OnStateEnter(ref Animator animator, ref AnimatorStateInfo stateInfo, ref int layerIndex);

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override	public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//	}

	//OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	void OnStateExit(ref Animator animator, ref AnimatorStateInfo stateInfo, ref int layerIndex);
}
