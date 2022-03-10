using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject screen;
    public Text scoreText, finalScoreText, highScoreText, newText, scoreGainText;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void EndGame()
    {
        if (ScoreCounter.instance.score > PlayerPrefs.GetInt("High Score", 0))
        {
            PlayerPrefs.SetInt("High Score", ScoreCounter.instance.score);
            newText.gameObject.SetActive(true);
        }
        else newText.gameObject.SetActive(false);
        AudioManager.instance.Stop("Idle");
        //AudioManager.instance.Stop("BGM");
        scoreText.gameObject.SetActive(false);
        scoreGainText.gameObject.SetActive(false);
        finalScoreText.text = ScoreCounter.instance.score.ToString();
        highScoreText.text = PlayerPrefs.GetInt("High Score", 0).ToString();
        screen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
