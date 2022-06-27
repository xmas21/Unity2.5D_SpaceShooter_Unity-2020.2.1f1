using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerController : MonoBehaviour
{
    #region - Fields -
    [Header("PC 遊玩操控方式")] public PCmoveway pcMoveway;
    [Header("Mobile 遊玩操控方式")] public Mobilemoveway mobilemoveway;

    [Header("血量")] public float hp;

    [SerializeField] [Header("搖桿物件")] GameObject joystick_obj;
    [SerializeField] [Header("子彈物件")] GameObject bullet_obj;
    [SerializeField] [Header("子彈生成點")] Transform bullet_trans;

    [SerializeField] [Header("子彈生成間隔")] float bulletInterval;

    [SerializeField] [Header("操控方式選單")] Dropdown moveway_Dropdown;

    float X_min, X_max;      // 飛機 X軸上下限
    float Y_min, Y_max;      // 飛機 Y軸上下面
    float moveSpeed;         // 移動速度

    bool mouseTouchPlayer;   // 滑鼠 / 手指是否按到玩家
    bool isJoysticked;       // 是否點擊到搖桿

    Vector3 mousePos;        // 滑鼠座標
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
    /// 按下搖桿
    /// </summary>
    public void IsUsingJoystick()
    {
        isJoysticked = true;
    }

    /// <summary>
    /// 放開搖桿
    /// </summary>
    public void UnUsingJoystick()
    {
        isJoysticked = false;
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// 移動搖桿
    /// </summary>
    public void UsingJoystick(Vector3 pos)
    {
        if (isJoysticked) transform.Translate(pos.x * moveSpeed * Time.deltaTime, 0f, pos.y * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 切換操控方式
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
    /// 初始化
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
    /// 移動
    /// </summary>
    /// <param name="_speed">移動速度</param>
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
                transform.Translate(Input.acceleration.x * Time.deltaTime * _speed, 0, Input.acceleration.y * Time.deltaTime * _speed); // 陀螺儀
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
    /// 射擊
    /// </summary>
    void Shoot()
    {
        GameObject temp = Instantiate(bullet_obj, bullet_trans.position, bullet_trans.rotation);
    }
    #endregion
}

/// <summary>
/// PC 移動
/// </summary>
public enum PCmoveway
{
    mouse, keyboard, joystick
}

/// <summary>
/// 手機 移動
/// </summary>
public enum Mobilemoveway
{
    acceleration, joystick
}
