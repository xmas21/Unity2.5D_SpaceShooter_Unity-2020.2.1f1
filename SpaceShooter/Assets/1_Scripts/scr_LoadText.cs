using UnityEngine;
using UnityEngine.UI;

public class scr_LoadText : MonoBehaviour
{
    [SerializeField] [Header("¤º®e")] Text content;
    [SerializeField] [Header("¤å¦rID")] int id;

    void Awake()
    {
        content = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        switch (scr_StaticVar.language_Dropdown)
        {
            case 0:
                content.text = GameObject.Find("TextData").GetComponent<scr_ReadText>().chDatas[id];
                break;
            case 1:
                content.text = GameObject.Find("TextData").GetComponent<scr_ReadText>().enDatas[id];
                break;
        }
    }
}
