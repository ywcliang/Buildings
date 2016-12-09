/// <summary>
/// This script use for control state of character(healer)
/// to heal,skill,call sound effect,call particle by using with Setthing Action(Script)
/// </summary>

using UnityEngine;
using System.Collections;

public class ActionEventHealer : MonoBehaviour {
	
	public Controller controller;
	public GetAllPlayer getAllPlayer;
	
	public void CastSkillFX(GameObject FX){
		//Spawn particle fx when calling this method
		Instantiate(FX,this.transform.position, this.transform.rotation);	
	}
	
	public void Healing(GameObject FX){
		//healing to other character when calling this method
		if(controller.target != null){
			Controller playerTarget = controller.target.GetComponent<Controller>();
			Instantiate(FX, playerTarget.transform.position, playerTarget.transform.rotation);
			if(playerTarget.hp > 0){
				playerTarget.damageGet = HealingValue(controller.actionValue);
				playerTarget.hp += playerTarget.damageGet;
				
				playerTarget.InitTextDamage(Color.green);
				if(playerTarget.hp >= playerTarget.maxhp){
					playerTarget.hp = playerTarget.maxhp;	
				}
			}
		}
	}
	
	public void HealingGroup(GameObject FX){
		//healing all character when calling this method
		if(getAllPlayer.players.Length != 0){
			for(int i = 0; i < getAllPlayer.players.Length; i++){
					if(getAllPlayer.players[i] != null){
						Controller playerTarget = getAllPlayer.players[i].GetComponent<Controller>();
						Instantiate(FX, playerTarget.transform.position, playerTarget.transform.rotation);
						if(playerTarget.hp > 0){
							playerTarget.damageGet = HealingValue(controller.skill_1_Value);
							playerTarget.hp += playerTarget.damageGet;
							playerTarget.InitTextDamage(Color.green);
							if(playerTarget.hp >= playerTarget.maxhp){
								playerTarget.hp = playerTarget.maxhp;	
						}
					}
				}
			}
		}
	}

	public void BuffAttack(GameObject FX){
		//give buff(atk up) to other character when calling this method
		if(controller.target != null){
			Controller playerTarget = controller.target.GetComponent<Controller>();
			GameObject go = (GameObject)Instantiate(FX, playerTarget.transform.position, playerTarget.transform.rotation);
			go.transform.parent = playerTarget.transform;
			go.GetComponent<DestroyForTime>().time = 10;
			playerTarget.AddBuff(controller.skill_2_Value, 10);
		}
	}
	
	public void CastSkill_Sound(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	public void Healing_Sound(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	public void HealingGroup_Sound(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	public void BuffAttack_Sound(AudioClip audio){
		//calling sfx when calling this method
		 AudioSource.PlayClipAtPoint(audio, Vector3.zero);	
	}
	
	private int HealingValue(float val){
		//random value of healing power when calling this method
		float healingValue = Random.Range(val*0.8f, val*1.2f);
		return  (int)healingValue;
	}
}
