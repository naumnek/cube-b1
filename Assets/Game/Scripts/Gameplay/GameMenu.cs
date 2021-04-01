using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public ControllerPlayer cPlayer;
    public GameObject bReload;
    public List<GameObject> UI = new List<GameObject> {};

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void gameover()
    {
        foreach(GameObject copy in UI)
        {
            copy.SetActive(false);
        }
        bReload.SetActive(true);
    }

    void Update()
    {
        if (cPlayer.run == false)
        {
            gameover();
        }
    }
}
