using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
    public static GameObject itemBeingDragged;
	public bool isDragable = false;
	Vector3 startPosition;
	Transform startParent;

	
	#region IBeginDragHandler implementation
	
	public void OnBeginDrag (PointerEventData eventData)
	{
		Debug.Log ("isdragable " + isDragable);
		if (!isDragable)
			return;
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		//GetComponent<CanvasGroup>().blocksRaycasts = false;
		startParent = transform.parent;
		transform.SetParent(  transform.parent.parent);
	}
	
	#endregion
	
	#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)
	{

		if (!isDragable)
			return;
		//transform.position = eventData.position;
		transform.position = Input.mousePosition;
		//Debug.Log ("Drag item current position " + eventData.position);
	}

	
	#endregion
	public void OnDrop(PointerEventData data)
	{
        Debug.Log("Dropped object was: " + isDragable);
		if (!isDragable)
			return;
		if (data.pointerDrag != null)
		{
			Debug.Log ("Dropped object was: "  + data.pointerDrag);
		}
	}
	
	#region IEndDragHandler implementation
	
	public void OnEndDrag (PointerEventData eventData)
	{

		if (!isDragable)
			return;
		if (eventData.pointerDrag != null)
		{
			//Debug.Log ("Dropped object was: "  + eventData.pointerDrag);
		}
	//	
		itemBeingDragged = null;
		//GetComponent<CanvasGroup>().blocksRaycasts = true;
		Debug.Log ("<color>I am here 1</color> "+transform.parent.gameObject.name);
        if(transform.parent != startParent && (transform.parent != GetComponent<CardScript>().tablePanel.transform))
        {
			Debug.Log ("<color>I am here </color> "+transform.parent.gameObject.name);

			transform.SetParent(startParent);
			transform.position  = startPosition;
		}
	}
	
	#endregion
	
	
	
}