﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 15;
    public GameObject explosion;
    public GameObject[] otherAsteroids;

    Vector3 rotationSpeed;
    Transform badShip;
    Transform player;
    AudioSource[] audioSources;
    public enum AsteroidClass { BIG, MEDIUM, SMALL }
    [SerializeField]
    AsteroidClass asteroidClass;
    bool destroyed = false;

    public delegate void OnAsteroidExplode(AsteroidClass asteroidClass);
    public static event OnAsteroidExplode onAsteroidExplode;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        if (tempPlayer != null)
        {
            player = tempPlayer.transform;
        }
        GameObject tempBadShip = GameObject.FindGameObjectWithTag("BadShip");
        if(tempBadShip != null)
        {
            badShip = tempBadShip.transform;
        }

        rotationSpeed = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    void LateUpdate()
    {
        if (badShip != null)
        {
            Vector3 directionToBadShip = badShip.transform.position - transform.position;
            if (directionToBadShip.magnitude < 150)
            {
                direction -= directionToBadShip.normalized * 0.02f;
            }
        }

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        direction += directionToPlayer * 0.01f;
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        transform.Rotate(rotationSpeed);
    }

    public void Explode()
    {
        int index = 0;
        float speedToApply = 40;

        if (asteroidClass == AsteroidClass.BIG)
        {
            if(onAsteroidExplode != null)
            {
                onAsteroidExplode(AsteroidClass.BIG);
            }
            index = Random.Range(2, 4);
        }
        else if (asteroidClass == AsteroidClass.MEDIUM)
        {
            speedToApply = 60;
            index = Random.Range(0, 2);
        }
        if (asteroidClass != AsteroidClass.SMALL)
        {
            Asteroid tempAsteroid1 = Instantiate(otherAsteroids[index], transform.position, transform.rotation).GetComponent<Asteroid>();
            tempAsteroid1.direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            tempAsteroid1.speed = speedToApply;
            Asteroid tempAsteroid2 = Instantiate(otherAsteroids[index], transform.position, transform.rotation).GetComponent<Asteroid>();
            tempAsteroid2.direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            tempAsteroid2.speed = speedToApply;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block" && !destroyed)
        {
            destroyed = true;
            other.GetComponent<Block>().Explode();
            Explode();
        }
    }
}
