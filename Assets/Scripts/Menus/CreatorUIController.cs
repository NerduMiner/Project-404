using UnityEngine;

public class CreatorUIController : BaseUIController
{
	// for actual UI go to Scripts/Scene_Specific/Creator/GridManager.cs

	[SerializeField] GameObject _Canvas = null;
	[SerializeField] GameObject _GridManager = null;
	[SerializeField] GridCamera _GridCamera = null;

	private void OnEnable()
	{
		_GridManager.SetActive(false);
		_GridCamera.enabled = false;
	}

	public void OnAcknowledge()
	{
		_Canvas.SetActive(false);
		_GridManager.SetActive(true);
		_GridCamera.enabled = true;
	}
}
