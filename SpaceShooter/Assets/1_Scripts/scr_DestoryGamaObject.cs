using UnityEngine;

public class scr_DestoryGamaObject : MonoBehaviour
{
    float deleteTime;

    void Start()
    {
        deleteTime = 3;
        Destroy(gameObject, deleteTime);
    }
}
