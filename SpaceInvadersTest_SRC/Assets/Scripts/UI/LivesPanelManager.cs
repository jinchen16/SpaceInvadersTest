using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesPanelManager : BasePanelManager
{
    public static LivesPanelManager instance;
    
    public Image[] livesImages;
    
    private void Awake()
    {
        instance = this;    
    }

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void ShowAllLives()
    {
        for (int i = 0; i < livesImages.Length; i++)
        {
            livesImages[i].gameObject.SetActive(true);
        }
    }

    public void LoseALive(int index)
    {
        if (index >= livesImages.Length) // Length proof
            return;
        livesImages[index].gameObject.SetActive(false);
    }
}
