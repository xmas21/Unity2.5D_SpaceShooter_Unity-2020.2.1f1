using UnityEngine;

public class scr_Potion : MonoBehaviour
{
    float moveSpeed;        //移動速度
    float deleteTime;       //刪除時間
    float healValue;        // 補血量

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
        if (col.gameObject.tag == "Player")
        {
            gameManager.HealPlayer(healValue);
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
        moveSpeed = 2f;
        deleteTime = 6f;
        healValue = 20f;
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
