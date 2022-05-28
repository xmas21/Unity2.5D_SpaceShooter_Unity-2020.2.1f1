using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class scr_ReadText : MonoBehaviour
{
    [Header("中文每行文字資料")] public string[] chDatas;
    [Header("英文每行文字資料")] public string[] enDatas;

    [SerializeField] [Header("平台")] Platform platform;

    string chPath;                            // 中文文字檔路徑
    string enPath;                            // 英文文字檔路徑
    string chData;                            // 中文文字檔
    string enData;                            // 英文文字檔

    [System.Obsolete] WWW chReader;           // 中文讀檔器
    [System.Obsolete] WWW enReader;           // 英文讀檔器

    [System.Obsolete]
    void Awake()
    {
        // 抓路徑
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
    /// PC 讀檔
    /// </summary>
    void PCReadText()
    {
        // 透過 *本機* 檔案讀檔
        chData = File.ReadAllText(chPath);
        enData = File.ReadAllText(enPath);

        // 切割
        chDatas = chData.Split('\n');
        enDatas = enData.Split('\n');
    }

    /// <summary>
    /// 手機 讀檔
    /// </summary>
    [System.Obsolete]
    void MobileReadText()
    {
        // 透過 *網址* 來做讀檔
        chReader = new WWW(chPath);
        enReader = new WWW(enPath);

        // 切割
        chDatas = chReader.text.Split('\n');
        enDatas = enReader.text.Split('\n');
    }

    /// <summary>
    /// 平台
    /// </summary>
    enum Platform
    {
        PC, Mobile
    }
}

