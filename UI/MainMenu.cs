using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private UILabel _bestScoreText;

    public void OnClickStartButton() => GameManager.Scene.LoadScene(StringDefines.Strings.InGameScene);
    public void OnClickShopButton() => GameManager.Scene.LoadScene(StringDefines.Strings.ShopScene);
    public void OnClickExitButton() => UnityEditor.EditorApplication.isPlaying = false;
    public void SetBestScore() => _bestScoreText.text = $"{StringDefines.Strings.BestScoreText}{GameManager.Data.BestScore}";

    public void Start()
    {
        SetBestScore();
    }

}
