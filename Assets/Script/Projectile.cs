using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public static Projectile Instance;
    public GameObject Prefabs;

	// Use this for initialization
	void Awake () {
        Projectile.Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ProjectileCube()
    {
        GameObject temp = GameObject.Instantiate<GameObject>(Prefabs);
        Vector3 RandomVec3 = Random.onUnitSphere;
        RandomVec3.y = Random.Range(10, 15);
        temp.GetComponent<Rigidbody>().velocity = RandomVec3;
    }
}
