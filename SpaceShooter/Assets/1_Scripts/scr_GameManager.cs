using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_GameManager : MonoBehaviour
{
    [SerializeField] [Header("敵人 / 隕石")] GameObject[] enemies;

    [SerializeField] [Header("血瓶物件")] GameObject hpPotion_Obj;
    [SerializeField] [Header("第一血瓶時間")] float hpPotionSpawnTime;
    [SerializeField] [Header("血瓶生成間隔")] float hpPotionInterval;

    [HideInInspector] [Header("敵人傷害")] public float enemyDamage;
    [HideInInspector] [Header("打敵機的分數")] public int hitEnemyScore;
    [HideInInspector] [Header("打隕石的分數")] public int hitAsterroidScore;

    [SerializeField] [Header("設定頁面")] GameObject setting_Page;
    [SerializeField] bool isSetting;

    [Header("玩家當前血量")] public float playerCurrentHp;

    int totalScore;             // 總分

    float minX, maxX;           // 場景X軸限制
    float enemySpawnTime;       // 初始生成時間
    float enemiesInterval;      // 敵人生成間隔
    float playerMaxHp;          // 玩家最大血量

    Text score_Text;            // 分數文字
    Image playerHpBar;
    scr_PlayerController playerController;

    #region - MonoBehaviour -
    void Awake()
    {
        GC();
    }

    void Start()
    {
        Initialize();

        InvokeRepeating("CreateEnemies", enemySpawnTime, enemiesInterval);
        InvokeRepeating("CreateHpPotion", hpPotionSpawnTime, hpPotionInterval);
    }

    void GC()
    {
        playerHpBar = GameObject.Find("HUD/血量/血條").GetComponent<Image>();
        playerController = GameObject.Find("玩家飛機").GetComponent<scr_PlayerController>();
        score_Text = GameObject.Find("HUD/分數").GetComponent<Text>();
    }

    void Initialize()
    {
        minX = -1.4f;
        maxX = 1.4f;

        enemySpawnTime = 1.5f;
        enemiesInterval = 0.8f;
        enemyDamage = 10f;

        hitEnemyScore = 10;
        hitAsterroidScore = 20;

        isSetting = false;

        playerMaxHp = playerController.hp;
        playerCurrentHp = playerMaxHp;
        Time.timeScale = 1;
    }
    #endregion

    /// <summary>
    /// 玩家受傷
    /// </summary>
    public void HurtPlayer(float _damage)
    {
        playerCurrentHp -= _damage;

        playerHpBar.fillAmount = playerCurrentHp / playerMaxHp;

        if (playerCurrentHp <= 0) Dead();
    }

    /// <summary>
    /// 玩家受傷
    /// </summary>
    public void HealPlayer(float _heal)
    {
        playerCurrentHp += _heal;
        playerCurrentHp = Mathf.Clamp(playerCurrentHp, 0, playerMaxHp);

        playerHpBar.fillAmount = playerCurrentHp / playerMaxHp;
    }

    /// <summary>
    /// 加分
    /// </summary>
    public void AddScore(int _score)
    {
        totalScore += _score;
        score_Text.text = "Score : " + totalScore;
    }

    /// <summary>
    /// 設定畫面
    /// </summary>
    public void Setting()
    {
        isSetting = !isSetting;

        setting_Page.SetActive(isSetting);

        Time.timeScale = isSetting ? 0 : 1;
    }

    /// <summary>
    /// 回首頁
    /// </summary>
    public void ToLobby()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// 重新遊戲
    /// </summary>
    public void Replay()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// 生成敵人
    /// </summary>
    void CreateEnemies()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(minX, maxX), 4f, -6f), transform.rotation);
    }

    /// <summary>
    /// 生成血瓶
    /// </summary>
    void CreateHpPotion()
    {
        Instantiate(hpPotion_Obj, new Vector3(Random.Range(minX, maxX), 4f, -6f), transform.rotation);
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    void Dead()
    {
        scr_StaticVar.currentScore = totalScore;
        SceneManager.LoadScene("Gameover");
    }
}
