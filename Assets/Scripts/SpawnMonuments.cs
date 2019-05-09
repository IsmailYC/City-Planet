using UnityEngine;
using System.Collections;

public class SpawnMonuments : MonoBehaviour {
    public GameObject[] monuments;
    public float secondsBetweenSpawns;

    int count;
	// Use this for initialization
	void Start () {
        count = 0;
        Spawn();
	}

    void Spawn()
    {
        Instantiate(monuments[count], transform.position, transform.rotation);
        count++;
        if (count < monuments.Length)
            Invoke("Spawn", secondsBetweenSpawns);
    }
}
