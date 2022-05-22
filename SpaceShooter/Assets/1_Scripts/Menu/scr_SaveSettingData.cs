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
    void CheckFile(string _path)
    {
        if (File.Exists(_path))
        {
            // ��_�W���]�w
            ReadString(path);
        }
        else
        {
            fs = new FileStream(_path, FileMode.Create);
            fs.Close();
        }
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
    }

    /// <summary>
    /// Ū�� - �]�w�y�� / �j�p
    /// </summary>
    /// <param name="_path">�ɮ׸��|</param>
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
