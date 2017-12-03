using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeLimiter : MonoBehaviour {
    public int timeLimit;

    float time;
    int roundedtime;
    TextMesh timetext;

	void Start () {
        timetext = GetComponent<TextMesh>();
        time = timeLimit;
	}

    void FixedUpdate()
    {
        UpdateTimer();
        ChangeTime();
    }

    void UpdateTimer()
    {
        time -= Time.deltaTime;
        roundedtime = (int)Mathf.Round(time);
        timetext.text = "Time: " + roundedtime.ToString();
    }

    void ChangeTime()
    {
        int difference = 200 - roundedtime;
        Time.fixedDeltaTime = difference * 0.002f;
        Time.timeScale = difference;
        Debug.Log(Time.fixedDeltaTime);
    }
}
