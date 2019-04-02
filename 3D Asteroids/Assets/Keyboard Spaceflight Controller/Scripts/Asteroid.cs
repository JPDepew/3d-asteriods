using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 15;
    public GameObject explosion;
    public GameObject otherAsteroid;

    Vector3 rotationSpeed;
    Transform badShip;
    Transform player;
    public enum AsteroidClass { BIG, MEDIUM, SMALL }
    [SerializeField]
    AsteroidClass asteroidClass;
    bool destroyed = false;

    public delegate void OnAsteroidExplode(AsteroidClass asteroidClass);
    public static event OnAsteroidExplode onAsteroidExplode;

    void Start()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        if (tempPlayer != null)
        {
            player = tempPlayer.transform;
        }
        GameObject tempBadShip = GameObject.FindGameObjectWithTag("BadShip");
        if (tempBadShip != null)
        {
            badShip = tempBadShip.transform;
        }

        rotationSpeed = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    void LateUpdate()
    {
        if (Time.timeScale > 0)
        {
            if (badShip != null)
            {
                Vector3 directionToBadShip = badShip.transform.position - transform.position;
                if (directionToBadShip.magnitude < 150)
                {
                    direction -= directionToBadShip.normalized * 0.02f;
                }
            }

            if (player != null)
            {
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
                direction += directionToPlayer * 0.01f;
            }
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            transform.Rotate(rotationSpeed);
        }
    }

    public void Explode()
    {
        if (!destroyed)
        {
            destroyed = true;
            float speedToApply = 40;

            // This is to keep track of the # of asteroids in a scene
            onAsteroidExplode?.Invoke(asteroidClass);
            if (asteroidClass == AsteroidClass.MEDIUM)
            {
                speedToApply = 60;
            }
            if (asteroidClass != AsteroidClass.SMALL)
            {
                Asteroid tempAsteroid1 = Instantiate(otherAsteroid, transform.position, transform.rotation).GetComponent<Asteroid>();
                tempAsteroid1.direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                tempAsteroid1.speed = speedToApply;
                Asteroid tempAsteroid2 = Instantiate(otherAsteroid, transform.position, transform.rotation).GetComponent<Asteroid>();
                tempAsteroid2.direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                tempAsteroid2.speed = speedToApply;
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            other.GetComponent<Block>()?.Explode(false);
            Explode();
        }
    }
}
