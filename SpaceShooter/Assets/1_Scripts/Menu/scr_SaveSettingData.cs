using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class scr_SaveSettingData : MonoBehaviour
{
    [SerializeField] [Header("�y�����")] Dropdown language_Dropdown;
    [SerializeField] [Header("�ؤo���")] Dropdown size_Dropdown;
    [SerializeField] [Header("���x")] Platform platform;
    [SerializeField] [Header("���")] string[] datas;

    string path;
    string readerPC;

    FileStream fs;
    [System.Obsolete] WWW reader;

    void Awake()
    {
        path = Application.persistentDataPath + "Save.txt";
    }

    [System.Obsolete]
    void Start()
    {
        CheckFile(path);
    }

    /// <summary>
    /// �x�s�ɮ�
    /// </summary>
    public void SaveData()
    {
        WriteString(path, size_Dropdown.value + "@" + language_Dropdown.value);
    }

    /// <summary>
    /// �Ы��ɮ�
    /// </summary>
    /// <param name="_path">�ɮ׸��|</param>
    [System.Obsolete]
    void CheckFile(string _path)
    {
        if (File.Exists(_path)) ReadString(_path);
        else fs = new FileStream(_path, FileMode.Create);
    }

    /// <summary>
    /// Ū�� - �]�w�y�� / �j�p
    /// </summary>
    /// <param name="_path">�ɮ׸��|</param>
    [System.Obsolete]
    void ReadString(string _path)
    {
        switch (platform)
        {
            case Platform.PC:
                readerPC = File.ReadAllText(_path);
                datas = readerPC.Split('@');
                break;
            case Platform.Mobile:
                reader = new WWW(_path);
                datas = reader.text.Split('@');
                break;
        }
        size_Dropdown.value = int.Parse(datas[0]);
        language_Dropdown.value = int.Parse(datas[1]);
    }

    /// <summary>
    /// �g�ɮ�
    /// </summary>
    /// <param name="_path">��Ƹ��|</param>
    /// <param name="_data">��Ƥ��e</param>
    void WriteString(string _path, string _data)
    {
        fs = new FileStream(_path, FileMode.Open);

        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine(_data);

        sw.Close();
        fs.Close();
    }
}

/// <summary>
/// ���x
/// </summary>
enum Platform
{
    PC, Mobile
}
