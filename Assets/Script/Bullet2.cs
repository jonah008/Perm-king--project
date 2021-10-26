using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
	public Rigidbody2D rb;
	public float speed;
	public int destroyTime;
	
	
	
	void Start()
	{
		rb.velocity = transform.right * -speed;
	}
   void OnTriggerEnter2D(Collider2D col)
   {
	   Destroy(col.gameObject);
	   Destroy(gameObject, destroyTime);
	   
	   
   }
   

    
}
