using UnityEngine;

public class scr_DontDestory : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
