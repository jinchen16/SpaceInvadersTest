using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScorePanelManager : BasePanelManager
{
    public static FinalScorePanelManager instance;

    public Text highestScoreText;
    public Text scoreText;

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

    public void UpdateText(int highest, int score)
    {
        highestScoreText.text = highest.ToString();
        scoreText.text = score.ToString();
    }

    public void OnRestartButtonPressed()
    {
        Hide();
        GameManager.instance.OnRestartingGame();
    }

    public void OnContinueButtonPressed()
    {
        Hide();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
