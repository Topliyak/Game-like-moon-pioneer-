using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giver : MonoBehaviour
{
	[SerializeField] private Storage _storage;

	private void OnTriggerStay(Collider other)
	{
		var acceptor = other.GetComponent<Acceptor>();

		if (acceptor != null)
			Give(acceptor);
	}

	private void Give(Acceptor acceptor)
	{
		ResourceSet availableForGive = _storage.PullOut(_storage.ResourcesCount);
		ResourceSet notAccepted = acceptor.AcceptAndReturnExtra(availableForGive);
		_storage.PutIn(notAccepted);
	}
}
