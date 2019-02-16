using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject explosion;
    public bool canExplode = true;
    public bool targetBlock = false;

    private bool allExplode = true;

    public delegate void OnTargetExplode();
    public static event OnTargetExplode onTargetExplode;

    private void Start()
    {
        onTargetExplode += ShipExplode;
    }

    public void Explode(bool shouldEmitParticles)
    {
        if (canExplode)
        {
            if (targetBlock && onTargetExplode != null)
            {
                onTargetExplode();
            }
            int shouldExplode = 1;
            if (!allExplode)
            {
                shouldExplode = Random.Range(0, 7);
            }
            if (shouldExplode == 1 && shouldEmitParticles)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        onTargetExplode -= ShipExplode;
    }

    void ShipExplode()
    {
        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        float randomWait = Random.Range(1f, 5.5f);
        float randomWait2 = Random.Range(5f, 5.5f);
        float actualWait = Random.Range(0, 2) == 1 ? randomWait : randomWait2;
        yield return new WaitForSeconds(actualWait);
        canExplode = true;
        allExplode = false;
        Explode(true);
    }
}
