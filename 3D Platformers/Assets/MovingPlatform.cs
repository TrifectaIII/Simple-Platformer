using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{	
	public Vector3 pos1;
	public Vector3 pos2;
	public float moveDur;
	private bool moving = false;
	private Vector3 difference;
	
	//Duration is half a lap
    // Start is called before the first frame update
    void Start()
    {
		transform.position = pos1;
		difference = pos2 - pos1;
    }

    // Update is called once per frame
    void Update()
    {
        float timepassed = (Time.time % (moveDur * 2))/moveDur;
		if(timepassed > 1)
		{
			timepassed = 2 - timepassed;
		}
		transform.position = pos1 + difference * timepassed;
    }
}
