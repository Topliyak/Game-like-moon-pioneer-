using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentResourcesGiver : MonoBehaviour
{
	[SerializeField] private DifferentResourcesStorage _storage;
	
	private void OnTriggerStay(Collider other)
	{
		var acceptor = other.GetComponent<Acceptor>();

		if (acceptor != null)
			Give(acceptor);
	}

	private void Give(Acceptor acceptor)
	{
		ResourceSet available = _storage.PutOut(acceptor.AcceptedResource, _storage.ResourcesCount);
		ResourceSet notAccepted = acceptor.AcceptAndReturnExtra(available);
		_storage.PutInAndReturnExtra(notAccepted);
	}
}
