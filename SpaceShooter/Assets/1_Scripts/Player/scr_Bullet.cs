using UnityEngine;

public class scr_Bullet : MonoBehaviour
{
    [SerializeField] [Header("子彈移動速度")] float moveSpeed;
    [SerializeField] [Header("打到敵人特效")] GameObject hitEnemy_VFX;
    [SerializeField] [Header("打到玩家特效")] GameObject hitPlayer_VFX;
    [SerializeField] [Header("打到星球特效")] GameObject hitAsterroid_VFX;

    float deleteTime; // 刪除時間

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
        if (gameObject.tag == "Player" && col.gameObject.tag == "Enemy")
        {
            Instantiate(hitEnemy_VFX, col.transform.position, transform.rotation);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if (gameObject.tag == "Player" && col.gameObject.tag == "Asterroid")
        {
            Instantiate(hitAsterroid_VFX, col.transform.position, transform.rotation);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        if (gameObject.tag == "Enemy" && col.gameObject.tag == "Player")
        {
            Instantiate(hitPlayer_VFX, col.transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        deleteTime = 1f;
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Move()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
