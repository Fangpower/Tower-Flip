using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highscoreText;
    [SerializeField] TMP_Text coinText;
    [SerializeField] ParticleSystem recordPart;

    private float highscore;
    private float coins;
    private float curScore;
    private float prevScore;
    private bool playedPart;

    void Start()
    {
        coins = PlayerPrefs.GetFloat("Coins");
        coinText.text = coins.ToString();
        highscore = PlayerPrefs.GetFloat("HighScore");
        highscoreText.text = "Highscore: " + highscore.ToString();
    }

    void Update()
    {
        curScore = (int)(player.transform.position.y/2);
        if(curScore > prevScore){
            prevScore = (int)(player.transform.position.y/2);
            scoreText.text = ((int)player.transform.position.y/2).ToString();
        }
        if(curScore > highscore){
            highscoreText.text = "Highscore: " + curScore.ToString();
            if(!playedPart){
                recordPart.Play();
                playedPart = true;
            }
        }
    }

    public void Done(){
        highscore = (int)(player.transform.position.y/2);
        if(highscore > PlayerPrefs.GetFloat("HighScore")){
            PlayerPrefs.SetFloat("HighScore", highscore);
        }
        PlayerPrefs.SetFloat("Coins", coins);
    }

    public void AddCoin(){
        coins++;
        coinText.text = coins.ToString();
    }
}
