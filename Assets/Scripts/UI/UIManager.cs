using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private bool panel_showed;

    public GameObject canvas_endGame;
    public Text text_endGame;

    public HealthBar health_bar;

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshUI()
    {
        if (GameManager.player != null)
        {
            health_bar.RefreshBar(GameManager.player.GetComponent<Actor>().health);
        }
    }
    
    public void ShowWinPanel()
    {
        if (panel_showed == true)
            return;

        text_endGame.text = "You win!";
        canvas_endGame.SetActive(true);
        panel_showed = true;
    }

    public void ShowLosePanel()
    {
        if (panel_showed == true)
            return;

        text_endGame.text = "You lose!";
        canvas_endGame.SetActive(true);
        panel_showed = true;
    }

    private void HidePanel()
    {
        canvas_endGame.SetActive(false);
        panel_showed = false;
    }

    public void OnClick_Restart()
    {
        HidePanel();
        GameManager.Instance.RestartGame();
    }
}
