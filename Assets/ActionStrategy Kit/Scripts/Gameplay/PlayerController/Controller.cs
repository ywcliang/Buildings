/// <summary>
/// This script use for control state of character.
/// </summary>

using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public enum TypeChatacter{ //Type of character
		Melee, Range, Healer	
	}
	
	public enum ActionStat{ //State of character
		Idle, Move, Action, Skill, Dead
	}
	
	public TypeChatacter typeCharacter; 
	public AnimationClip idle, walk, action, cast_1, cast_2, skill_1, skill_2, dead; //animation character
	public GameObject[] objMeshAndSkinMesh; //Gameobject Mesh use for change color material when taking attack
	public Texture2D icon_Skill_1, icon_Skill_2; //Icon skill
	public Texture2D textureHpFull, textureHpEmpty; //hp bar
	public Color colorTakeDamage; //color when character take damage
	
	//Status Character
	public float maxhp; //Max hp
	public float def; //Defend
	public float speedMove; //Speed movement
	public float actionValue; //If character is Melee,Range it mean Attack , If character is Healer it mean Healing power
	public float skill_1_Value; //Skill 1 Damage
	public float skill_2_Value; //Skill 2 Damage
	public float actionSpeed; //If character is Melee,Range it mean AttackSpeed , If character is Healer it mean Healing speed
	public float distanceAction; //Distance attack , Distance heal
	public float deley_Cast_Skill_1; //Cast skill timer
	public float deley_Cast_Skill_2; //Cast skill timer
	public float coolDown_Skill_1; //Cooldown to use skill 1 again
	public float coolDown_Skill_2; //Cooldown to use skill 2 again
	
	
	//Variable private field 
	private float countCastSkill_1;
	private float countCastSkill_2;
	private float countAction;
	private float checkDistance;
	private Vector3 pointHp;
    private Rect rectHp;
	
	//Delegate update function
	public delegate void FunctionHandle();
	public FunctionHandle ActionHandle;
	public FunctionHandle SkillHandle;
	
	[HideInInspector]
	public GameObject target;
	
	[HideInInspector]
	public float damageGet;
	
	[HideInInspector]
	public Vector3 positionWay;
	[HideInInspector]
	public ActionStat actionStat;
	[HideInInspector]
	public bool active;
	[HideInInspector]
	public float value_CoolDown_Skill_1,value_CoolDown_Skill_2;
	[HideInInspector]
	public float hp;
	[HideInInspector]
	public Transform pointSpell;
	
	void Start(){
		ActionHandle = Action_1;
		hp = maxhp;
	}
	
	void Update(){
		UpdateActionStat();	
	}
	
	void OnGUI(){
		//Draw hp GUI on character
		Vector3 pointTransform = Vector3.zero;
		pointTransform.x = transform.position.x;
		pointTransform.y = transform.position.y+1.5f;
		pointTransform.z = transform.position.z;
		pointHp = TouchController.instance.cameraTarget.WorldToScreenPoint(pointTransform);
		rectHp.width = 100;
		rectHp.height = 10;
		rectHp.x = pointHp.x-(rectHp.width/2);
		rectHp.y = Screen.height-pointHp.y-(rectHp.height/2);
		GUI.DrawTexture(rectHp,textureHpEmpty);
		GUI.BeginGroup(rectHp,"");
		GUI.DrawTexture(new Rect(0,0,100*(hp/maxhp),rectHp.height),textureHpFull);
		GUI.EndGroup();
	}
	
	void UpdateActionStat(){
		
		//Hero State animation
		switch(actionStat){
			case ActionStat.Idle:{
				if(actionStat != ActionStat.Dead){
					GetComponent<Animation>().CrossFade(idle.name);
				}
			}
			break;
			
			case ActionStat.Move:{
				if(actionStat != ActionStat.Dead){
					checkDistance = Vector3.Distance(transform.position, LookAtTo(positionWay));
					if(checkDistance >= 1){
						GetComponent<Animation>().CrossFade(walk.name);
						transform.Translate(Vector3.forward*speedMove*Time.deltaTime);
					}else{
						actionStat = ActionStat.Idle;
					}
				}
			}
			break;
			
			case ActionStat.Action:{
				if(actionStat != ActionStat.Dead){
					if(target != null){
						checkDistance = (transform.position - LookAtTo(target.transform.position)).magnitude;
						if(checkDistance >= distanceAction){
							if(GetComponent<Animation>()[action.name].normalizedTime <= 0){
								GetComponent<Animation>().CrossFade(walk.name);
								transform.Translate(Vector3.forward*speedMove*Time.deltaTime);
							}
						}else{
							ActionHandle();
							if(target.GetComponent<Enemy>() != null && target.GetComponent<Enemy>().hp <= 0){
									target = null;
							}
						}
					}else{
						actionStat = ActionStat.Idle;
					}
				}
			}
			break;
			
			case ActionStat.Skill:{
				if(actionStat != ActionStat.Dead){
					if(SkillHandle != null){
						SkillHandle();
					}
				}
			}
			break;
			
			case ActionStat.Dead:{
				GetComponent<Animation>().CrossFade(dead.name);
				Destroy(gameObject,4);
			}
			break;
		}
	}
	
	private Vector3 LookAtTo(Vector3 pos){
		//Lookat Monster
		Vector3 look = Vector3.zero;
		look.x = pos.x;
		look.y = transform.position.y;
		look.z = pos.z;
		try{
			this.transform.LookAt(look);
		}catch{
			Debug.Log("None Look");	
		}
		return look;
	}
	
	private void Action_1(){
		//Wait delay attack
		GetComponent<Animation>().CrossFade(idle.name);
		countAction += actionSpeed * Time.smoothDeltaTime;
		if(countAction >= 100){
			countAction = 0;
			ActionHandle = Action_2;	
		}
	}
	
	private void Action_2(){
		//Attack
		GetComponent<Animation>().Play(action.name);
		if(GetComponent<Animation>()[action.name].normalizedTime > 0.9f){
			ActionHandle = Action_1;	
		}
	}
	
	public void Skill_1_Cast(){
		//Cast Skill 1
		GetComponent<Animation>().Play(cast_1.name);
		countCastSkill_1 += 1 * Time.smoothDeltaTime;
		if(countCastSkill_1 >= deley_Cast_Skill_1){
			SkillHandle = Skill_1_Action;
			countCastSkill_1 = 0;
		}
	}
	
	public void Skill_1_Action(){
		//Skill 1
		GetComponent<Animation>().CrossFade(skill_1.name);
		Debug.Log("Skill_1");
		if(GetComponent<Animation>()[skill_1.name].normalizedTime > 0.9f){
			if(target != null){
				actionStat = ActionStat.Action;
			}else{
				actionStat = ActionStat.Idle;
			}
			SkillHandle = null;
		}
	}
	
	public void Skill_2_Cast(){
		//Cast Skill 2
		GetComponent<Animation>().Play(cast_2.name);
		countCastSkill_2 += 1 * Time.smoothDeltaTime;
		if(countCastSkill_2 >= deley_Cast_Skill_2){
			SkillHandle = Skill_2_Action;
			countCastSkill_2 = 0;
		}
	}
	
	public void Skill_2_Action(){
		//Skill 2
		GetComponent<Animation>().CrossFade(skill_2.name);
		if(GetComponent<Animation>()[skill_2.name].normalizedTime > 0.9f){
			if(target != null){
				actionStat = ActionStat.Action;
			}else{
				actionStat = ActionStat.Idle;
			}
			SkillHandle = null;
		}
	}
	
	public void TakingDamage(){
		//if take damage material monster will change to white color
		int index = 0;
		while(index < objMeshAndSkinMesh.Length){
			objMeshAndSkinMesh[index].GetComponent<Renderer>().material.color = Color.white;
			index++;
		}
		
		StartCoroutine(TakeDamage(0.1f));
	}
	
	public void InitTextDamage(Color colorText){
		// Init text damage
		GameObject loadPref = (GameObject)Resources.Load("TextDamage");
		GameObject go = (GameObject)Instantiate(loadPref, transform.position, Quaternion.identity);
		go.GetComponentInChildren<TextDamage>().SetDamage(damageGet, colorText);
	}
	
	public void AddBuff(float buffValue, float time){
		// Buff value
		actionValue += buffValue;
		skill_1_Value += buffValue;
		skill_2_Value += buffValue;
		StartCoroutine(BuffCount(buffValue, time));
	}
	
	private IEnumerator BuffCount(float buffValue ,float time){
		// Buff duration
		yield return new WaitForSeconds(time);
		actionValue -= buffValue;
		skill_1_Value -= buffValue;
		skill_2_Value -= buffValue;
	}
	
	private IEnumerator TakeDamage(float time){
		//if take damage material monster will change to setting color
		int index = 0;
		Color[] colorDef = new Color[objMeshAndSkinMesh.Length];
		while(index < objMeshAndSkinMesh.Length){
			colorDef[index] = objMeshAndSkinMesh[index].GetComponent<Renderer>().material.color;
			objMeshAndSkinMesh[index].GetComponent<Renderer>().material.color = colorTakeDamage;
			index++;
		}
		yield return new WaitForSeconds(time);
		index = 0;
		while(index < objMeshAndSkinMesh.Length){
			objMeshAndSkinMesh[index].GetComponent<Renderer>().material.color = colorDef[index];
			index++;
		}
		yield return 0;
		StopCoroutine("TakeDamage");
	}
	
	private IEnumerator CalCoolDownSkill_1(){
		//Cooldown skill 1
		float countCoolDown = coolDown_Skill_1;
		while(countCoolDown > 0){
			countCoolDown -= 1 * Time.smoothDeltaTime;
			value_CoolDown_Skill_1 = countCoolDown/coolDown_Skill_1;
			yield return 0;
		}
		yield return 0;
	}
	
	private IEnumerator CalCoolDownSkill_2(){
		//Cooldown skill 2
		float countCoolDown = coolDown_Skill_2;
		while(countCoolDown > 0){
			countCoolDown -= 1 * Time.smoothDeltaTime;
			value_CoolDown_Skill_2 = countCoolDown/coolDown_Skill_2;
			yield return 0;
		}
		yield return 0;
	}
}
