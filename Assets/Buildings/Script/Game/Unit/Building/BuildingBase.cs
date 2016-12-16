﻿using Unit;
using UnityEngine;
using G;

namespace Buildings
{
	public class BuildingBase : Building {

		public BuildingBase()
		{
			m_SUnitName = "War";
			m_SUnitType = UnitType.BUILDING_BASE;
			m_ECurrentLevel = UnitLevel.LEVEL_ZERO;
			m_CModel = null;
			m_CProduceRate = s_ProduceRate[0];
		}

		public override void onTouch (ref TapGesture e)
		{
			base.onTouch (ref e);

			if (m_CAnimatorController)
				m_CAnimatorController.DestoryBuild ();
		}

		//OnStateExit is called when a transition ends and the state machine finishes evaluating this state
		override public void OnStateExit(ref Animator animator, ref AnimatorStateInfo stateInfo, ref int layerIndex) {
			if (stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Construct_Doing)) {

			} else if (stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Produce_Doing)){

			}
			else if (stateInfo.IsName (G.GlobalDef.s_GBuildAnimator_Destory_Doing)){
				//create new one
				UnitType nextBuilding = UnitType.BUILDING_TOWN_HALL;
				BuildingManager.LoadBuildingByType(nextBuilding,  transform);
			}

			base.OnStateExit (ref animator, ref stateInfo, ref layerIndex);
		}
	}
}