using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_Video : MonoBehaviour
{
    [SerializeField] [Header("Vider Player")] VideoPlayer vp;
    [SerializeField] [Header("�v���t�� Slider")] Slider speed_Slider;

    void Start()
    {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// �U�@����
    /// </summary>
    public void NextScene()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// �վ�v���t��
    /// </summary>
    public void VideoSpeed()
    {
        vp.playbackSpeed = speed_Slider.value;
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <returns>�ɶ�</returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        if (!vp.isPlaying) NextScene();
    }
}
