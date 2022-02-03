using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuelDependencyFactory : Factory
{
	[SerializeField] private MyDictionary<Resource, int> _fuelAndCountPairs;

	[Header("Events")]
	[SerializeField] private UnityEvent _fuelDestroyedEvent;

	public UnityEvent FuelDestroyedEvent => _fuelDestroyedEvent;

	public override bool CanProduct => base.CanProduct && HasUnitOfEveryFuel;

	private bool HasUnitOfEveryFuel
	{
		get
		{
			foreach (var pair in _fuelAndCountPairs.pairs)
				if (pair.value == 0)
					return false;

			return true;
		}
	}

	public void AddFuel(Resource fuel)
	{
		_fuelAndCountPairs[fuel]++;

	}

	public void DestroyEveryFuelUnit()
	{
		foreach (var pair in _fuelAndCountPairs.pairs)
			pair.value--;

		_fuelDestroyedEvent.Invoke();
	}
}
