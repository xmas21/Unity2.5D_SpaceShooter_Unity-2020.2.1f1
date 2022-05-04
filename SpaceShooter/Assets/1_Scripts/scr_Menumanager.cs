using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_Menumanager : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("設定畫面")] GameObject settingPage;
    [SerializeField] [Header("靜音圖片")] Sprite[] mute_sprite;
    [SerializeField] [Header("靜音按鈕 - 圖片")] Image mute_img;

    bool isSetting;              // 是否開啟設定畫面
    bool isMute;                 // 是否全部靜音

    Button start_btn;            // 開始按鈕
    #endregion

    #region - MonoBehaviour - 
    void Awake()
    {
        start_btn = GameObject.Find("Start - btn").GetComponent<Button>();
    }

    void Start()
    {
        Initialize();

        // button event
        start_btn.onClick.AddListener(() => { ChangeScene("Video"); });
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// 設定畫面
    /// </summary>
    public void Setting()
    {
        isSetting = !isSetting;

        settingPage.SetActive(isSetting);
    }

    /// <summary>
    /// 控制靜音
    /// </summary>
    public void Sound()
    {
        isMute = !isMute;

        if (isMute) mute_img.sprite = mute_sprite[1];
        else if (!isMute) mute_img.sprite = mute_sprite[0];

        // pause = true > 靜音  |  pause = false > 恢復聲音
        AudioListener.pause = isMute;
    }

    /// <summary>
    /// 數值初始化
    /// </summary>
    void Initialize()
    {
        isSetting = false;
        isMute = false;
    }

    /// <summary>
    /// 切換場景
    /// </summary>
    /// <param name="sceneName">場景名稱</param>
    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion
}
