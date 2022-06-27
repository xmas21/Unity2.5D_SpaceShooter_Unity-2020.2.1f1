using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerController : MonoBehaviour
{
    #region - Fields -
    [Header("PC �C���ޱ��覡")] public PCmoveway pcMoveway;
    [Header("Mobile �C���ޱ��覡")] public Mobilemoveway mobilemoveway;

    [Header("��q")] public float hp;

    [SerializeField] [Header("�n�쪫��")] GameObject joystick_obj;
    [SerializeField] [Header("�l�u����")] GameObject bullet_obj;
    [SerializeField] [Header("�l�u�ͦ��I")] Transform bullet_trans;

    [SerializeField] [Header("�l�u�ͦ����j")] float bulletInterval;

    [SerializeField] [Header("�ޱ��覡���")] Dropdown moveway_Dropdown;

    float X_min, X_max;      // ���� X�b�W�U��
    float Y_min, Y_max;      // ���� Y�b�W�U��
    float moveSpeed;         // ���ʳt��

    bool mouseTouchPlayer;   // �ƹ� / ����O�_���쪱�a
    bool isJoysticked;       // �O�_�I����n��

    Vector3 mousePos;        // �ƹ��y��
    #endregion

    #region - MonoBehaviour -
    void Start()
    {
        Initialize();

        InvokeRepeating("Shoot", 1f, bulletInterval);
    }

    void Update()
    {
        Move(moveSpeed);
    }

    void OnMouseDown()
    {
        mouseTouchPlayer = true;
    }

    void OnMouseUp()
    {
        mouseTouchPlayer = false;
    }
    #endregion

    #region - Unity Event -
    /// <summary>
    /// ���U�n��
    /// </summary>
    public void IsUsingJoystick()
    {
        isJoysticked = true;
    }

    /// <summary>
    /// ��}�n��
    /// </summary>
    public void UnUsingJoystick()
    {
        isJoysticked = false;
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// ���ʷn��
    /// </summary>
    public void UsingJoystick(Vector3 pos)
    {
        if (isJoysticked) transform.Translate(pos.x * moveSpeed * Time.deltaTime, 0f, pos.y * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// �����ޱ��覡
    /// </summary>
    public void ChangeMoveway()
    {
        switch (moveway_Dropdown.value)
        {
            case 0:
                pcMoveway = PCmoveway.mouse;
                break;
            case 1:
                pcMoveway = PCmoveway.keyboard;
                break;
            case 2:
                pcMoveway = PCmoveway.joystick;
                break;
        }
    }

    /// <summary>
    /// ��l��
    /// </summary>
    void Initialize()
    {
        X_min = -1.42f;
        X_max = 1.42f;
        Y_min = -0.9f;
        Y_max = 2.7f;

        moveSpeed = 2.5f;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="_speed">���ʳt��</param>
    void Move(float _speed)
    {
#if UNITY_STANDALONE_WIN
        switch (pcMoveway)
        {
            case PCmoveway.mouse:
                joystick_obj.SetActive(false);
                if (mouseTouchPlayer)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = new Vector3(mousePos.x, mousePos.y, -6f);
                }
                break;
            case PCmoveway.keyboard:
                joystick_obj.SetActive(false);
                transform.Translate(Input.GetAxisRaw("Horizontal") * Time.deltaTime * _speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * _speed);
                break;
            case PCmoveway.joystick:
                joystick_obj.SetActive(true);
                break;
        }
#endif
#if UNITY_ANDROID
        switch (mobilemoveway)
        {
            case Mobilemoveway.acceleration:
                joystick_obj.SetActive(false);
                transform.Translate(Input.acceleration.x * Time.deltaTime * _speed, 0, Input.acceleration.y * Time.deltaTime * _speed); // ������
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, X_min, X_max), Mathf.Clamp(transform.position.y, Y_min, Y_max), transform.position.z);
                break;
            case Mobilemoveway.joystick:
                joystick_obj.SetActive(true);
                break;
        }
#endif
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, X_min, X_max), Mathf.Clamp(transform.position.y, Y_min, Y_max), -6f);
    }

    /// <summary>
    /// �g��
    /// </summary>
    void Shoot()
    {
        GameObject temp = Instantiate(bullet_obj, bullet_trans.position, bullet_trans.rotation);
    }
    #endregion
}

/// <summary>
/// PC ����
/// </summary>
public enum PCmoveway
{
    mouse, keyboard, joystick
}

/// <summary>
/// ��� ����
/// </summary>
public enum Mobilemoveway
{
    acceleration, joystick
}
