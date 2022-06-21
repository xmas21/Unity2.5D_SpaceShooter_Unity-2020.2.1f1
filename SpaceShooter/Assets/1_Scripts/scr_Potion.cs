using UnityEngine;

public class scr_Potion : MonoBehaviour
{
    float moveSpeed;        //���ʳt��
    float deleteTime;       //�R���ɶ�
    float healValue;        // �ɦ�q

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
        moveSpeed = 2f;
        deleteTime = 6f;
        healValue = 20f;
    }

    /// <summary>
    /// ����
    /// </summary>
    void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
