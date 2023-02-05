using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// List of Canvas of the scene 
    /// </summary>
    [Tooltip("The element 0 is the first display")]
    [SerializeField] private List<GameObject> listOfCanvas = new List<GameObject>();

    [SerializeField] private bool _quitWithDownKey = false;
    [SerializeField] private KeyCode _quitAfterDownKey = KeyCode.Space;
    [SerializeField] private string _nameSceneToQuitAfterDownKey = "MainMenu";

    private void Start()
    {
        // Display first panel
        ChangePanel(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_quitAfterDownKey) && _quitWithDownKey)
        {
            GameManager.Instance.ChangeScene(_nameSceneToQuitAfterDownKey);
        }
    }

    /// <summary>
    /// Recupere l'index dans "listOfPanels" du panneau designer par un nom par rapport au nom de l'objet
    /// </summary>
    /// <param name="index">Index encore inconnu</param>
    /// <param name="name">Nom du canvas rechercer</param>
    /// <returns>Retourne si le panneau as ete retrouver ou non</returns>
    private bool TryGetPanelIndexFromName(out int index, string name)
    {
        // Recherche dans la liste des canvas
        for (int i = 0; i < listOfCanvas.Count; i++)
        {
            // Si le nom du canvas correspond a l'entree alors le retourne et retourne vrai
            if (listOfCanvas[i].name == name) {
                index = i;
                return true;
            }
        }

        // Sinon l'index est egal a 0 et il retourne faux
        index = 0;
        return false;
    }

    /// <summary>
    /// Change de panneau par rapport a son nom
    /// </summary>
    /// <param name="name">Nom du panneau que l'on veut afficher</param>
    public void ChangePanel(string name)
    {
        int index = 0;

        if (TryGetPanelIndexFromName(out index, name))
        {
            Debug.Log("Panel : " + name + " is actived");
            ChangePanel(index);
        }
        else
        {
            Debug.LogWarning("Panel : " + name + " is not found");
        }
    }

    /// <summary>
    /// Change de panneau par rapport a son index dans "listOfPanels"
    /// </summary>
    /// <param name="index">Index du panneau dans la liste</param>
    public void ChangePanel(int index)
    {
        if (listOfCanvas.Count == 0) {
            Debug.LogWarning("List of panel is Empty");
            return; 
        }

        for (int i = 0; i < listOfCanvas.Count; i++)
        {
            if (index == i) { listOfCanvas[i].SetActive(true); }
            else { listOfCanvas[i].SetActive(false); }
        }
    }

    /// <summary>
    /// Change scene
    /// </summary>
    /// <param name="name">Name of new scene</param>
    public void ChangeScene(string name)
    {
        ChangeScene(name);
    }

    /// <summary>
    /// Quit application of game
    /// </summary>
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

}
