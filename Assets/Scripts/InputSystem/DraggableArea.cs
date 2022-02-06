using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DraggableArea : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] private UnityEvent<PointerEventData> _dragEvent;
	[SerializeField] private UnityEvent<PointerEventData> _beginDragEvent;
	[SerializeField] private UnityEvent<PointerEventData> _endDragEvent;

	public UnityEvent<PointerEventData> DragEvent => _dragEvent;

	public UnityEvent<PointerEventData> BeginDragEvent => _beginDragEvent;

	public UnityEvent<PointerEventData> EndDragEvent => _endDragEvent;

	public void OnPointerDown(PointerEventData eventData) => _beginDragEvent.Invoke(eventData);

	public void OnPointerUp(PointerEventData eventData) => _endDragEvent.Invoke(eventData);

	public void OnDrag(PointerEventData eventData) => _dragEvent.Invoke(eventData);
}
