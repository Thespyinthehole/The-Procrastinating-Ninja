using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public float timeLimit;

    float time;
    int roundedtime;
    TextMesh timetext;
    PlayerMove player;

    public void resetTime()
    {
        time = timeLimit;
    }

    void Start()
    {
        timetext = GetComponent<TextMesh>();
        time = timeLimit;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    void FixedUpdate()
    {
        UpdateTimer();
        player.ChangeMultiplier(time, timeLimit);
    }

    void UpdateTimer()
    {
        time -= Time.deltaTime;
        roundedtime = (int)Mathf.Round(time);
        timetext.text = "Time: " + roundedtime.ToString();
        if (roundedtime <= 0) transform.parent.parent.parent.GetComponent<PlayerMove>().Die();
    }
}
