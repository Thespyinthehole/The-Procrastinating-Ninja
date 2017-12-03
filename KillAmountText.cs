using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillAmountText : MonoBehaviour {
	void Update () {
        Quests Quests = GameObject.Find("QuestSystem").GetComponent<Quests>();
        int killAmount = Quests.killamount - Quests.killed;
        GetComponent<Text>().text = "To Kill: " + killAmount;
	}
}
