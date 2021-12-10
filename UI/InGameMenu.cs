using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    private static InGameMenu _gameMenu;

    public static InGameMenu GameMenu
    {
        get
        {
            if (_gameMenu == null) _gameMenu = FindObjectOfType<InGameMenu>();
            return _gameMenu;
        }
    }

    [SerializeField]
    private UISlider _healthBar;
    [SerializeField]
    private UILabel _scoreText;
    [SerializeField]
    private UILabel _coinCountText;
    [SerializeField]
    private GameObject _gameOverMenu;
    [SerializeField]
    private GameObject[] _lifes;
    [SerializeField]
    private GameObject[] _Booms;

    public void Awake()
    {
        InGameManager.Game.OnPlayerDead += () => InGameMenu.GameMenu.UpdateLifes();
    }

    public void AddBoom()
    {
        _Booms[InGameManager.Game.BoomCount - 1].SetActive(true);
    }

    public void RemoveBoom()
    {
        _Booms[InGameManager.Game.BoomCount - 1].SetActive(false);
    }

    public void UpdateScore()
    {
        _scoreText.text = string.Format("{0:D6}", InGameManager.Game.Score);
    }

    public void UpdateLifes()
    {
        _lifes[InGameManager.Game.Life].SetActive(false);
    }

    public void UpdateHealthBar(int hp, int maxHp)
    {
        _healthBar.value = (float)hp / maxHp;
    }

    public void SetGameOverMenu()
    {
        _coinCountText.text = $"{StringDefines.Strings.GainedCoin}{InGameManager.Game.Coin}";
        _gameOverMenu.SetActive(true);
        GameManager.Data.Money += InGameManager.Game.Coin;
        if (GameManager.Data.BestScore < InGameManager.Game.Score)
            GameManager.Data.BestScore = InGameManager.Game.Score;
    }

    public void OnClickExit()
    {
        GameManager.Scene.LoadScene(StringDefines.Strings.MainMenuScene);
    }

    public void OnClickReplay()
    {
        GameManager.Scene.LoadScene(StringDefines.Strings.InGameScene);
    }
}
