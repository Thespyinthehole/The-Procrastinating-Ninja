using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenemanange : MonoBehaviour {
    public List<string> levelnames;

    Transform loadLevelButton;
    void Start()
    {
        loadLevelButton = GameObject.Find("Canvas").transform.GetChild(3);
        if (PlayerPrefs.GetInt("Level", 0) != 0) {
            loadLevelButton.gameObject.SetActive(true);
            loadLevelButton.GetChild(0).GetComponent<Text>().text = "Load: " + levelnames[PlayerPrefs.GetInt("Level")];
        }
    }

    public void Arena()
    {
        SceneManager.LoadScene("Arena");
    }

    public void howtoplay()
    {
        SceneManager.LoadScene("HowTo");
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("Level", 0);
        SceneManager.LoadScene(levelnames[0]);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(levelnames[PlayerPrefs.GetInt("Level")]);
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
