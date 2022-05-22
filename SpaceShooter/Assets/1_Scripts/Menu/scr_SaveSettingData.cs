using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class scr_SaveSettingData : MonoBehaviour
{
    [SerializeField] [Header("語言選單")] Dropdown language_Dropdown;
    [SerializeField] [Header("尺寸選單")] Dropdown size_Dropdown;
    [SerializeField] [Header("平台")] Platform platform;
    [SerializeField] [Header("資料")] string[] datas;

    string path;
    string readerPC;

    WWW reader;
    FileStream fs;

    void Awake()
    {
        path = Application.persistentDataPath + "Save.txt";
    }

    void Start()
    {
        CheckFile(path);
    }

    /// <summary>
    /// 儲存檔案
    /// </summary>
    public void SaveData()
    {
        WriteString(path, size_Dropdown.value + "@" + language_Dropdown.value);
    }

    /// <summary>
    /// 創建檔案
    /// </summary>
    /// <param name="_path">檔案路徑</param>
    void CheckFile(string _path)
    {
        if (File.Exists(_path))
        {
            // 恢復上次設定
            ReadString(path);
        }
        else
        {
            fs = new FileStream(_path, FileMode.Create);
            fs.Close();
        }
    }

    /// <summary>
    /// 寫檔案
    /// </summary>
    /// <param name="_path">資料路徑</param>
    /// <param name="_data">資料內容</param>
    void WriteString(string _path, string _data)
    {
        fs = new FileStream(_path, FileMode.Open);

        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine(_data);

        sw.Close();
    }

    /// <summary>
    /// 讀檔 - 設定語言 / 大小
    /// </summary>
    /// <param name="_path">檔案路徑</param>
    void ReadString(string _path)
    {
        switch (platform)
        {
            case Platform.PC:
                readerPC = File.ReadAllText(_path);
                datas = readerPC.Split('@');
                size_Dropdown.value = int.Parse(datas[0]);
                language_Dropdown.value = int.Parse(datas[1]);
                break;
            case Platform.Mobile:
                reader = new WWW(_path);
                datas = reader.text.Split('@');
                size_Dropdown.value = int.Parse(datas[0]);
                language_Dropdown.value = int.Parse(datas[1]);
                break;
        }
    }
}

enum Platform
{
    PC, Mobile
}
