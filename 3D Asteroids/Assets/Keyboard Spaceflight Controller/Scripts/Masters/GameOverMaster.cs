using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMaster : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        scoreText.text = "Ships destroyed: " + Data.Instance.score.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Data.Instance.ResetScore();
            SceneManager.LoadScene(1);
        }
    }
}
