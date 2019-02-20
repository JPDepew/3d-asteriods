using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public BadShip badShip;
    public GameObject playerShip;
    public Text exitText;

    GameObject playerRef;

    int waveCount = 0;
    int currentMaxForBadShip = 0;
    int currentCountForBadShip = 0;

    public enum GameState { RUNNING, PAUSED };
    public GameState gameState;

    void Start()
    {
        BadShip.onBadShipExplode += OnBadShipExplode;
        Spaceflight.onExplode += OnShipExplode;

        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameState.RUNNING)
            {
                exitText.gameObject.SetActive(true);
                gameState = GameState.PAUSED;
                Time.timeScale = 0;
                AudioListener.volume = 0;
            }
            else
            {
                exitText.gameObject.SetActive(false);
                gameState = GameState.RUNNING;
                Time.timeScale = 1;
                AudioListener.volume = 1;
            }
        }
        if(Input.GetKeyDown(KeyCode.N) && gameState == GameState.PAUSED)
        {
            exitText.gameObject.SetActive(false);
            gameState = GameState.RUNNING;
            Time.timeScale = 1;
            AudioListener.volume = 1;
        }
        if(Input.GetKeyDown(KeyCode.Y) && gameState == GameState.PAUSED)
        {
            AudioListener.volume = 1;
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }

    void StartGame()
    {
        playerRef = Instantiate(playerShip, transform.position, playerShip.transform.rotation);
        Instantiate(badShip, playerShip.transform.position + new Vector3(600, -50, 300), transform.rotation);
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
        Instantiate(badShip, playerRef.transform.position + 1000 * playerRef.transform.right, transform.rotation);
    }

    private void OnDestroy()
    {
        BadShip.onBadShipExplode -= OnBadShipExplode;
        Spaceflight.onExplode -= OnShipExplode;
    }
}