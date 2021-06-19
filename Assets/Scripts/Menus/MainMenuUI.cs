/*
 * MainMenuUI.cs
 * Created by: Ambrosia
 * Created on: 25/4/2020 (dd/mm/yy)
 * Created for: needing a UI for the main menu
 */

using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : BaseUIController
{
	[Header("Components")]
	[SerializeField] private Transform _Canvas = null;
	[SerializeField] private Transform _TemplateButton = null;

	private void Awake()
	{
		if (Application.isEditor || Debug.isDebugBuild)
		{
			bool skippedCurrent = false;
			// Generate options for every scene
			for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
			{
				// Don't generate an option for the current scene
				if (SceneUtility.GetScenePathByBuildIndex(i) == SceneManager.GetActiveScene().path)
				{
					skippedCurrent = true;
					continue;
				}

				string sceneName = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

				Transform obj = Instantiate(_TemplateButton, _Canvas);
				obj.GetComponentInChildren<Text>().text = sceneName;
				obj.GetComponent<Button>().onClick.AddListener(() => Globals._FadeManager.FadeInOut(2, 1, () => SceneManager.LoadScene(sceneName)));
				obj.GetComponent<RectTransform>().localPosition = new Vector3(-450, 300 - ((skippedCurrent ? i - 1 : i) * 100));
				obj.gameObject.SetActive(true);
			}

			// Reparent because we want it to overlay everything
			_FadePanel.transform.SetParent(null);
			_FadePanel.transform.SetParent(_Canvas);
		}
	}

	public void PressPlay()
	{
		Globals._FadeManager.FadeInOut(2, 1, () => SceneManager.LoadScene("scn_demo_level"));
	}

	public void PressExit()
	{
		Application.Quit();
		Debug.Break();
	}
}
