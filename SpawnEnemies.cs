using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class enemytype
{
    public GameObject Enemy;
    public int minRound;
    public int powerAmount;
}

public class SpawnEnemies : MonoBehaviour {
    public float maxX;
    public float maxUpY;
    public float maxDownY;
    public float powerlevel;
    public float maxTime;
    public float roundMultiplier;
    public List<enemytype> enemytypes;

    int round = 0;
    public List<GameObject> spawnedEnemies;
    bool doneSpawning;
    bool spawning;
    int currentpowerlevel;
    GameObject player;
    GameObject timer;

    void Start()
    {
        powerlevel = PlayerPrefs.GetFloat("ArenaPower", 10);
        maxTime = PlayerPrefs.GetFloat("ArenaTime", 60);
        roundMultiplier = PlayerPrefs.GetFloat("ArenaScale", 10);
        player = GameObject.FindGameObjectWithTag("Player");
        timer = player.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        NewRound();
    }

    void Update()
    {
        if (doneSpawning && spawnedEnemies.Count == 0) NewRound();
    }

    void NewRound()
    {
        powerlevel *= roundMultiplier;
        maxTime *= roundMultiplier;
        roundMultiplier *= roundMultiplier;
        ++round;
        spawning = true;
        doneSpawning = false;
        currentpowerlevel = 0;
        player.GetComponent<PlayerMove>().health = player.GetComponent<PlayerMove>().maxHealth;
        timer.GetComponent<Timer>().timeLimit = maxTime;
        timer.GetComponent<Timer>().resetTime();
        Spawn();
    }

    public void RemoveEnemy(GameObject toremove)
    {
        spawnedEnemies.Remove(toremove);
    }

    void Spawn()
    {
        while (!doneSpawning)
        {
            int times = 0;
            bool searching = true;

            while (searching && times < 10)
            {
                int choice = Random.Range(0, enemytypes.Count);
                if (currentpowerlevel + enemytypes[choice].powerAmount <= powerlevel && enemytypes[choice].minRound < round)
                {
                    Vector3 pos = new Vector3(Random.Range(-maxX, maxX), Random.Range(maxDownY, maxUpY), 0);
                    GameObject spawned = Instantiate(enemytypes[choice].Enemy, pos, Quaternion.identity) as GameObject;
                    spawnedEnemies.Add(spawned);
                    searching = false;
                    currentpowerlevel += enemytypes[choice].powerAmount;
                }
                ++times;
            }
            if (currentpowerlevel == powerlevel) doneSpawning = true;
            if (times == 10) doneSpawning = true;
        }
    }
}
