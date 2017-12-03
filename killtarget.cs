using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class killtarget : MonoBehaviour
{
    void Start()
    {
        Quests Quests = GameObject.Find("QuestSystem").GetComponent<Quests>();
        GetComponent<Text>().text = "To Kill: " + Quests.target.name;
    }
}
