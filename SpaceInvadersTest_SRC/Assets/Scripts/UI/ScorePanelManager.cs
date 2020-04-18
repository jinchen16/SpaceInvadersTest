using UnityEngine.UI;

public class ScorePanelManager : BasePanelManager
{
    public Text scoreText;

    public static ScorePanelManager instance;

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

    public void UpdateScoreText(int value)
    {
        scoreText.text = value.ToString();
    }
}
