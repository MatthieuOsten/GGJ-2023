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

    private string _nameScenePause = "Pause", _nameSceneHighscore = "Highscore";

    #endregion

    #region ASCESSEUR

    #endregion

    #region FUNCTION UNITY

    private void Update()
    {
        switch (GameManager.Instance.State)
        {
            case GameManager.GameState.Ingame:
                if (Input.GetKeyDown(KeyCode.P))
                {
                    GameManager.Instance.State = GameManager.GameState.Paused;
                    GameManager.Instance.AdditiveScene(_nameScenePause);
                }
                break;
            case GameManager.GameState.Tutorial:
                break;
            case GameManager.GameState.Paused:
                if (Input.GetKeyDown(KeyCode.P))
                {
                    GameManager.Instance.State= GameManager.GameState.Ingame;
                    GameManager.Instance.UnloadScene(_nameScenePause);
                }
                break;
            case GameManager.GameState.Victory:
            case GameManager.GameState.Gameover:
                GameManager.Instance.ChangeScene(_nameSceneHighscore);
                break;
            case GameManager.GameState.IngameIntro:
                break;
            case GameManager.GameState.Cinematic:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            GameManager.Instance.QuitGame();
        }
    }

    #endregion

    #region FUNCTION

    #endregion

}
