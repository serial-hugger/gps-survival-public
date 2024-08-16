using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour {

	public ParticleSystem particles;
	public float timeTillDestroy = 5.0f;
	public string enemyID;
	public string weaponID;
	public bool enemyHit;
	public Inventory inventoryScript;
	public GameObject enemy;

	// Use this for initialization
	void Start () {
		weaponID = Controller.lastWeaponUsed;
		GameObject slash = (GameObject)Instantiate (Resources.Load ("HitSound"), new Vector3 (transform.position.x, transform.position.y+.05f, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if(particles.isStopped){
			if(enemy!=null){
				inventoryScript.openBattleWindow (enemyID,weaponID);
			}
			GameObject.Destroy (gameObject);
			GameObject.Destroy (enemy);
		}
	}
	void OnCollisionEnter(Collision collision){
		if(collision.transform.tag == "Entity" && enemy == null){
			enemyHit = true;
			GeneralEntity enemyScript = collision.gameObject.GetComponent<GeneralEntity> ();
			enemyID = enemyScript.entityID;
			enemy = collision.gameObject;
		}
	}
}
