using UnityEngine;

public class scr_GameManager : MonoBehaviour
{
    [SerializeField] [Header("敵人 / 隕石")] GameObject[] enemies;

    float minX, maxX;           // 場景X軸限制
    float firstTime;            // 初始生成時間
    float enemiesInterval;      // 敵人生成間隔

    void Start()
    {
        Initialize();

        InvokeRepeating("CreateEnemies", firstTime, enemiesInterval);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        minX = -1.4f;
        maxX = 1.4f;
        firstTime = 1.5f;
        enemiesInterval = 0.8f;
    }

    /// <summary>
    /// 生成敵人
    /// </summary>
    void CreateEnemies()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(minX, maxX), 4f, -6f), transform.rotation);
    }
}
