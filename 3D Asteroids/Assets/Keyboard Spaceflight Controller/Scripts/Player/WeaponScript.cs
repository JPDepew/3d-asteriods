using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public GameObject[] shotSpawns;
    public GameObject[] bombSpawns;
    public GameObject shot;
    public GameObject bomb;
    public float timeBetweenShots = 0.1f;
    public float timeBetweenBombs = 0.3f;

    AudioSource[] audioSource;
    bool canShoot = true;
    bool canBomb = true;

    void Start()
    {
        audioSource = GetComponents<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (canShoot)
            {
                foreach (GameObject ss in shotSpawns)
                {
                    audioSource[0].Play();
                    Instantiate(shot, ss.transform.position, ss.transform.rotation);
                }
                canShoot = false;
                StartCoroutine(ShootDelay());
            }
        }
        if (Input.GetMouseButton(1))
        {
            if (canBomb)
            {
                foreach (GameObject bs in bombSpawns)
                {
                    audioSource[1].Play();
                    Instantiate(bomb, bs.transform.position, bs.transform.rotation);
                }
                canBomb = false;
                StartCoroutine(BombDelay());
            }
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    IEnumerator BombDelay()
    {
        yield return new WaitForSeconds(timeBetweenBombs);
        canBomb = true;
    }
}
