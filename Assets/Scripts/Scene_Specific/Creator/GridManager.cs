using System.Collections.Generic;
using UnityEngine;

public enum GM_Direction
{
	RIGHT,
	LEFT,
	UP,
	DOWN
}

public class GridManager : MonoBehaviour
{
	[Header("Settings")]
	public int _GridObjectSize = 5;

	[SerializeField] private Vector2Int _GridSize;
	[SerializeField] private Vector2Int _CurrentPosition;

	[Header("Debugging")]
	[SerializeField] private bool _ResetPositionInDebugger = false;

	public GridObject _CurrentObject = null;
	public List<GridObject> _GridObjects = new List<GridObject>();

	private void Awake()
	{
		for (int x = 0; x < _GridSize.x; x++)
		{
			for (int y = 0; y < _GridSize.y; y++)
			{
				GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				newObj.transform.position = new Vector3(_GridObjectSize * x, 0, _GridObjectSize * y);
				newObj.transform.SetParent(transform);
				newObj.name = $"grid_obj({x},{y})";

				GridObject obj = newObj.AddComponent<GridObject>();
				obj._Position = new Vector2Int(x, y);
				_GridObjects.Add(obj);
				if (obj._Position == _CurrentPosition)
				{
					_CurrentObject = obj;
					_CurrentObject.SetState(GO_State.Selected);
				}
			}
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_CurrentObject.SetupMesh(_GridObjectSize, _GridSize);
		}
		else if (Input.GetMouseButtonDown(1))
		{
			_CurrentObject.DestroyMesh();
		}
	}

	private void OnDrawGizmos()
	{
		if (_ResetPositionInDebugger)
		{
			_CurrentPosition = new Vector2Int(Mathf.FloorToInt(_GridSize.x / 2), Mathf.FloorToInt(_GridSize.y / 2));

			_ResetPositionInDebugger = false;
		}

		for (int x = 0; x < _GridSize.x; x++)
		{
			for (int y = 0; y < _GridSize.y; y++)
			{
				// Draw current position
				if (x == _CurrentPosition.x && y == _CurrentPosition.y)
				{
					Gizmos.color = Color.red;
					Gizmos.DrawSphere(new Vector3(_GridObjectSize * x, 0, _GridObjectSize * y), 1);
					Gizmos.color = Color.white;
					continue;
				}

				Gizmos.DrawSphere(new Vector3(_GridObjectSize * x, 0, _GridObjectSize * y), 1);
			}
		}
	}

	public void SetCurrentPosition(GM_Direction dir)
	{
		switch (dir)
		{
			case GM_Direction.RIGHT:
				_CurrentPosition.x++;
				break;
			case GM_Direction.LEFT:
				_CurrentPosition.x--;
				break;
			case GM_Direction.UP:
				_CurrentPosition.y++;
				break;
			case GM_Direction.DOWN:
				_CurrentPosition.y--;
				break;
			default:
				break;
		}

		_CurrentPosition.x = Mathf.Clamp(_CurrentPosition.x, 0, _GridSize.x - 1);
		_CurrentPosition.y = Mathf.Clamp(_CurrentPosition.y, 0, _GridSize.y - 1);

		if (_CurrentObject._Mesh != null)
		{ 
			_CurrentObject.SetState(GO_State.Mesh);
		}
		else
		{
			_CurrentObject.SetState(GO_State.Unselected);
		}

		foreach (GridObject obj in _GridObjects)
		{
			if (obj._Position == _CurrentPosition)
			{
				_CurrentObject = obj;
				break;
			}
		}

		_CurrentObject.SetState(GO_State.Selected);
	}

	public Vector3 GetCurrentPosition()
	{
		return new Vector3(_GridObjectSize * _CurrentPosition.x, 0, _GridObjectSize * _CurrentPosition.y);
	}
}
