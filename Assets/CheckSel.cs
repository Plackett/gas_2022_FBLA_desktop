using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckSel : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public int index;
    public Menu m;
    public void OnPointerEnter(PointerEventData data){
        m.selected = index;
    }
    public void OnSelect(BaseEventData eventData)
    {
        m.handleClick(m.selected);
    }
}
