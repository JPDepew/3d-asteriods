using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadShip : MonoBehaviour
{
    public GameObject ps1;
    public GameObject ps2;
    public GameObject asteroid;
    public Transform shootPos;

    public float waitBetweenShots = 5;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Block.onTargetExplode += ShipHasExploded;

        StartCoroutine(ShootAsteroids());
    }

    private void OnDestroy()
    {
        Block.onTargetExplode -= ShipHasExploded;
    }

    void ShipHasExploded()
    {
        StartCoroutine(ExplodeShip());
        Block.onTargetExplode -= ShipHasExploded;
    }

    IEnumerator ExplodeShip()
    {
        yield return new WaitForSeconds(4.7f);
        audioSource.Play();
        yield return new WaitForSeconds(0.3f);
        ps1.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        ps2.SetActive(true);
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        for(int i = 0; i < asteroids.Length; i++)
        {
            Destroy(asteroids[i].gameObject);
            yield return null;
        }
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }

    IEnumerator ShootAsteroids()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitBetweenShots);
            asteroid = Instantiate(asteroid, shootPos.position, asteroid.transform.rotation);
            asteroid.GetComponent<Asteroid>().direction = Vector3.up;
        }
    }
}
