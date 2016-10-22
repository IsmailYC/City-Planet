using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour {
    public int speed;

    int dir;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(-dir*speed* Time.deltaTime * Vector3.forward);
	}

    public void SetSpeed(int d)
    {
        dir = d;
    }
}
