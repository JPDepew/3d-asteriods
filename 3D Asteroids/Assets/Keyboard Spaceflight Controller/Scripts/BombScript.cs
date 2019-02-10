using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float speed = 20f;

    public GameObject blast;

    public float lifespan = 5f;

    Vector3 direction;

    void Start()
    {
        Destroy(gameObject, lifespan);
        direction = (transform.forward - transform.up).normalized;
    }


    void Update()
    {
        direction -= transform.up * 0.05f;
        direction.Normalize();
        transform.position += direction * Time.deltaTime * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
            other.GetComponent<Block>().Explode();
        Instantiate(blast, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
