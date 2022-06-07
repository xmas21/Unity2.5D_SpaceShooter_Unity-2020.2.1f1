using UnityEngine;

public class scr_GameManager : MonoBehaviour
{
    [SerializeField] [Header("�ĤH / �k��")] GameObject[] enemies;

    float minX, maxX;           // ����X�b����
    float firstTime;            // ��l�ͦ��ɶ�
    float enemiesInterval;      // �ĤH�ͦ����j

    void Start()
    {
        Initialize();

        InvokeRepeating("CreateEnemies", firstTime, enemiesInterval);
    }

    /// <summary>
    /// ��l��
    /// </summary>
    void Initialize()
    {
        minX = -1.4f;
        maxX = 1.4f;
        firstTime = 1.5f;
        enemiesInterval = 0.8f;
    }

    /// <summary>
    /// �ͦ��ĤH
    /// </summary>
    void CreateEnemies()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(Random.Range(minX, maxX), 4f, -6f), transform.rotation);
    }
}
