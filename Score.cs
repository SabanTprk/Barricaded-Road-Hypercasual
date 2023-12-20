using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public TextMeshProUGUI loseScorePanel;
    public TextMeshProUGUI winScorePanel;
    public TextMeshProUGUI highScoreText;
    public AudioSource audioSource;
    public AudioClip jump;
    public AudioClip coin;
    public AudioClip lose;
    public AudioClip impact;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        HightScore();
    }


    public void jumpSound()
    {
        audioSource.clip = jump;
        audioSource.Play();
    }

    public void coinSound()
    {
        audioSource.clip = coin;
        audioSource.Play();
    }

    public void loseSound()
    {
        audioSource.clip = lose;
        audioSource.Play();
    }

    public void impactSound()
    {
        audioSource.clip = impact;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("token"))
        {
            Destroy(other.gameObject);
            score += 4;
            scoreRestart();
            coinSound();


        }

        else if (other.gameObject.CompareTag("obstacles"))
        {
            score -= 2;
            scoreRestart();
            Destroy(other.gameObject);
            impactSound();
            FindObjectOfType<Player>().ChangeMaterial();


        }
    }

    void scoreRestart()
    {
        scoreText.text = score.ToString();
    }

    void scorePanelWrite()
    {
        loseScorePanel.text = "Total Score: " + score.ToString();
        winScorePanel.text = "Total Score: " + score.ToString();
    }

    private void Update()
    {
        if(score > PlayerPrefs.GetInt("highScore")){
        PlayerPrefs.SetInt("highScore",score);
        }

    }

    private void HightScore()
    {
        highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
    }
}
