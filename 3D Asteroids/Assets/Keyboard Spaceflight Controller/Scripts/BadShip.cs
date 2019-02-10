using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadShip : MonoBehaviour
{
    public GameObject ps1;
    public GameObject ps2;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Block.onTargetExplode += ShipHasExploded;
    }

    void ShipHasExploded()
    {
        StartCoroutine(ExplodeShip());
    }

    IEnumerator ExplodeShip()
    {
        yield return new WaitForSeconds(4.7f);
        audioSource.Play();
        yield return new WaitForSeconds(0.3f);
        ps1.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        ps2.SetActive(true);
    }
}
