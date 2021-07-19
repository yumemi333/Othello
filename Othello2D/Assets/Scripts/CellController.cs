using UnityEngine;
using UnityEngine.EventSystems;

public class CellController : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    public int X;
    [HideInInspector]
    public int Y;
    [HideInInspector]
    public int _cellContent;
    [HideInInspector]
    public Sprite _sprite;
    

    public void OnPointerDown(PointerEventData _data)
    {
        Debug.Log("clicked");
    }
}
