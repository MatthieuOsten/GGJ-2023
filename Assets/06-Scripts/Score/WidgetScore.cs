using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class WidgetScore : MonoBehaviour
{
    private float _score;
    private bool _animate_text;
    private float _timer = 0.25f;
    [SerializeField] private TMP_Text _TextScore;
    [SerializeField] private TMP_Text _TextHighscore;
    [SerializeField] private HighScore _scorehig;
    
    void Start()
    {
        _score = 0;
        _animate_text = false;
        DisableHighscore();
    }

    private void Update()
    {
        if (_animate_text && _TextScore != null)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _TextScore.fontSize = 70;
                _animate_text = false;
                _timer = 0.25f;
            }
        }

        DisplayTopThree();

    }

    public void Add_score()
    {
        _score += 1;

        if (_TextScore != null)
        {
            _TextScore.text = _score.ToString();
            _animate_text = true;
            _TextScore.fontSize = 100;
        }

    }

    private void DisplayTopThree()
    {
        if (_TextHighscore != null)
        {
            string text = "Highscore\n";
             text += "Top 1 : " + _scorehig.TopThree[0].ToString() + "\n";
             text += "Top 2 : " + _scorehig.TopThree[1].ToString() + "\n";
             text += "Top 3 : " + _scorehig.TopThree[2].ToString() + "\n";

            _TextHighscore.text = text;
        }
    }

    //public void CheckHighScore()
    //{
    //    if (_score > _scorehig.TopThree[0])
    //    {
    //        _scorehig.third_score = _scorehig.second_score;
    //        _scorehig.second_score = _scorehig.first_score;
    //        _scorehig.first_score = _score;
    //    }
    //    else if (_score > _scorehig.second_score)
    //    {
    //        _scorehig.third_score = _scorehig.second_score;
    //        _scorehig.second_score = _score;
    //    }
    //    else if (_score > _scorehig.third_score)
    //    {
    //        _scorehig.third_score = _score;
    //    }
    //}

    public void UpdateLastScore()
    {
        _scorehig.LastScore.ScoreValue = _score;
    }

    public void AddDebug()
    {
        _scorehig.AddNewScore("Debug",UnityEngine.Random.Range(-99,1000));
    }

    public void AddLastScore()
    {
        UpdateLastScore();
        _scorehig.AddNewScore(_scorehig.LastScore);
        _scorehig.UpdateOrderOfHighScore();
    }

    public void EnableScore()
    {
        if (_TextScore != null)
        {
            _TextScore.gameObject.SetActive(true);
        }
    }
    
    public void DisableScore()
    {
        if (_TextScore != null)
        {
            _TextScore.gameObject.SetActive(false);
        }
    }
    public void EnableHighscore()
    {
        if (_TextHighscore != null)
        {
            _TextHighscore.gameObject.SetActive(true);
        }
    }
    
    public void DisableHighscore()
    {
        if (_TextHighscore != null)
        {
            _TextHighscore.gameObject.SetActive(false);
        }

    }
    public void Update_highscore()
    {
        if (_TextHighscore != null)
        _TextHighscore.text = "HighScore\n\n1. " + _scorehig.TopThree[0].ToString() + "\n2. " + _scorehig.TopThree[1].ToString() + "\n3. " + _scorehig.TopThree[2].ToString();
    }
}
