using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiverZone : MonoBehaviour
{
	[SerializeField] private Storage _storage;

	private void OnTriggerStay(Collider other)
	{
		Acceptor acceptor = other.GetComponent<Acceptor>();

		if (acceptor != null)
			Give(acceptor);
	}

	private void Give(Acceptor acceptor)
	{
		if (acceptor.HasFreeCells == false)
			return;

		var available = _storage.PullOutAll(x => acceptor.CompatibleWith(x.Resource));
		var extra = acceptor.AcceptAndReturnExtra(available);
		_storage.PutInAndReturnExtra(extra);
	}
}
