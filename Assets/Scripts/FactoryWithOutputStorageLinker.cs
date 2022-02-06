using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FactoryWithOutputStorageLinker : MonoBehaviour
{
	private bool _storageFull = false;

	[SerializeField] private Factory _factory;
	[SerializeField] private Storage _storage;

	private void OnEnable()
	{
		_storage.StorageIsFullEvent.AddListener(OnStorageBecomeFull);
		_storage.PullOutEvent.AddListener(OnStorageHadFreeCells);
		_factory.ProducedEvent.AddListener(OnFactoryProduce);
	}

	private void OnDisable()
	{
		_storage.StorageIsFullEvent.RemoveListener(OnStorageBecomeFull);
		_storage.PullOutEvent.RemoveListener(OnStorageHadFreeCells);
		_factory.ProducedEvent.RemoveListener(OnFactoryProduce);
	}

	private void OnStorageBecomeFull(object sender)
	{
		_factory.TurnOff();
		_storageFull = true;
	}

	private void OnStorageHadFreeCells(object sender, ResourceUnit resourceUnit)
	{
		if (_storageFull)
		{
			_storageFull = false;
			_factory.TurnOn();
		}
	}

	private void OnFactoryProduce(object sender)
	{
		ResourceUnit resourceUnit = null;
		_factory.TryPullOutProducedResourceUnit(ref resourceUnit);
		_storage.PutInAndReturnExtra(resourceUnit);
	}
}
