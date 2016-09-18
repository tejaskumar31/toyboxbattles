/*  Written by William Castellano 
	This script allows for a game to have collectible items that don't reappear
	in scenes in which said colelctibles have been collected.
	Colectible game objects must all be tagged "Collectible" in the scene and must have a Collectible class attached.
	The Collectible Manager itself must be present in each scene in which there are collectibles.*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Lists
using UnityEngine.SceneManagement; //Scene information
using System.Linq; //Extension of Lists (ElementAt(int))

public class CollectibleManager : MonoBehaviour {

	static List<Manager> managers = new List<Manager>(); //Dynamic list of Managers. Managers handle the collectibles of a designated scene.
	
	// Use this for initialization
	void Start () {
		Manager levelManager = GetManager(SceneManager.GetActiveScene().name); //Try to find a Manager with a name equal to that of the scene.
		if(levelManager==null){ //If no such Manager exists...
			CreateManager(); //Create one
		}
		else{ //If a Manager is found...
			levelManager.PrimeCollectibles(); //Have it rady the collectibles for the player
		}
		//PresentCollectibles();
	}

	public void CreateManager(){ //Adds a new manager to the Manager list, naming it after the scene which it serves.
		managers.Add(new Manager(SceneManager.GetActiveScene().name));
	}

	public Manager GetManager(string n){ //Retrieves a Manager by name from the Manager list, if one exists.
		for(int i = 0; i<managers.Count; i++){
			if(managers.ElementAt(i).name == n){
				return managers.ElementAt(i);
			}
		}
		return null; //No Manager with a name matching the argument.
	}

	public class Manager{ //Stores a list of game objects, which are the collectibles of a level. 

		public string name; //Name for identification purposes.
		List<GameObject> collectibles = new List<GameObject>(); //This manager's individual list of collectibles.

		public Manager(string n){ //Creates a new Manager, setting it's name to the argument and populating it's collectible list.
			name = n;
			InitializeManager();
		}

		public void InitializeManager(){ //Finds all the collectibles in the scene, puts them in a list, and makes them appear for the player.
			if(GameObject.FindGameObjectsWithTag("Collectible")!=null){
				GameObject[] cls = GameObject.FindGameObjectsWithTag("Collectible");
				for(int i = 0; i < cls.Length; i++){
					collectibles.Add(cls[i]);
					cls[i].GetComponent<Collectible>().Appear();
				}
			}
		}

		public void RemoveEntry(GameObject obj){ //Removes a collectible from the list (For when the player collects it)
			collectibles.Remove(obj);
		}

		public void PrimeCollectibles(){ //Makes all collectibles on the list appear for the player.
			for(int i = 0; i<collectibles.Count; i++){
				collectibles.ElementAt(i).GetComponent<Collectible>().Appear();
			}
		}

		public void DebugCollectibles(){ //Prints the names of all collectibles on the list in the console.
			for(int i = 0; i<collectibles.Count; i++){
				Debug.Log(collectibles.ElementAt(i).gameObject.name);
			}
		}
	}
		
	public void PresentCollectibles(){ //For testing purposes; prints out information
			Debug.Log("The following are objects the player has yet to collect in this scene:");
			Manager levelManager = GetManager(SceneManager.GetActiveScene().name);
			levelManager.DebugCollectibles();
	}
	
}