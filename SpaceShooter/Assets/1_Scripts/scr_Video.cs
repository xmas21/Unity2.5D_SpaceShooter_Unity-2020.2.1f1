using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_Video : MonoBehaviour
{
    [SerializeField] [Header("Vider Player")] VideoPlayer vp;
    [SerializeField] [Header("影片速度 Slider")] Slider speed_Slider;

    void Start()
    {
        StartCoroutine(Wait());
    }

    /// <summary>
    /// 下一場景
    /// </summary>
    public void NextScene()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// 調整影片速度
    /// </summary>
    public void VideoSpeed()
    {
        vp.playbackSpeed = speed_Slider.value;
    }

    /// <summary>
    /// 延後執行
    /// </summary>
    /// <returns>時間</returns>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        if (!vp.isPlaying) NextScene();
    }
}
