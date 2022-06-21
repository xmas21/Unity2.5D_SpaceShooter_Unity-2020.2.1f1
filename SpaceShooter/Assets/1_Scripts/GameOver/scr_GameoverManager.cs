using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_GameoverManager : MonoBehaviour
{
    [SerializeField] [Header("目前得分")] Text currentScore_Text;
    [SerializeField] [Header("最高得分")] Text highScore_Text;
    string highestScore;

    void Awake()
    {
        highestScore = "highestScore";
    }

    void Start()
    {
        RecordhighestScore();
        Initialize();
    }

    void Initialize()
    {
        currentScore_Text.text = "本次得分 : " + scr_StaticVar.currentScore;
        highScore_Text.text = "最高得分 : " + PlayerPrefs.GetInt(highestScore);
    }

    /// <summary>
    /// 紀錄最高分數
    /// </summary>
    void RecordhighestScore()
    {
        if (PlayerPrefs.HasKey(highestScore))
        {
            if (PlayerPrefs.GetInt(highestScore) < scr_StaticVar.currentScore) PlayerPrefs.SetInt(highestScore, scr_StaticVar.currentScore);
        }
        else
        {
            PlayerPrefs.SetInt(highestScore, scr_StaticVar.currentScore);
        }
    }

    /// <summary>
    /// 回首頁
    /// </summary>
    public void ToLobby()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// 回遊戲
    /// </summary>
    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
