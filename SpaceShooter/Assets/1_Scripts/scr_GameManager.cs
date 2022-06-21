using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_GameManager : MonoBehaviour
{
    [SerializeField] [Header("�ĤH / �k��")] GameObject[] enemies;

    [SerializeField] [Header("��~����")] GameObject hpPotion_Obj;
    [SerializeField] [Header("�Ĥ@��~�ɶ�")] float hpPotionSpawnTime;
    [SerializeField] [Header("��~�ͦ����j")] float hpPotionInterval;

    [HideInInspector] [Header("�ĤH�ˮ`")] public float enemyDamage;
    [HideInInspector] [Header("���ľ�������")] public int hitEnemyScore;
    [HideInInspector] [Header("���k�۪�����")] public int hitAsterroidScore;

    [SerializeField] [Header("�]�w����")] GameObject setting_Page;
    [SerializeField] bool isSetting;

    [Header("���a��e��q")] public float playerCurrentHp;

    int totalScore;             // �`��

    float minX, maxX;           // ����X�b����
    float enemySpawnTime;       // ��l�ͦ��ɶ�
    float enemiesInterval;      // �ĤH�ͦ����j
    float playerMaxHp;          // ���a�̤j��q

    Text score_Text;            // ���Ƥ�r
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
        playerHpBar = GameObject.Find("HUD/��q/���").GetComponent<Image>();
        playerController = GameObject.Find("���a����").GetComponent<scr_PlayerController>();
        score_Text = GameObject.Find("HUD/����").GetComponent<Text>();
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
    /// ���a����
    /// </summary>
    public void HurtPlayer(float _damage)
    {
        playerCurrentHp -= _damage;

        playerHpBar.fillAmount = playerCurrentHp / playerMaxHp;

        if (playerCurrentHp <= 0) Dead();
    }

    /// <summary>
    /// ���a����
    /// </summary>
    public void HealPlayer(float _heal)
    {
        playerCurrentHp += _heal;
        playerCurrentHp = Mathf.Clamp(playerCurrentHp, 0, playerMaxHp);

        playerHpBar.fillAmount = playerCurrentHp / playerMaxHp;
    }

    /// <summary>
    /// �[��
    /// </summary>
    public void AddScore(int _score)
    {
        totalScore += _score;
        score_Text.text = "Score : " + totalScore;
    }

    /// <summary>
    /// �]�w�e��
    /// </summary>
    public void Setting()
    {
        isSetting = !isSetting;

        setting_Page.SetActive(isSetting);

        Time.timeScale = isSetting ? 0 : 1;
    }

    /// <summary>
    /// �^����
    /// </summary>
    public void ToLobby()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// ���s�C��
    /// </summary>
    public void Replay()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// �ͦ��ĤH
    /// </summary>
    void CreateEnemies()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(minX, maxX), 4f, -6f), transform.rotation);
    }

    /// <summary>
    /// �ͦ���~
    /// </summary>
    void CreateHpPotion()
    {
        Instantiate(hpPotion_Obj, new Vector3(Random.Range(minX, maxX), 4f, -6f), transform.rotation);
    }

    /// <summary>
    /// ���a���`
    /// </summary>
    void Dead()
    {
        scr_StaticVar.currentScore = totalScore;
        SceneManager.LoadScene("Gameover");
    }
}
