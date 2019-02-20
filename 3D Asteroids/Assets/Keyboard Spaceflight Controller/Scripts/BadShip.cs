using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadShip : MonoBehaviour
{
    public GameObject ps1;
    public GameObject ps2;
    public GameObject asteroid;
    public Transform shootPos;

    public int maxAsteroids = 10;

    public float waitBetweenShots = 5;

    AudioSource[] audioSource;

    int currentAsteroidsCount = 0;
    bool dead = false;

    public delegate void OnBadShipExplode();
    public static event OnBadShipExplode onBadShipExplode;

    void Start()
    {
        audioSource = GetComponents<AudioSource>();

        Asteroid.onAsteroidExplode += AsteroidHasBeenDestroyed;
        Block.onTargetExplode += ShipHasExploded;

        StartCoroutine(ShootAsteroids());
    }

    private void OnDestroy()
    {
        Asteroid.onAsteroidExplode -= AsteroidHasBeenDestroyed;
        Block.onTargetExplode -= ShipHasExploded;
    }

    void AsteroidHasBeenDestroyed(Asteroid.AsteroidClass asteroidClass)
    {
        if (asteroidClass == Asteroid.AsteroidClass.BIG)
        {
            currentAsteroidsCount++;
        }
        else if (asteroidClass == Asteroid.AsteroidClass.MEDIUM)
        {
            currentAsteroidsCount++;
        }
        else if (asteroidClass == Asteroid.AsteroidClass.SMALL)
        {
            currentAsteroidsCount--;
        }
    }

    void ShipHasExploded()
    {
        StartCoroutine(ExplodeShip());
        Block.onTargetExplode -= ShipHasExploded;
    }

    IEnumerator ExplodeShip()
    {
        dead = true;
        yield return new WaitForSeconds(4.7f);
        audioSource[1].Stop();
        audioSource[0].Play();
        yield return new WaitForSeconds(0.3f);
        ps1.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        ps2.SetActive(true);

        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        for (int i = 0; i < asteroids.Length; i++)
        {
            Destroy(asteroids[i].gameObject);
            yield return null;
        }
        onBadShipExplode?.Invoke();
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }

    IEnumerator ShootAsteroids()
    {
        while (!dead)
        {
            yield return new WaitForSeconds(waitBetweenShots);
            if (currentAsteroidsCount < maxAsteroids)
            {
                currentAsteroidsCount++;
                GameObject tempAsteroid = Instantiate(asteroid, shootPos.position, asteroid.transform.rotation);
                tempAsteroid.GetComponent<Asteroid>().direction = Vector3.up;
            }
        }
    }
}
