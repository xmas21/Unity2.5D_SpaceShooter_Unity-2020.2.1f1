using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class scr_Menumanager : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("�]�w�e��")] GameObject settingPage;
    [SerializeField] [Header("�n���Ϥ�")] Sprite[] sound_sprite;
    [SerializeField] [Header("�R�����s - �Ϥ�")] Image mute_img;
    [SerializeField] [Header("�n���Ա�")] Slider sound_Slider;

    Sprite soundOpenSprite;

    bool isSetting;              // �O�_�}�ҳ]�w�e��
    bool isMute;                 // �O�_�����R��

    Button start_btn;            // �}�l���s

    [SerializeField] [Header("Stream Assets ���|")] string Path;
    [SerializeField] [Header("Stream Assets ���|���ɮ�")] string[] FilePath;
    #endregion

    #region - MonoBehaviours - 
    void Awake()
    {
        start_btn = GameObject.Find("Start - btn").GetComponent<Button>();

        Initialize();
        LoadStreamFile();

        // �ϥ� Resources
        sound_sprite[0] = Resources.Load<Sprite>("Sound on");
        sound_sprite[1] = Resources.Load<Sprite>("Sound off");

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

        if (isMute) mute_img.sprite = sound_sprite[1];
        else if (!isMute) mute_img.sprite = sound_sprite[0];

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

        sound_sprite = new Sprite[2];
    }

    /// <summary>
    /// ��� StreamingAssets �����ɮ�
    /// </summary>
    void LoadStreamFile()
    {
        // Ū�� StreamingAssets ���|
        Path = Application.streamingAssetsPath;
        // ��� StreamingAssets ��Ƨ����Ҧ��� png ��
        FilePath = Directory.GetFiles(Path, "*.png");
        // Ū�� StreamingAssets ��Ƨ����Ҧ��� png ���ɨ��ഫ�� Byte�A�ϵ{���ݪ����o�榡
        byte[] pngBytes = System.IO.File.ReadAllBytes(FilePath[0]);
        // �w�q�Ϥ��j�p
        Texture2D tex = new Texture2D(2, 2);
        // Ū���Ϥ�
        tex.LoadImage(pngBytes);
        // �N�Ϥ��ഫ�� sprite
        soundOpenSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0f, 0f), 100f);
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="sceneName">�����W��</param>
    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion
}
