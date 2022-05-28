using UnityEngine;

public class scr_PlayerController : MonoBehaviour
{
    #region - Fields -
    [Header("PC 遊玩操控方式")] static Moveway moveway;

    float X_min, X_max;      // 飛機 X軸上下限
    float Y_min, Y_max;      // 飛機 Y軸上下面
    float moveSpeed;         // 移動速度

    bool mouseTouchPlayer;   // 滑鼠 / 手指是否按到玩家

    Vector3 mousePos;        // 滑鼠座標
    #endregion

    #region - MonoBehaviour -
    void Start()
    {
        Initialize();
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

    #region - Methods -
    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        X_min = -1.2f;
        X_max = 1.2f;
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
        switch (moveway)
        {
            case Moveway.mouse:
                if (mouseTouchPlayer)
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = new Vector3(mousePos.x, mousePos.y, -6f);
                }
                break;
            case Moveway.keyboard:
                transform.Translate(Input.GetAxisRaw("Horizontal") * Time.deltaTime * _speed, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * _speed);
                break;
        }
#endif

#if UNITY_ANDROID
        transform.Translate(Input.acceleration.x * Time.deltaTime * _speed, 0, Input.acceleration.y * Time.deltaTime * _speed); // 陀螺儀
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, X_min, X_max), Mathf.Clamp(transform.position.y, Y_min, Y_max), transform.position.z);
#endif

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, X_min, X_max), Mathf.Clamp(transform.position.y, Y_min, Y_max), -6f);
    }
    #endregion
}

enum Moveway
{
    mouse, keyboard
}
