using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_Menumanager : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("�]�w�e��")] GameObject settingPage;
    [SerializeField] [Header("�R���Ϥ�")] Sprite[] mute_sprite;
    [SerializeField] [Header("�R�����s - �Ϥ�")] Image mute_img;

    bool isSetting;              // �O�_�}�ҳ]�w�e��
    bool isMute;                 // �O�_�����R��

    Button start_btn;            // �}�l���s
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
    /// �]�w�e��
    /// </summary>
    public void Setting()
    {
        isSetting = !isSetting;

        settingPage.SetActive(isSetting);
    }

    /// <summary>
    /// �����R��
    /// </summary>
    public void Sound()
    {
        isMute = !isMute;

        if (isMute) mute_img.sprite = mute_sprite[1];
        else if (!isMute) mute_img.sprite = mute_sprite[0];

        // pause = true > �R��  |  pause = false > ��_�n��
        AudioListener.pause = isMute;
    }

    /// <summary>
    /// �ƭȪ�l��
    /// </summary>
    void Initialize()
    {
        isSetting = false;
        isMute = false;
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
