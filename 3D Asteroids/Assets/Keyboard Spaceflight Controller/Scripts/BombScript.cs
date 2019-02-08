using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float speed = 20f;

    public GameObject blast;

    public float lifespan = 5f;

    void Start()
    {
        Destroy(gameObject, lifespan);
    }


    void FixedUpdate()
    {
        transform.position += (transform.forward - transform.up).normalized * Time.fixedDeltaTime * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
            other.GetComponent<Block>().Explode();
        Instantiate(blast, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
