using UnityEngine;

public class scr_Bullet : MonoBehaviour
{
    [SerializeField] [Header("�l�u���ʳt��")] float moveSpeed;
    [SerializeField] [Header("�R���ɶ�")] float deleteTime;
    [SerializeField] [Header("����ĤH�S��")] GameObject hitEnemy_VFX;
    [SerializeField] [Header("���쪱�a�S��")] GameObject hitPlayer_VFX;
    [SerializeField] [Header("����P�y�S��")] GameObject hitAsterroid_VFX;

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
    /// �줸��
    /// </summary>
    void GC()
    {
        gameManager = GameObject.Find("GamaManager").GetComponent<scr_GameManager>();
    }

    /// <summary>
    /// ��l��
    /// </summary>
    void Initialize()
    {
    }

    /// <summary>
    /// ����
    /// </summary>
    void Move()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
