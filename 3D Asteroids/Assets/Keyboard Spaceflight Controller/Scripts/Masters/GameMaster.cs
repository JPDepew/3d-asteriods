using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public BadShip badShip;
    public GameObject playerShip;

    GameObject playerRef;

    int waveCount = 0;
    int currentMaxForBadShip = 0;
    int currentCountForBadShip = 0;


    void Start()
    {
        BadShip.onBadShipExplode += OnBadShipExplode;
        Spaceflight.onExplode += OnShipExplode;

        StartGame();
    }

    void StartGame()
    {
        playerRef = Instantiate(playerShip, transform.position, playerShip.transform.rotation);
        Instantiate(badShip, playerShip.transform.position + new Vector3(300, -50, 300), transform.rotation);
    }

    void OnShipExplode()
    {
        StartCoroutine(OnShipExplodeWait());
    }

    IEnumerator OnShipExplodeWait()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnBadShipExplode()
    {
        Data.Instance.score++;
        StartCoroutine(InstantiateShipDelay());
    }

    IEnumerator InstantiateShipDelay()
    {
        yield return new WaitForSeconds(5);
        Instantiate(badShip, playerRef.transform.position + 600 * playerRef.transform.right, transform.rotation);
    }

    private void OnDestroy()
    {
        BadShip.onBadShipExplode -= OnBadShipExplode;
        Spaceflight.onExplode -= OnShipExplode;
    }
}