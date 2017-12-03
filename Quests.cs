using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Quests : MonoBehaviour {
    public bool KillQuest;
    public GameObject target;
    public bool placeQuest;
    public Transform targetPos;
    public bool enemyKillsQuest;
    public int killamount;

    public string NextLevel;
    public int killed;

    void Update()
    {
        if (KillQuest && target == null) CompleteLevel();
        if (placeQuest && CheckPosition()) CompleteLevel();
        if (enemyKillsQuest && CheckKills()) CompleteLevel();
    }

    bool CheckKills()
    {
        if (killed >= killamount) return true;
        return false;
    }

    bool CheckPosition()
    {
        if(Vector3.Distance(targetPos.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 1) return true;
        return false;
    }
    void CompleteLevel()
    {
        int level = PlayerPrefs.GetInt("Level", 0);
        PlayerPrefs.SetInt("Level", ++level);
        SceneManager.LoadScene(NextLevel);
    }

    public void EnemyKilled()
    {
        ++killed;
    }
}
