using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    #region GAMESTATE
    public enum GameState
    {
        Ingame,

        Tutorial,
        Paused,

        Victory,
        Gameover,

        IngameIntro,
        Cinematic
    }
    #endregion

    #region SINGLETON

    /// <summary>
    ///  Force a avoir qu'un seul LevelManager
    /// </summary>
    [SerializeField] private static LevelManager _instance = null;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelManager>();
                // Si vrai, l'instance va etre cree
                if (_instance == null)
                {
                    var newObjectInstance = new GameObject();
                    newObjectInstance.name = typeof(LevelManager).ToString();
                    _instance = newObjectInstance.AddComponent<LevelManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    #region VARIABLE

    private GameState state;

    private void Start()
    {
        switch (state)
        {
            case GameState.Ingame:
                break;
            case GameState.Tutorial:
                break;
            case GameState.Paused:
                break;
            case GameState.Victory :
                break;
            case GameState.Gameover:
                break;
            case GameState.IngameIntro:
                break;
            case GameState.Cinematic:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion

    #region ASCESSEUR

    #endregion

    #region FUNCTION UNITY

    #endregion

    #region FUNCTION

    #endregion

}
