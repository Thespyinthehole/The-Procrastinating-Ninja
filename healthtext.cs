using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthtext : MonoBehaviour
{
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            PlayerMove player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
            GetComponent<Text>().text = "Health: " + player.health + "/" + player.maxHealth;
        }
        else
        {
            GetComponent<Text>().text = "";
        }
    }
}
