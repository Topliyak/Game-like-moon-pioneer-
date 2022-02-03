using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryWithFuelLinker : MonoBehaviour
{
	private bool _needFuel;

	[SerializeField] private FuelDependencyFactory _factory;
	[SerializeField] private Storage _fuelStorage;

	private void Awake() => Init();

	public void Init()
	{
		_needFuel = true;
	}

	public void TickFactoryAsNeededInFuel() => _needFuel = true;

	public void OnFuelInStorageAdded()
	{
		if (!_needFuel)
			return;

		var availableFuelSet = _fuelStorage.PullOut(1);

		if (availableFuelSet.Count == 1)
		{
			_factory.AddFuel(availableFuelSet.Resource);
			_needFuel = false;
		}
	}
}
