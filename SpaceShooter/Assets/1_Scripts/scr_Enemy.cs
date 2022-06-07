using UnityEngine;

public class scr_Enemy : MonoBehaviour
{
    [SerializeField] [Header("�l�u")] GameObject bullet_obj;
    [SerializeField] [Header("�l�u�ͦ��I")] Transform bullet_trans;
    [SerializeField] [Header("�l�u�ͦ����j")] float bulletInterval;
    [SerializeField] [Header("�O�_���԰���")] bool isFighter;

    float deleteTime; // �R���ɶ�
    float moveSpeed;  // ���ʳt��

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
    /// ��l��
    /// </summary>
    void Initialize()
    {
        deleteTime = 5f;
        moveSpeed = 1f;
    }

    /// <summary>
    /// ����
    /// </summary>
    void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// �g��
    /// </summary>
    void Shoot()
    {
        GameObject temp = Instantiate(bullet_obj, bullet_trans.position, bullet_trans.rotation);
    }
}
