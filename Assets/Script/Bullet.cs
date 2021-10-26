using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Rigidbody2D rb;
	public float speed;
	public float destroyTime;
	
	void Start()
	{
		rb.velocity = transform.right * speed;
	}
   void OnTriggerEnter2D(Collider2D col)
   {
	   Destroy(col.gameObject);
	   Destroy(gameObject, destroyTime);
	   
	   
   }
}
