using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class scr_ReadText : MonoBehaviour
{
    [Header("����C���r���")] public string[] chDatas;
    [Header("�^��C���r���")] public string[] enDatas;

    [SerializeField] [Header("���x")] Platform platform;

    string chPath;                            // �����r�ɸ��|
    string enPath;                            // �^���r�ɸ��|
    string chData;                            // �����r��
    string enData;                            // �^���r��

    [System.Obsolete] WWW chReader;           // ����Ū�ɾ�
    [System.Obsolete] WWW enReader;           // �^��Ū�ɾ�

    [System.Obsolete]
    void Awake()
    {
        // ����|
        chPath = Application.streamingAssetsPath + "/CH.txt";
        enPath = Application.streamingAssetsPath + "/EN.txt";

        switch (platform)
        {
            case Platform.PC:
                PCReadText();
                break;
            case Platform.Mobile:
                MobileReadText();
                break;
        }

    }

    /// <summary>
    /// PC Ū��
    /// </summary>
    void PCReadText()
    {
        // �z�L *����* �ɮ�Ū��
        chData = File.ReadAllText(chPath);
        enData = File.ReadAllText(enPath);

        // ����
        chDatas = chData.Split('\n');
        enDatas = enData.Split('\n');
    }

    /// <summary>
    /// ��� Ū��
    /// </summary>
    [System.Obsolete]
    void MobileReadText()
    {
        // �z�L *���}* �Ӱ�Ū��
        chReader = new WWW(chPath);
        enReader = new WWW(enPath);

        // ����
        chDatas = chReader.text.Split('\n');
        enDatas = enReader.text.Split('\n');
    }

    /// <summary>
    /// ���x
    /// </summary>
    enum Platform
    {
        PC, Mobile
    }
}

