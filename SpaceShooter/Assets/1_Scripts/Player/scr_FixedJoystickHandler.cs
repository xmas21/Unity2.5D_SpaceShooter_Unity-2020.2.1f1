using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//拖曳物體IDragHandler 
//IEndDragHandler和 IBeginDragHandler 開始和結束拖曳物體
public class scr_FixedJoystickHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    //在inspector面板內顯示自定義class物件的內部資料
    [System.Serializable]
    public class VirtualJoystickEvent : UnityEvent<Vector3> { }

    public Transform content;
    public UnityEvent beginControl;
    public VirtualJoystickEvent controlling;
    public UnityEvent endControl;

    /// <summary>
    /// 按下搖桿
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.beginControl.Invoke();
    }

    /// <summary>
    /// 移動搖桿
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (this.content)
        {
            this.controlling.Invoke(this.content.localPosition.normalized);
        }
    }

    /// <summary>
    /// 放開搖桿
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        this.endControl.Invoke();
    }
}