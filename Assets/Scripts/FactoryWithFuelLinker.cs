using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryWithFuelLinker : MonoBehaviour
{
	[SerializeField] private FuelDependencyFactory _factory;
	[SerializeField] private Storage _fuelStorage;

	public bool NeedFuel { get; set; }

	private void Awake() => Init();

	public void Init()
	{
		NeedFuel = true;
	}

	public void OnFactoryDestroyFuel()
	{
		bool success = TryAddFuelToFactory();

		if (success)
		{
			NeedFuel = false;
		}
		else
		{
			NeedFuel = true;
		}
	}

	public void OnFuelInStorageAdded()
	{
		if (!NeedFuel)
			return;

		bool success = TryAddFuelToFactory();

		if (success)
		{
			NeedFuel = false;
		}
	}

	private bool TryAddFuelToFactory()
	{
		ResourceSet resourceSet = _fuelStorage.PullOut(1);

		if (resourceSet.Count == 0)
		{
			return false;
		}
		else
		{
			_factory.AddFuel(resourceSet.Resource);
			return true;
		}
	}
}
