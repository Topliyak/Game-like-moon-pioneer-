using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DifferentResourcesStorage : MonoBehaviour
{
	[SerializeField] private int _cellsCount;
	[SerializeField] private List<Resource> _resources;

	[Header("Events")]
	[SerializeField] private UnityEvent<List<Resource>> _resourceSetChangedEvent;

	public UnityEvent<List<Resource>> ResourceSetChangedEvent => _resourceSetChangedEvent;

	public int CellsCount => _cellsCount;

	public int ResourcesCount => _resources.Count;

	public int FreeCells => CellsCount - ResourcesCount;

	public bool TryPutIn(Resource resource)
	{
		if (FreeCells == 0)
			return false;

		_resources.Add(resource);
		_resourceSetChangedEvent.Invoke(_resources);

		return true;
	}

	public ResourceSet PutInAndReturnExtra(ResourceSet resourceSet)
	{
		int notAcceptedCount = resourceSet.Count;

		while (_resources.Count < _cellsCount && notAcceptedCount > 0)
		{
			_resources.Add(resourceSet.Resource);
			notAcceptedCount--;
		}

		if (notAcceptedCount < resourceSet.Count)
		{
			_resourceSetChangedEvent.Invoke(_resources);
		}

		return new ResourceSet(resourceSet.Resource, notAcceptedCount);
	}

	public ResourceSet PutOut(Resource resource, int desiredCount)
	{
		int availableCount = 0;

		for (int i = _resources.Count - 1; i >= 0; i--)
		{
			if (_resources[i] == resource)
			{
				availableCount++;
				_resources.RemoveAt(i);
			}
		}

		if (availableCount > 0)
		{
			_resourceSetChangedEvent.Invoke(_resources);
		}

		return new ResourceSet(resource, availableCount);
	}
}
