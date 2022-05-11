using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class scr_Menumanager : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("設定畫面")] GameObject settingPage;
    [SerializeField] [Header("開聲音 - 圖片")] Sprite soundOn_sprite;
    [SerializeField] [Header("關聲音 - 圖片")] Sprite soundOff_sprite;
    [SerializeField] [Header("靜音按鈕 - 物件")] Image mute_img;
    [SerializeField] [Header("聲音拉桿")] Slider sound_Slider;
    [SerializeField] [Header("Audio Mixer")] AudioMixer am;
    [SerializeField] [Header("BGM Toggle")] Toggle bgm_Toggle;
    [SerializeField] [Header("SFX Toggle")] Toggle sfx_Toggle;

    bool isSetting;              // 是否開啟設定畫面
    bool isMute;                 // 是否全部靜音

    Button start_btn;            // 開始按鈕

    [SerializeField] [Header("Stream Assets 路徑")] string path;
    [SerializeField] [Header("Stream Assets 路徑內檔案")] string[] fileInPath;
    #endregion

    #region - MonoBehaviours - 
    void Awake()
    {
        start_btn = GameObject.Find("Start - btn").GetComponent<Button>();

        Initialize();
        LoadStreamFile();

        // 使用 Resources
        soundOn_sprite = Resources.Load<Sprite>("Sound on");
        soundOff_sprite = Resources.Load<Sprite>("Sound off");

        // 使用 StreamingAssets
    }

    void Start()
    {
        ButtonEvent();
    }

    void Update()
    {
        AudioListener.volume = sound_Slider.value;
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
    /// 靜音功能
    /// </summary>
    public void Mute()
    {
        isMute = !isMute;

        if (isMute) mute_img.sprite = soundOff_sprite;
        else if (!isMute) mute_img.sprite = soundOn_sprite;

        // pause = true > 靜音  |  pause = false > 恢復聲音
        AudioListener.pause = isMute;
    }

    /// <summary>
    /// 聲音拉桿
    /// </summary>
    public void SliderChange()
    {
        if (sound_Slider.value == 0)
        {
            isMute = false;
            Mute();
        }
        else
        {
            isMute = true;
            Mute();
        }
    }

    /// <summary>
    /// 控制切換
    /// </summary>
    public void ControlToggle(string audiomixer)
    {
        if (bgm_Toggle.isOn) am.SetFloat(audiomixer, 0f);

        else am.SetFloat(audiomixer, -80f);
    }

    /// <summary>
    /// 按鈕點擊
    /// </summary>
    void ButtonEvent()
    {
        start_btn.onClick.AddListener(() => { ChangeScene("Video"); }); // 開始按鈕
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        isSetting = false;
        isMute = false;

        sound_Slider.value = 0.5f;
    }

    /// <summary>
    /// 抓取 StreamingAssets 內的檔案
    /// </summary>
    void LoadStreamFile()
    {
        // 讀取 StreamingAssets 路徑
        path = Application.streamingAssetsPath;
        // 抓取 StreamingAssets 資料夾內所有的 png 檔
        fileInPath = Directory.GetFiles(path, "*.png");
        // 讀取 StreamingAssets 資料夾內所有的 png 圖檔並轉換成 Byte，使程式看的懂得格式
        byte[] pngBytes = System.IO.File.ReadAllBytes(fileInPath[0]);
        // 定義圖片大小
        Texture2D tex = new Texture2D(2, 2);
        // 讀取圖片
        tex.LoadImage(pngBytes);
        // 將圖片轉換成 sprite
        soundOn_sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0f, 0f), 100f);
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
