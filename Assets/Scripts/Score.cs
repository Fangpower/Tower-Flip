using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highscoreText;
    [SerializeField] TMP_Text coinText;
    [SerializeField] ParticleSystem recordPart;
    [SerializeField] Button reviveButton;
    [SerializeField] TMP_Text reviveText;

    private float highscore;
    public float coins;
    private float curScore;
    private float prevScore;
    private bool playedPart;
    private int cost = 10;

    void Start()
    {
        //coins = PlayerPrefs.GetFloat("Coins");
        coinText.text = coins.ToString();
        highscore = PlayerPrefs.GetFloat("HighScore");
        highscoreText.text = "Highscore: " + highscore.ToString();
    }

    void Update()
    {
        curScore = (int)(player.transform.position.y/2);
        if(curScore > prevScore && player.GetComponent<Player>().state == Player.State.Alive){
            prevScore = (int)(player.transform.position.y/2);
            scoreText.text = ((int)player.transform.position.y/2).ToString();
        }
        if(curScore > highscore && player.GetComponent<Player>().state == Player.State.Alive){
            highscoreText.text = "Highscore: " + curScore.ToString();
            if(!playedPart){
                recordPart.Play();
                playedPart = true;
            }
        }

        reviveButton.interactable = coins >= cost;
    }

    public void Done(){
        //highscore = (int)(player.transform.position.y/2);
        if((int)(player.transform.position.y/2) > PlayerPrefs.GetFloat("HighScore")){
            PlayerPrefs.SetFloat("HighScore", prevScore);
        }
        PlayerPrefs.SetFloat("Coins", coins);
    }

    public void AddCoin(){
        coins++;
        coinText.text = coins.ToString();
    }

    public void RemoveCoin(){
        coins-=cost;
        cost += 5;
        reviveText.text = cost.ToString();
    }
}
