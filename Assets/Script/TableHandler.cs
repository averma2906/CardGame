using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class TableHandler : MonoBehaviour,IDropHandler {

	
	public static TableHandler instance;
	#region IDropHandler implementation
	void Start()
	{
		instance = this;
	}

	public void OnDrop (PointerEventData eventData)
	{
        GameObject go  = DragHandler.itemBeingDragged;
        go.GetComponent<RectTransform>().SetParent(gameObject.transform, true);
        go.GetComponent<CardScript>().ShowCard();
         
	}
	#endregion
}
