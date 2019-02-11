using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 15;
    public GameObject explosion;
    public GameObject[] otherAsteroids;
    Transform player;
    enum AsteroidClass { BIG, MEDIUM, SMALL }
    [SerializeField]
    AsteroidClass asteroidClass;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        direction += directionToPlayer * 0.01f;
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void Explode()
    {
        int index = 0;
        float speedToApply = 40;
        if (asteroidClass == AsteroidClass.BIG)
        {
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
        Destroy(gameObject);
    }
}
