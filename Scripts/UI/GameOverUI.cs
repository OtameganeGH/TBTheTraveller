using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
	private void Start()
	{
		Cursor.lockState = CursorLockMode.None;
	}
	public void ChangeScene(int scene)
	{
		SceneManager.LoadScene(scene);
	}
	public void QuitGame()
	{
		Application.Quit();
	}



}
