/// <summary>
/// This Script use with ActionEventRange(Script) to spawn bullet
/// </summary>

using UnityEngine;
using System.Collections;

public class Spell_Bullet : MonoBehaviour {
	
	public GameObject Fx; //bullet particle
	public AudioClip audio; // sound effect
	
	[HideInInspector]
	public Transform target;
	[HideInInspector]
	public float speed;
	[HideInInspector]
	public float damage;
	
	void OnTriggerEnter(Collider col){
		//decrease hp enemy when it hit object tag "Enemy"
		if(col.tag == "Enemy"){
			Enemy enemyTarget = col.GetComponent<Enemy>();
			
			float damageVal = damage - enemyTarget.def;
			if(damageVal <= 0){
				damageVal = 0;	
			}
			enemyTarget.TakingDamage();
			enemyTarget.hp -= damageVal;
			enemyTarget.damageGet = damageVal;
			//Draw text damage
			enemyTarget.InitTextDamage(Color.red);
			if((enemyTarget.hp - damageVal) <= 0){
				enemyTarget.hp = 0;	
				enemyTarget.actionStat = Enemy.ActionStat.Dead;
			}
			//Spawn Damage effect when hit to enemy
			if(Fx != null){
				Instantiate(Fx, enemyTarget.transform.position, enemyTarget.transform.rotation);
			}
			
			//Spawn sfx when hit to enemy
			if(audio != null){
				AudioSource.PlayClipAtPoint(audio, Vector3.zero);
			}
			Destroy(this.gameObject);
		}
	}
	
	void Start(){
		StartCoroutine(UpdateMove());	
	}
	
	IEnumerator UpdateMove(){
		
		//bullet move to target
		while(true){
			if(target != null){
				transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
				transform.LookAt(target.position);
			}
			yield return 0;
		}
	}
}
