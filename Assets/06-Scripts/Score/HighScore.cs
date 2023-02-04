using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHighScore", menuName = "HighScore")]
public class HighScore : ScriptableObject
{
    public float first_score;
    public float second_score;
    public float third_score;
}
