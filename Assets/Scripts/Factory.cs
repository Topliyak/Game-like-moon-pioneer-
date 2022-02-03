using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Factory : MonoBehaviour
{
	private float _lastUnitProducingTime;

	[SerializeField] private Resource _producedResource;
	[SerializeField] private float _secondsToProductOneUnit;

	[Header("Events")]
	[SerializeField] private UnityEvent<Resource> _resourceProducedEvent;
	[SerializeField] private UnityEvent _pausedEvent;
	[SerializeField] private UnityEvent _launchedEvent;

	public UnityEvent<Resource> ResourceProducedEvent => _resourceProducedEvent;

	public UnityEvent PausedEvent => _pausedEvent;

	public UnityEvent LaunchedEvent => _launchedEvent;

	public float ProducingProgress
	{
		get => Mathf.Clamp01(_lastUnitProducingTime / _secondsToProductOneUnit);
	}

	public bool Producing { get; private set; }

	virtual public bool CanProduct => Producing;

	public void TurnOn()
	{
		Producing = true;
		_launchedEvent.Invoke();
	}

	public void TurnOff()
	{
		Producing = false;
		_pausedEvent.Invoke();
	}

	private void Update()
	{
		if (CanProduct)
		{
			_lastUnitProducingTime += Time.deltaTime;
		}

		if (ProducingProgress >= 1)
		{
			_resourceProducedEvent.Invoke(_producedResource);
			_lastUnitProducingTime = 0;
		}
	}
}
