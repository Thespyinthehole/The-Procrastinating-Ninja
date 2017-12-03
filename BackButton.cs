using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
