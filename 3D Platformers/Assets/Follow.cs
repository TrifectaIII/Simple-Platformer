using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
//https://www.youtube.com/watch?v=YtzIXCKr8Wo
public class Follow : MonoBehaviour
{
    private Vector3 offset;
    public float distance;
    public float yaw; 
	public float pitch; 
	public float mouseScale;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {	
		distance -= Input.mouseScrollDelta.y;
		distance = Mathf.Clamp(distance, 1.5f, 18);
		if (Input.GetMouseButton(1)){
			yaw += Input.GetAxis("Mouse X")* mouseScale;
			pitch -= Input.GetAxis("Mouse Y")* mouseScale;
			if (pitch > Mathf.PI/3){
				pitch = Mathf.PI/3;
			}
			else if (pitch < -Mathf.PI/3){
				pitch = -Mathf.PI/3;
			}
		}
		offset = new Vector3(Mathf.Sin(yaw)* Mathf.Cos(pitch),Mathf.Sin(pitch),Mathf.Cos(yaw) * Mathf.Cos(pitch))*distance;
		transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
		transform.LookAt(target);
		
    }
}
