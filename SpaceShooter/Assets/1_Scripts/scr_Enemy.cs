using UnityEngine;

public class scr_Enemy : MonoBehaviour
{
    [SerializeField] [Header("子彈")] GameObject bullet_obj;
    [SerializeField] [Header("子彈生成點")] Transform bullet_trans;
    [SerializeField] [Header("子彈生成間隔")] float bulletInterval;
    [SerializeField] [Header("是否為戰鬥機")] bool isFighter;

    float deleteTime; // 刪除時間
    float moveSpeed;  // 移動速度

    void Start()
    {
        Initialize();
        Destroy(gameObject, deleteTime);

        if (isFighter) InvokeRepeating("Shoot", 1f, bulletInterval);
    }

    void Update()
    {
        Move();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        deleteTime = 5f;
        moveSpeed = 1f;
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 射擊
    /// </summary>
    void Shoot()
    {
        GameObject temp = Instantiate(bullet_obj, bullet_trans.position, bullet_trans.rotation);
    }
}
