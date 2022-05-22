using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class scr_ReadText : MonoBehaviour
{
    [Header("����C���r���")] public string[] chDatas;
    [Header("�^��C���r���")] public string[] enDatas;

    [SerializeField] [Header("���x")] Platform platform;

    string chPath;          // �����r�ɸ��|
    string enPath;          // �^���r�ɸ��|
    string chData;          // �����r��
    string enData;          // �^���r��

    WWW chReader;           // ����Ū�ɾ�
    WWW enReader;           // �^��Ū�ɾ�

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
    void MobileReadText()
    {
        // �z�L *���}* �Ӱ�Ū��
        chReader = new WWW(chPath);
        enReader = new WWW(enPath);

        // ����
        chDatas = chReader.text.Split('\n');
        enDatas = enReader.text.Split('\n');
    }

    enum Platform
    {
        PC, Mobile
    }
}

