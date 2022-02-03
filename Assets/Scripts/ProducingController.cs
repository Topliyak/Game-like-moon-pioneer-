using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducingController : MonoBehaviour
{
	[SerializeField] private Factory _factory;
	[SerializeField] private Storage _storage;

	private void Start()
	{
		if (_storage.FreeCells > 0)
			_factory.TurnOn();
	}

	public void AcceptResourceFromFactory(ResourceSet resourceSet)
	{
		_storage.PutIn(resourceSet);
	}

	public void AcceptResourceFromFactory(Resource resource)
	{
		_storage.PutIn(new ResourceSet(resource, 1));
	}

	public void PauseFactory()
	{
		if (_factory.Producing)
			_factory.TurnOff();
	}

	public void LaunchFactoryIfPaused()
	{
		if (!_factory.Producing)
			_factory.TurnOn();
	}
}
