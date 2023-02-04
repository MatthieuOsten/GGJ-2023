using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

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

    #endregion

    #region ASCESSEUR

    #endregion

    #region FUNCTION UNITY

    private void Start()
    {
        switch (GameManager.Instance.State)
        {
            case GameManager.GameState.Ingame:
                break;
            case GameManager.GameState.Tutorial:
                break;
            case GameManager.GameState.Paused:
                break;
            case GameManager.GameState.Victory:
                break;
            case GameManager.GameState.Gameover:
                break;
            case GameManager.GameState.IngameIntro:
                break;
            case GameManager.GameState.Cinematic:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #endregion

    #region FUNCTION

    #endregion

}
