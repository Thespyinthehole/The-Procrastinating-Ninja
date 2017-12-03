using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArenaOption : MonoBehaviour {
    public int minpowerlevel;
    public int minmaxTime;
    public int minroundMultiplier;

    public Text powerlevel;
    public Text maxTime;
    public Text roundMultiplier;


    void Start()
    {
        powerlevel.text = PlayerPrefs.GetFloat("ArenaPower", 10).ToString();
        maxTime.text = PlayerPrefs.GetFloat("ArenaTime", 10).ToString();
        roundMultiplier.text = PlayerPrefs.GetFloat("ArenaScale", 1).ToString();
    }

    public void IncreasePower()
    {
        PlayerPrefs.SetFloat("ArenaPower", Mathf.Round(PlayerPrefs.GetFloat("ArenaPower", 10) + 10));
        powerlevel.text = PlayerPrefs.GetFloat("ArenaPower", 10).ToString();
    }
    public void IncreaseTime()
    {
        PlayerPrefs.SetFloat("ArenaTime", Mathf.Round(PlayerPrefs.GetFloat("ArenaTime", 10) + 10));
        maxTime.text = PlayerPrefs.GetFloat("ArenaTime", 10).ToString();
    }
    public void IncreaseMultiplier()
    {
        PlayerPrefs.SetFloat("ArenaScale", Mathf.Round((PlayerPrefs.GetFloat("ArenaScale", 1) + 0.01f)*100)/100);
        roundMultiplier.text = PlayerPrefs.GetFloat("ArenaScale", 1).ToString();
    }
    public void DecreasePower()
    {
        float power = PlayerPrefs.GetFloat("ArenaPower", 10);
        power -= 10;
        if (power < minpowerlevel) power = minpowerlevel;
        PlayerPrefs.SetFloat("ArenaPower", Mathf.Round(power));
        powerlevel.text = PlayerPrefs.GetFloat("ArenaPower", 10).ToString();
    }
    public void DecreaseTime()
    {
        float time = PlayerPrefs.GetFloat("ArenaTime", 10);
        time -= 10;
        if (time < minmaxTime) time = minmaxTime;
        PlayerPrefs.SetFloat("ArenaTime", Mathf.Round(time));
        maxTime.text = PlayerPrefs.GetFloat("ArenaTime", 10).ToString();
    }
    public void DecreaseMultiplier()
    {
        float scale = PlayerPrefs.GetFloat("ArenaScale", 1);
        scale -= 0.01f;
        if (scale < minroundMultiplier) scale = minroundMultiplier;
        PlayerPrefs.SetFloat("ArenaScale", Mathf.Round(scale*100)/100);
        roundMultiplier.text = PlayerPrefs.GetFloat("ArenaScale", 1).ToString();
    }
}
