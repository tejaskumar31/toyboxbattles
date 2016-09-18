using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float movementSpeed = 10;
    public float turningSpeed = 60;

    public GameObject prefab;
    public GameObject projectileParticleEffectPrefab;
    public Transform projectileSpawn;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);

        if (Input.GetButtonDown("Fire1"))
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
