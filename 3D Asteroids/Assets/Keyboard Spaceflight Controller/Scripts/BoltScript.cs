﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour {

	public float speed = 100f;

	public GameObject blast;

	public float lifespan = 5f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifespan);
	}
	

	void FixedUpdate () {
		transform.position += transform.forward * Time.fixedDeltaTime * speed;
	}

	void OnTriggerEnter(Collider other) {
		Instantiate (blast,transform.position,transform.rotation);
        if (other.tag == "Block")
            other.GetComponent<Block>().Explode();
        if (other.tag == "Asteroid")
        {
            other.GetComponent<Asteroid>().Explode();
        }
        Destroy(gameObject);
	}
}
