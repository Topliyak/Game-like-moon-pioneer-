using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageView : MonoBehaviour
{
	private bool _inited = false;
	private GameObject[] _modelsInStorage;

	[SerializeField] private int _cellsRows;
	[SerializeField] private int _cellsColumns;
	[SerializeField] private int _cellsFloors;
	[SerializeField] private float _cellsSize;
	[SerializeField] private Vector3 _cellsOffset;
	[SerializeField] private Transform _firstCell;

	[Header("Debug")]
	public bool drawCells;

	public int CellsCountOnFloor => _cellsRows * _cellsColumns;

	public int CellsCount => _cellsRows * _cellsColumns * _cellsFloors;

	private void Awake() => Init();

	public void Init()
	{
		if (!_inited)
		{
			_modelsInStorage = new GameObject[_cellsColumns * _cellsRows * _cellsFloors];
			_inited = true;
		}
	}

	public void UpdateView(ResourceSet resourceSet)
	{
		for (int i = 0; i < CellsCount; i++)
		{
			int row = (i % CellsCountOnFloor) / _cellsColumns;
			int column = (i % CellsCountOnFloor) % _cellsColumns;
			int floor = i / CellsCountOnFloor;

			if (i < resourceSet.Count)
			{
				if (_modelsInStorage[i] == null)
				{
					_modelsInStorage[i] = Instantiate(resourceSet.Resource.Model, transform);
					_modelsInStorage[i].transform.rotation = _firstCell.rotation;
					_modelsInStorage[i].transform.localScale = Vector3.one * _cellsSize;
					_modelsInStorage[i].transform.position = _firstCell.position;
					_modelsInStorage[i].transform.position += _firstCell.right * _cellsOffset.x * column;
					_modelsInStorage[i].transform.position += _firstCell.forward * _cellsOffset.z * row;
					_modelsInStorage[i].transform.position += _firstCell.up * _cellsOffset.y * floor;
				}
			}
			else
			{
				if (_modelsInStorage[i] != null)
				{
					Destroy(_modelsInStorage[i]);
					_modelsInStorage[i] = null;
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (drawCells == false || _firstCell == null)
			return;

		for (int i = 0; i < CellsCount; i++)
		{
			int row = (i % CellsCountOnFloor) / _cellsColumns;
			int column = (i % CellsCountOnFloor) % _cellsColumns;
			int floor = i / CellsCountOnFloor;

			Vector3 pos = _firstCell.position;
			pos += _firstCell.right * _cellsOffset.x * column;
			pos += _firstCell.forward * _cellsOffset.z * row;
			pos += _firstCell.up * _cellsOffset.y * floor;

			Gizmos.DrawSphere(pos, _cellsSize / 2);
		}
	}
}
