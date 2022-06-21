using UnityEngine;

public class scr_Bullet : MonoBehaviour
{
    [SerializeField] [Header("子彈移動速度")] float moveSpeed;
    [SerializeField] [Header("刪除時間")] float deleteTime;
    [SerializeField] [Header("打到敵人特效")] GameObject hitEnemy_VFX;
    [SerializeField] [Header("打到玩家特效")] GameObject hitPlayer_VFX;
    [SerializeField] [Header("打到星球特效")] GameObject hitAsterroid_VFX;

    scr_GameManager gameManager;


    void Awake()
    {
        GC();
    }

    void Start()
    {
        Initialize();
        Destroy(gameObject, deleteTime);
    }

    void Update()
    {
        Move();
    }

    void OnTriggerEnter(Collider col)
    {
        if (gameObject.tag == "Player Bullet" && col.gameObject.tag == "Enemy")
        {
            Instantiate(hitEnemy_VFX, col.transform.position, transform.rotation);
            gameManager.AddScore(gameManager.hitEnemyScore);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if (gameObject.tag == "Player Bullet" && col.gameObject.tag == "Asterroid")
        {
            Instantiate(hitAsterroid_VFX, col.transform.position, transform.rotation);
            gameManager.AddScore(gameManager.hitAsterroidScore);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

        if (gameObject.tag == "Enemy" && col.gameObject.tag == "Player")
        {
            gameManager.HurtPlayer(gameManager.enemyDamage);
            // Instantiate(hitPlayer_VFX, col.transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (gameObject.tag == "Asterroid" && col.gameObject.tag == "Player")
        {
            gameManager.HurtPlayer(gameManager.playerCurrentHp);
            Instantiate(hitPlayer_VFX, col.transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 抓元件
    /// </summary>
    void GC()
    {
        gameManager = GameObject.Find("GamaManager").GetComponent<scr_GameManager>();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Move()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
