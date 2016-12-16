using UnityEngine;
using System.Collections;
using Buildings;
using G;

public class BuildAnimator : MonoBehaviour {
	private Animator m_CAnimator = null;
	private BuildBehaviour m_CbuildBehaviour = null;

	public Building m_CBuildInstance{ set; get;}


	// Use this for initialization
	void Start () {
		//StartCoroutine (InitAnimator());
		ResetVar();
	}

	public IEnumerator InitAnimator()
	{
		while (m_CbuildBehaviour == null)
		{
			m_CAnimator = gameObject.GetComponent<Animator> ();

			if (m_CAnimator != null) {
				BuildBehaviour tt = m_CAnimator.GetBehaviour<BuildBehaviour> ();
				if (tt != null) {
					m_CbuildBehaviour = tt;
					m_CbuildBehaviour.m_Animator = this;
				}
			}
			yield return 0;
		}
	}
		
	// Update is called once per frame
	void Update () {
		
	}

	public void PreloadOver()
	{
		if (m_CAnimator == null)
			return;
		//after load resource we should decide to change state to new construct build or directly build, player's building built before will change the build level.
		m_CAnimator.SetBool (GlobalDef.s_GBuildAnimator_PreLoadDone_Var, true);
		m_CAnimator.SetInteger(GlobalDef.s_GBuildAnimator_BuildLevel_Var, (int)m_CBuildInstance.getLevelPhase ());
	}

	//play construct animation
	public void StartConstruct(int build_level)
	{
		if (m_CAnimator == null)
			return;
		m_CAnimator.SetInteger (GlobalDef.s_GBuildAnimator_BuildLevel_Var, build_level);
		m_CAnimator.SetBool (GlobalDef.s_GBuildAnimator_Construct_Var, true);
	}

	//stop construct animation
	public void StopConstruct()
	{
		if (m_CAnimator == null)
			return;
		m_CAnimator.SetBool (GlobalDef.s_GBuildAnimator_Construct_Var, false);
	}

	//destory build
	public void DestoryBuild()
	{
		if (m_CAnimator == null)
			return;
		m_CAnimator.SetTrigger (GlobalDef.s_GBuildAnimator_Destory_Var);
	}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	public void OnStateEnter(ref Animator animator, ref AnimatorStateInfo stateInfo, ref int layerIndex) {
		if (m_CBuildInstance == null)
			return;
		m_CBuildInstance.OnStateEnter (ref animator, ref stateInfo, ref layerIndex);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//	public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//	}

	//OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	public void OnStateExit(ref Animator animator, ref AnimatorStateInfo stateInfo, ref int layerIndex) {
		if (m_CBuildInstance == null)
			return;
		m_CBuildInstance.OnStateExit (ref animator, ref stateInfo, ref layerIndex);
	}

	public void ResetVar()
	{
		m_CAnimator.SetInteger (GlobalDef.s_GBuildAnimator_BuildLevel_Var, 0);
		m_CAnimator.SetBool (GlobalDef.s_GBuildAnimator_Construct_Var, false);
		m_CAnimator.SetTrigger (GlobalDef.s_GBuildAnimator_Destory_Var);
		m_CAnimator.SetBool (GlobalDef.s_GBuildAnimator_PreLoadDone_Var, false);
	}
}
