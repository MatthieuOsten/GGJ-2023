using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHighScore", menuName = "HighScore")]
public class HighScore : ScriptableObject
{
    [SerializeField] private Score _lastScore;
    [SerializeField] private List<Score> _scores;

    [System.Serializable]
    public class Score
    {
        [SerializeField] private string _name;
        [SerializeField] private int _id;
        [SerializeField] private float _score;
        [SerializeField] private DateTime _time;

        public string Name { get { return _name; } }
        public int Id { get { return _id; } }
        public float ScoreValue { 
            get { return _score; } 
            set { _score = (value < 0) ? 0 : value; }
        }

        public DateTime Time { 
            get { return _time; } 
            set { _time = value; }
        }

        public Score(string name, int id,float score, DateTime time)
        {
            _name = name;
            _score = score;
            _time = time;
            _id = id;

            ScoreValue = score;
            Time = time;
        }

        public override string ToString()
        {
            return string.Format("{0} | {1} as score of {2} make in {3}", Id, Name, ScoreValue,Time.ToString());
        }
    }

    public Score LastScore
    {
        get { return _lastScore; }
        set {
            _lastScore = new Score(value.Name,-1,value.ScoreValue,DateTime.Now); 
        }
    }

    public List<Score> AllData
    { 
        get { return _scores; }
    }

    public List<float> AllScores {
        get {
            List<float> floats= new List<float>();

            foreach (Score score in AllData)
            {
                floats.Add(score.ScoreValue);
            }

            return floats; 
        }
    }

    public Score[] TopThree
    {
        get
        {
            Score[] tempTab = new Score[3];

            UpdateOrderOfHighScore();
            tempTab[0] = AllData[AllData.Count - 1];
            tempTab[1] = AllData[AllData.Count - 2];
            tempTab[2] = AllData[AllData.Count - 3];

            return tempTab;
        }
    }

    public void UpdateOrderOfHighScore()
    {
        string debugList = "List -> ";
        foreach (var item in _scores)
        {
            debugList += item.ScoreValue.ToString() + " | ";
        }
        Debug.Log(debugList);

        int n = _scores.Count;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (_scores[j].ScoreValue < _scores[minIndex].ScoreValue)
                {
                    minIndex = j;
                }
            }
            Score temp = _scores[minIndex];
            _scores[minIndex] = _scores[i];
            _scores[i] = temp;
        }

        debugList = "List -> ";
        foreach (var item in _scores)
        {
            debugList += item.ScoreValue.ToString() + " | ";
        }
        Debug.Log("Tri : " + debugList);
    }

    public void AddNewScore(string name, float score)
    {
        Score newScore = new Score(name, GetNewID(), score, System.DateTime.Now);
        Debug.Log(newScore.ToString());
        _scores.Add(newScore);
    }

    public void AddNewScore(float score)
    {
        Score newScore = new Score("Default", GetNewID(), score, System.DateTime.Now);
        Debug.Log(newScore.ToString());
        _scores.Add(newScore);
    }

    public void AddNewScore(Score newScore)
    {
        newScore = new Score(newScore.Name, GetNewID(), newScore.ScoreValue, System.DateTime.Now);
        Debug.Log(newScore.ToString());
        _scores.Add(newScore);
    }

    public void AddNewScore()
    {
        Score newScore = new Score(LastScore.Name, GetNewID(), LastScore.ScoreValue, System.DateTime.Now);
        Debug.Log(newScore.ToString());
        _scores.Add(newScore);
    }

    private int GetNewID()
    {
        int id = 0;

        foreach (var item in AllData)
        {
            if (item.Id > id)
            {
                id = item.Id;
            }
        }

        return id++;
    }

    private Score GetTopScore()
    {
        Score tempScore = new Score("NULL", -1,0, DateTime.Now);

        foreach (var score in AllData)
        {
            if (score.ScoreValue > tempScore.ScoreValue)
            {
                return score;
            }
        }

        return tempScore;
    }

    private Score GetTopScore(float limit)
    {
        Score tempScore = new Score("NULL", -1, 0, DateTime.Now);

        foreach (var score in AllData)
        {
            if (score.ScoreValue > tempScore.ScoreValue && score.ScoreValue < limit)
            {
                return score;
            }
        }

        return tempScore;
    }

    private int GetEgalsScoreCount(float comparingScore)
    {
        int nbrOfEgals = 0;

        foreach (var score in AllScores)
        {
            if (score == comparingScore) { nbrOfEgals++; }
        }

        return nbrOfEgals;
    }

}
