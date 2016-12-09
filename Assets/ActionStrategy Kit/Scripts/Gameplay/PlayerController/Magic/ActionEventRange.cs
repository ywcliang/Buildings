/// <summary>
/// This script use for control state of character(Range)
/// to attack,skill,call sound effect,call particle by using with Setthing Action(Script)
/// </summary>

using UnityEngine;
using System.Collections;

public class ActionEventRange : MonoBehaviour {
	
	public Controller controller;
	
	public void CastSkillFX(GameObject FX){
		//Spawn particle fx when calling this method
		Instantiate(FX,this.transform.position, this.transform.rotation);	
	}
	
	public void Spelling(GameObject spellPref){
		//Spawn spell bullet from Spell_Buller(script) when calling this method
		GameObject spellObj = (GameObject)Instantiate(spellPref, controller.pointSpell.position, controller.pointSpell.rotation) as GameObject;
		Spell_Bullet spellBullet = spellObj.GetComponent<Spell_Bullet>();
		spellBullet.speed = 5;
		spellBullet.damage = AttackValue(controller.actionValue);
		if(controller.target != null){
			spellBullet.target =	controller.target.transform;
		}else{
			Destroy(spellObj);		
		}
		if(spellBullet.target == null){
			Destroy(spellObj);	
		}
	}
	
	public void Spelling_Cold(GameObject spellPref){
		//Spawn spell bullet (skill 1) from Spell_Buller(script) when calling this method
		GameObject spellObj = (GameObject)Instantiate(spellPref, controller.pointSpell.position, controller.pointSpell.rotation) as GameObject;
		Spell_Bullet spellBullet = spellObj.GetComponent<Spell_Bullet>();
		spellBullet.speed = 5;
		spellBullet.damage = AttackValue(controller.skill_1_Value);
		if(controller.target != null){
			spellBullet.target =	controller.target.transform;
		}else{
			Destroy(spellObj);		
		}
		if(spellBullet.target == null){
			Destroy(spellObj);	
		}
	}
	
	public void Spelling_Wind(GameObject spellPref){
		//Spawn spell bullet (skill 2) from Spell_Buller(script) when calling this method
		GameObject spellObj = (GameObject)Instantiate(spellPref, controller.pointSpell.position, controller.pointSpell.rotation) as GameObject;
		Spell_Bullet spellBullet = spellObj.GetComponent<Spell_Bullet>();
		spellBullet.speed = 5;
		spellBullet.damage = AttackValue(controller.skill_2_Value);
		if(controller.target != null){
			spellBullet.target =	controller.target.transform;
		}else{
			Destroy(spellObj);		
		}
		if(spellBullet.target == null){
			Destroy(spellObj);	
		}
	}
	
	public void CastSkill_Sound(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	public void Spelling_Sound(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	private int AttackValue(float val){
		//random value of healing power when calling this method
		float attackValue = Random.Range(val*0.8f, val*1.2f);
		return (int)attackValue;
	}
}
