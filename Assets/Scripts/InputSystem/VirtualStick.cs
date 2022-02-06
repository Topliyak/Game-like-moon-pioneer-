using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualStick : MonoBehaviour
{
	private float _stickAreaRadius;
	private float _stickAreaRadiusSqr;

	[SerializeField] private RectTransform _stickArea;
	[SerializeField] private RectTransform _stick;
	[SerializeField] private RectTransform _stickAreaMin, _stickAreaMax;

	public Vector2 Vector { get; private set; }

	private void Start() => Init();

	private void Init()
	{
		_stickAreaRadius = (_stickAreaMax.position.x - _stickAreaMin.position.x) / 2;
		_stickAreaRadiusSqr = _stickAreaRadius * _stickAreaRadius;
		_stickArea.gameObject.SetActive(false);
		Vector = Vector2.zero;
	}

	public void OnMouseUp(PointerEventData eventData)
	{
		_stick.position = _stickArea.position;
		_stickArea.gameObject.SetActive(false);
		Vector = Vector2.zero;
	}

	public void OnMouseDown(PointerEventData eventData)
	{
		_stickArea.gameObject.SetActive(true);
		_stickArea.position = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector2 vector = eventData.position - (Vector2)_stickArea.position;

		if (vector.sqrMagnitude > _stickAreaRadiusSqr)
		{
			vector.Normalize();
			vector *= _stickAreaRadius;
		}

		_stick.position = _stickArea.position + (Vector3)vector;
		Vector = vector / _stickAreaRadius;
	}
}
