using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Score : MonoBehaviour
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
        if (_animate_text)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _TextScore.fontSize = 70;
                _animate_text = false;
                _timer = 0.25f;
            }
        }
    }

    public void Add_score()
    {
        _score += 1;
        _TextScore.text = _score.ToString();
        _animate_text = true;
        _TextScore.fontSize = 100;
    }
    public void CheckHighScore()
    {
        if (_score > _scorehig.first_score)
        {
            _scorehig.third_score = _scorehig.second_score;
            _scorehig.second_score = _scorehig.first_score;
            _scorehig.first_score = _score;
        }
        else if (_score > _scorehig.second_score)
        {
            _scorehig.third_score = _scorehig.second_score;
            _scorehig.second_score = _score;
        }
        else if (_score > _scorehig.third_score)
        {
            _scorehig.third_score = _score;
        }
    }

    public void EnableScore()
    {
        _TextScore.gameObject.SetActive(true);
    }
    
    public void DisableScore()
    {
        _TextScore.gameObject.SetActive(false);
    }
    public void EnableHighscore()
    {
        _TextHighscore.gameObject.SetActive(true);
    }
    
    public void DisableHighscore()
    {
        _TextHighscore.gameObject.SetActive(false);
    }
    public void Update_highscore()
    {
        _TextHighscore.text = "HighScore\n\n1. " + _scorehig.first_score.ToString() + "\n2. " + _scorehig.second_score.ToString() + "\n3. " + _scorehig.third_score.ToString();
    }
}
