using UnityEngine;

public class scr_Bullet : MonoBehaviour
{
    [SerializeField] [Header("�l�u���ʳt��")] float moveSpeed;
    [SerializeField] [Header("����ĤH�S��")] GameObject hitEnemy_VFX;
    [SerializeField] [Header("���쪱�a�S��")] GameObject hitPlayer_VFX;
    [SerializeField] [Header("����P�y�S��")] GameObject hitAsterroid_VFX;

    float deleteTime; // �R���ɶ�

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
    /// ��l��
    /// </summary>
    void Initialize()
    {
        deleteTime = 1f;
    }

    /// <summary>
    /// ����
    /// </summary>
    void Move()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
