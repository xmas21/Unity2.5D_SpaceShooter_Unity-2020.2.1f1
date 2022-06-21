using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_GameoverManager : MonoBehaviour
{
    [SerializeField] [Header("�ثe�o��")] Text currentScore_Text;
    [SerializeField] [Header("�̰��o��")] Text highScore_Text;
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
        currentScore_Text.text = "�����o�� : " + scr_StaticVar.currentScore;
        highScore_Text.text = "�̰��o�� : " + PlayerPrefs.GetInt(highestScore);
    }

    /// <summary>
    /// �����̰�����
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
    /// �^����
    /// </summary>
    public void ToLobby()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// �^�C��
    /// </summary>
    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
