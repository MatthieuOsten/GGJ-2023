using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region GAMESTATE
    public enum GameState
    {
        Menu, // Menu phase just navigate in MainMenu 
        Ingame, // Game is lanched

        Tutorial, // Tutorial is now dont have the control or is limited
        Paused, // Dont control player and other objects is stoped

        Victory, // Everythings is stoped, next part of game is dicted by this state
        Gameover, // Everythings is stoped, next part of game is dicted by this state 

        IngameIntro, // The introGame is disable player controller but game is playing and show things to player
        Cinematic // Control player is totally stoped and other object is stoped before end of cinematic possibly passed
    }
    #endregion

    #region SINGLETON

    /// <summary>
    ///  Force a avoir qu'un seul GameManager
    /// </summary>
    [SerializeField] private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                // Si vrai, l'instance va etre cree
                if (_instance == null)
                {
                    var newObjectInstance = new GameObject();
                    newObjectInstance.name = typeof(GameManager).ToString();
                    _instance = newObjectInstance.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    #region VARIABLE

    [SerializeField] private GameState _state = GameState.IngameIntro;

    [SerializeField] private static int _defaultFrameRate = 60;

    [SerializeField] private List<string> _scenes = new List<string>();

    public bool forcechange;

    #endregion

    #region ASCESSEUR

    public GameState State { 
        get { return _state; }
        set { _state = value; }
    }

    #endregion

    #region FUNCTION UNITY

    private void Start()
    {
        ChangeFrameRate(_defaultFrameRate);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        _scenes.Clear();
        _scenes = new List<string>();

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            _scenes.Add(SceneManager.GetSceneByBuildIndex(i).name);
        }
    }

#endif

    #endregion

    #region FUNCTION

    public void ChangeFrameRate(int framerate)
    {
        Application.targetFrameRate = framerate;
    }

    /// <summary>
    /// Change the scene
    /// </summary>
    /// <param name="name">Name of scene loaded</param>
    public void ChangeScene(string name)
    {
        if (!SceneIsExist(name) && !forcechange) { return; }

        // If this scene is not entry scene also load new scene
        if (SceneManager.GetActiveScene().name != name)
            SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Change the scene but if for refresh actual scene also load this scene
    /// </summary>
    /// <param name="name">Name of scene loaded</param>
    /// <param name="refreshScene">If True ignore the name scene is egal of this scene</param>
    public void ChangeScene(string name, bool refreshScene)
    {
        if (!SceneIsExist(name) && !forcechange) { return; }

        // If this scene is not entry scene also load new scene
        if (SceneManager.GetActiveScene().name != name || refreshScene)
            SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Add scene to game 
    /// </summary>
    /// <param name="name">Nom de la scene rechercher</param>
    public void AdditiveScene(string name)
    {
        if (!SceneIsExist(name) && !forcechange) { return; }

        if (SceneManager.GetSceneByName(name).isLoaded == false)
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    /// <summary>
    /// Add scene to game 
    /// </summary>
    /// <param name="name">Nom de la scene rechercher</param>
    public void UnloadScene(string name)
    {
        if (!SceneIsExist(name) && !forcechange) { return; }

        if (SceneManager.GetSceneByName(name).isLoaded == true && SceneManager.sceneCount > 1)
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(name).buildIndex);
    }

    private bool SceneIsExist(string name)
    {
        // Verify validity of name entry
        if (SceneManager.sceneCountInBuildSettings > 1)
        {
            bool isValid = false;

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string actualSceneName = SceneManager.GetSceneByBuildIndex(i).name;

                // If scene name correspond also break loop and continue function else continue loop for search scene
                if (actualSceneName == name) { isValid = true; break; }
                else { Debug.Log("Scene " + actualSceneName + " is not " + name); }

            }

            // If is not valid return function
            if (!isValid)
            {
                Debug.LogWarning("The scene " + name + " is not found.");
                return false;
            }
        }
        else
        {
            Debug.LogError("Build Setting dont have scene");
            return false;

        }

        return true;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
