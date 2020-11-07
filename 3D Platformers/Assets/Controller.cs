using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed;
	private ParticleSystem particles;
	private ParticleSystem.ShapeModule shape;
    public float jumpStren;
    public float gravityscale;
	public int jumpsMax;
	private int jumps;
	private bool dashing = false; 
	public float dashspeed;
	public float dashdur;
	public int dashesMax;
	private int dashes;
	Transform camera; 
    CharacterController cc;
    Vector3 moveDirect ;
	
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
		particles = GetComponent<ParticleSystem>();
		shape = particles.shape;
		camera = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {	
		if (Input.GetButtonDown("Dash") && (moveDirect.x != 0 || moveDirect.z != 0) && dashes > 0 && !dashing)
		{
			dashes -= 1;
			dashing = true;
			Invoke("StopDash",dashdur);
			
			moveDirect.y = 0; 
			moveDirect = moveDirect.normalized * dashspeed;
			particles.Play();
		}
		if(dashing)
		{
			
			moveDirect.y = 0; 
			moveDirect = moveDirect.normalized * dashspeed;
			float angle = Mathf.Rad2Deg*Mathf.Atan2(moveDirect.x, moveDirect.z) + 180;
			shape.rotation = new Vector3(0f,angle,0f);
			transform.rotation = Quaternion.Euler(moveDirect.x,0,moveDirect.z);
			Vector3 angles = transform.eulerAngles;
			angles.y += 90;
			transform.eulerAngles = angles; 
			transform.Rotate(90 * Vector3.up);
		}
		else
		{
			float moveDirectY = moveDirect.y;
			moveDirect = new Vector3(Input.GetAxis("Horizontal") * speed, 0.0f, Input.GetAxis("Vertical") * speed);
			moveDirect = camera.TransformDirection(moveDirect);
			moveDirect.y = moveDirectY;
			if (cc.isGrounded)
			{
				jumps = jumpsMax;
				moveDirect.y = 0.0f;
				dashes = dashesMax;
			}
			moveDirect.y += Physics.gravity.y * gravityscale * Time.deltaTime;
			
		}
		if (Input.GetButtonDown("Jump") && jumps > 0 )
		{
		  jumps -= 1;
		  moveDirect.y = jumpStren;
		  StopDash();
		}

		cc.Move(moveDirect * Time.deltaTime);
		
    }
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Moving Platform")
		{
			Debug.Log("Hello :)");
		}
		Debug.Log(other);
	} 
	void StopDash()
	{	
		dashing = false;
		particles.Stop();
		transform.rotation = Quaternion.Euler(0,0,0);
	}
}
