using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Storage : MonoBehaviour
{
	private int _resourcesCount;

	private UnityException _incompatibleResourcesTypesError;

	[SerializeField] private Resource _resource;
	[SerializeField] private int _cellsCount;

	[Header("Events")]
	[SerializeField] private UnityEvent _storageIsFullEvent;
	[SerializeField] private UnityEvent _storageHadFreeCellsEvent;
	[SerializeField] private UnityEvent _inStorageAddedResourcesEvent;
	[SerializeField] private UnityEvent<ResourceSet> _resourcesCountChangedEvent;

	public UnityEvent StorageIsFullEvent => _storageIsFullEvent;

	public UnityEvent StorageHadFreeCellsEvent => _storageHadFreeCellsEvent;

	public UnityEvent InStorageAddedResourcesEvent => _inStorageAddedResourcesEvent;

	public UnityEvent<ResourceSet> ResourcesCountChangedEvent => _resourcesCountChangedEvent;

	public Resource Resource => _resource;

	public int FreeCells => _cellsCount - _resourcesCount;

	public int ResourcesCount
	{
		get 
		{
			return _resourcesCount;
		}
		private set
		{
			value = Mathf.Clamp(value, 0, _cellsCount);

			if (value == _cellsCount)
			{
				_storageIsFullEvent.Invoke();
			}

			int countBeforeAssignment = _resourcesCount;
			_resourcesCount = value;

			if (_resourcesCount > countBeforeAssignment)
			{
				_inStorageAddedResourcesEvent.Invoke();
				_resourcesCountChangedEvent.Invoke(new ResourceSet(_resource, _resourcesCount));
			}
			else if (_resourcesCount < countBeforeAssignment)
			{
				_storageHadFreeCellsEvent.Invoke();
				_resourcesCountChangedEvent.Invoke(new ResourceSet(_resource, _resourcesCount));
			}
		}
	}

	private void Awake() => Init();

	public void Init()
	{
		_incompatibleResourcesTypesError = new UnityException("Incompatible resources types");
	}

	public ResourceSet PullOut(int desiredCount)
	{
		int dispensedCount = Mathf.Min(desiredCount, ResourcesCount);
		ResourcesCount -= dispensedCount;

		return new ResourceSet(_resource, dispensedCount);
	}

	public ResourceSet PutInAndReturnExtra(ResourceSet resourceSet)
	{
		if (resourceSet.Resource != _resource)
			throw _incompatibleResourcesTypesError;

		int acceptedCount = Mathf.Min(resourceSet.Count, FreeCells);
		ResourcesCount += acceptedCount;

		return new ResourceSet(_resource, resourceSet.Count - acceptedCount);
	}

	public void PutIn(ResourceSet resourceSet)
	{
		if (resourceSet.Resource != _resource)
			throw _incompatibleResourcesTypesError;

		ResourcesCount += resourceSet.Count;
	}
}
