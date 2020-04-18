using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance;

    private void Awake()
    {
        instance = this;
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt("score");
    }

    public void SetScore(int value)
    {
        if (value >= 0)
        {
            PlayerPrefs.SetInt("score", value);
        }
        else
        {
            PlayerPrefs.SetInt("score", 0);
        }
    }
}
