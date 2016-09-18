using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //Scene information

public class Collectible : MonoBehaviour {

	private float rot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		gameObject.transform.eulerAngles = new Vector3(transform.rotation.x,transform.position.y+rot,transform.rotation.z);
		rot+=1.5f;
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.GetComponent<Player>() != null){
			Debug.Log("Collected");
			CollectibleManager cm = FindObjectOfType<CollectibleManager>();
			cm.GetManager(SceneManager.GetActiveScene().name).RemoveEntry(gameObject);
			Disappear();
		}
	}

	public void Appear(){
		gameObject.GetComponent<MeshRenderer>().enabled = true;
		gameObject.GetComponent<BoxCollider>().enabled = true;
	}

	public void Disappear(){
		gameObject.SetActive(false);
	}
}