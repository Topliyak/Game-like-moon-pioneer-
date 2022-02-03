using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceptor : MonoBehaviour
{
	[SerializeField] private Storage _acceptingStorage;

	public Resource AcceptedResource => _acceptingStorage.Resource;

	virtual public ResourceSet AcceptAndReturnExtra(ResourceSet resourceSet)
	{
		if (resourceSet.Resource != _acceptingStorage.Resource)
			return resourceSet;

		ResourceSet extra = _acceptingStorage.PutInAndReturnExtra(resourceSet);

		return extra;
	}
}
