using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentResourcesAcceptor : Acceptor
{
	[SerializeField] private DifferentResourcesStorage _storage;

	public override ResourceSet AcceptAndReturnExtra(ResourceSet resourceSet)
	{
		return _storage.PutInAndReturnExtra(resourceSet);
	}
}
