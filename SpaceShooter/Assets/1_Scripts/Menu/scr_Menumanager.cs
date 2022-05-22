using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class scr_Menumanager : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("�]�w�e��")] GameObject settingPage;
    [SerializeField] [Header("�}�n�� - �Ϥ�")] Sprite soundOn_sprite;
    [SerializeField] [Header("���n�� - �Ϥ�")] Sprite soundOff_sprite;
    [SerializeField] [Header("�R�����s - ����")] Image mute_img;
    [SerializeField] [Header("�n���Ա�")] Slider sound_Slider;
    [SerializeField] [Header("Audio Mixer")] AudioMixer am;
    [SerializeField] [Header("BGM Toggle")] Toggle bgm_Toggle;
    [SerializeField] [Header("SFX Toggle")] Toggle sfx_Toggle;
    [SerializeField] [Header("�ѪR�� Dropdown")] Dropdown screenDropdown;
    [SerializeField] [Header("�ѪR�� Dropdown")] Dropdown languageDropdown;

    bool isSetting;              // �O�_�}�ҳ]�w�e��
    bool isMute;                 // �O�_�����R��
    string path;                 // Stream Assets ���|
    string[] fileInPath;         // Stream Assets ���|���ɮ�

    Button start_btn;            // �}�l���s
    AudioSource aud;
    #endregion

    #region - MonoBehaviours - 
    void Awake()
    {
        start_btn = GameObject.Find("Start - btn").GetComponent<Button>();
        aud = GetComponent<AudioSource>();

        Initialize();
        // LoadStreamFile();

        // �ϥ� Resources
        soundOn_sprite = Resources.Load<Sprite>("Sound on");
        soundOff_sprite = Resources.Load<Sprite>("Sound off");

        // �ϥ� StreamingAssets
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
    /// �]�w�e��
    /// </summary>
    public void Setting()
    {
        isSetting = !isSetting;

        settingPage.SetActive(isSetting);
    }

    /// <summary>
    /// �R���\��
    /// </summary>
    public void Mute()
    {
        isMute = !isMute;

        if (isMute) mute_img.sprite = soundOff_sprite;
        else if (!isMute) mute_img.sprite = soundOn_sprite;

        // pause = true > �R��  |  pause = false > ��_�n��
        AudioListener.pause = isMute;
    }

    /// <summary>
    /// �n���Ա�
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
    /// ���� ���������R��
    /// </summary>
    public void ControlAudioToggle(string audiomixer)
    {
        if (bgm_Toggle.isOn) am.SetFloat(audiomixer, 0f);

        else am.SetFloat(audiomixer, -80f);
    }

    /// <summary>
    /// ���� �����ù��j�p
    /// </summary>
    public void ControlScreenSize()
    {
        switch (screenDropdown.value)
        {
            case 0:
                Screen.SetResolution(480, 800, false);
                break;
            case 1:
                Screen.SetResolution(720, 1280, false);
                break;
            case 2:
                Screen.SetResolution(1080, 1920, false);
                break;
        }
    }

    /// <summary>
    /// ���� �����y��
    /// </summary>
    public void ControlLanguage()
    {
        scr_StaticVar.language_Dropdown = languageDropdown.value;
    }

    /// <summary>
    /// ���s�I��
    /// </summary>
    void ButtonEvent()
    {
        start_btn.onClick.AddListener(() => { ChangeScene("Video"); }); // �}�l���s
    }

    /// <summary>
    /// ��l��
    /// </summary>
    void Initialize()
    {
        isSetting = false;
        isMute = false;

        sound_Slider.value = 0.5f;
    }

    /// <summary>
    /// ��� StreamingAssets �����ɮ�
    /// </summary>
    void LoadStreamFile()
    {
        // Ū�� StreamingAssets ���|
        path = Application.streamingAssetsPath;
        // ��� StreamingAssets ��Ƨ����Ҧ��� png ��
        fileInPath = Directory.GetFiles(path, "*.png");
        // Ū�� StreamingAssets ��Ƨ����Ҧ��� png ���ɨ��ഫ�� Byte�A�ϵ{���ݪ����o�榡
        byte[] pngBytes = System.IO.File.ReadAllBytes(fileInPath[0]);
        // �w�q�Ϥ��j�p
        Texture2D tex = new Texture2D(2, 2);
        // Ū���Ϥ�
        tex.LoadImage(pngBytes);
        // �N�Ϥ��ഫ�� sprite
        soundOn_sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0f, 0f), 100f);
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="sceneName">�����W��</param>
    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void BGMMute()
    {
        if (Application.loadedLevelName == "Video") aud.mute = true;

        else aud.mute = false;
    }
    #endregion
}
