using UnityEngine;
using System.Collections;

public class ProjectileSpawn : MonoBehaviour {

    public GameObject prefab;
    public GameObject projectileParticleEffectPrefab;
    public Transform projectileSpawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            SpawnProjectile();
        }
	
	}

    void SpawnProjectile()
    {
        Instantiate(projectileParticleEffectPrefab, projectileSpawn.position, Quaternion.identity);
        GameObject projectile = Instantiate(prefab, projectileSpawn.position, projectileSpawn.rotation) as GameObject;
        projectile.GetComponent<Rigidbody>().AddForce(projectileSpawn.forward * 1000);
    }
}
