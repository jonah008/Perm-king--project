using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Rigidbody2D rb;
	public float speed;
	
	
	void Start()
	{
		rb.velocity = transform.up * -speed;
	}
   void OnTriggerEnter2D(Collider2D col)
   {
	   Destroy(col.gameObject);
	   Destroy(gameObject);
	   
	   
   }
}
