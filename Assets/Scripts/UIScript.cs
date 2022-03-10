using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    PlayerController player;
    public static UIScript instance;
    public Image[] bulletUI;
    public Color full;
    public Color empty;
    public Text scoreText;
    public Color moreThanFive;
    public Color moreThanTen;
    public Text scoreGainText;
    bool showingScoreGain;
    bool wantToShowScoreGain;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;    
    }

    // Update is called once per frame
    void Update()
    {
        if (!wantToShowScoreGain && showingScoreGain)
        {
            scoreGainText.color = Color.LerpUnclamped(scoreGainText.color, empty, Time.deltaTime);
            if(scoreGainText.color.a <= 0.01)
            {
                scoreGainText.text = "";
                showingScoreGain = false;
            }
        }
    }

    public void UpdateBullets()
    {
        for (int i = 0; i < bulletUI.Length; i++)
        {
            //Debug.Log(i);
            if(i >= player.shootCounter)
            {
                bulletUI[i].color = empty;
            }

            else
            {
                bulletUI[i].color = full;
            }
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + ScoreCounter.instance.score;
    }

    public void UpdateScoreGain(int scoreGain, int multiplier)
    {
        wantToShowScoreGain = true;
        scoreGainText.color = full;
        scoreGainText.text = scoreGain + " x " + multiplier;
        if(multiplier >= 10)
        {
            scoreGainText.color = moreThanTen;
            scoreGainText.gameObject.GetComponent<Animator>().SetTrigger("Shake");
        }

        else if(multiplier >= 5)
        {
            scoreGainText.color = moreThanFive;
            scoreGainText.gameObject.GetComponent<Animator>().SetTrigger("Shake");
        }
        showingScoreGain = true;
        wantToShowScoreGain = false;
    }
}
