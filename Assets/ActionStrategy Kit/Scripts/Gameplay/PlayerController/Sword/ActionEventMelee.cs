/// <summary>
/// This script use for control state of character(Melee)
/// to attack,skill,call sound effect,call particle by using with Setthing Action(Script)
/// </summary>

using UnityEngine;
using System.Collections;

public class ActionEventMelee : MonoBehaviour {

	public Controller controller;
	
	public void CastSkillFX(GameObject FX){
		//Spawn particle fx when calling this method
		Instantiate(FX,this.transform.position, this.transform.rotation);	
	}
	
	public void Skill_1(GameObject FX){
		//Attack skill 1 enemy(target) when calling this method
		if(controller.target != null){
			Enemy enemyTarget = controller.target.GetComponent<Enemy>();
			enemyTarget.target = this.gameObject;
			Instantiate(FX, controller.target.transform.position, controller.target.transform.rotation);
			float damage = controller.skill_1_Value - enemyTarget.def;
			if(damage <= 0){
				damage = 0;	
			}
			enemyTarget.TakingDamage();
			enemyTarget.damageGet = AttackValue(damage);
			enemyTarget.hp -= enemyTarget.damageGet;
			enemyTarget.InitTextDamage(Color.red);
			if((enemyTarget.hp - damage) <= 0){
				enemyTarget.hp = 0;	
				enemyTarget.actionStat = Enemy.ActionStat.Dead;
				controller.target = null;
			}
			
		}
	}
	
	public void Skill_2(GameObject FX){
		//Attack skill 2 enemy(target) when calling this method
		if(controller.target != null){
			Enemy enemyTarget = controller.target.GetComponent<Enemy>();
			enemyTarget.target = this.gameObject;
			Instantiate(FX, controller.target.transform.position, controller.target.transform.rotation);
			float damage = controller.skill_2_Value - enemyTarget.def;
			if(damage <= 0){
				damage = 0;	
			}
			enemyTarget.TakingDamage();
			enemyTarget.damageGet = AttackValue(damage);
			enemyTarget.hp -= enemyTarget.damageGet;
			enemyTarget.InitTextDamage(Color.red);
			if((enemyTarget.hp - damage) <= 0){
				enemyTarget.hp = 0;	
				enemyTarget.actionStat = Enemy.ActionStat.Dead;
				controller.target = null;
			}
			
		}
	}
	
	public void Attacking(GameObject FX){
		//Attack enemy(target) when calling this method
		if(controller.target != null){
			Enemy enemyTarget = controller.target.GetComponent<Enemy>();
			enemyTarget.target = this.gameObject;
			Instantiate(FX, enemyTarget.transform.position, enemyTarget.transform.rotation);
			float damage = controller.actionValue - enemyTarget.def;
			if(damage <= 0){
				damage = 0;	
			}
			enemyTarget.TakingDamage();
			enemyTarget.damageGet = AttackValue(damage);
			enemyTarget.hp -= enemyTarget.damageGet;
			enemyTarget.InitTextDamage(Color.red);
			if((enemyTarget.hp - damage) <= 0){
				enemyTarget.hp = 0;	
				enemyTarget.actionStat = Enemy.ActionStat.Dead;
				controller.target = null;
			}
		}
	}
	
	public void CastSkill_Sound(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	public void Attacking_Sound(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	public void Skill_1_Sound(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	public void Skill_2_Sound(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	public void WeaponSwing(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	private int AttackValue(float val){
		//random value of healing power when calling this method
		float attackValue = Random.Range(val*0.8f, val*1.2f);
		return (int)attackValue;
	}
}
