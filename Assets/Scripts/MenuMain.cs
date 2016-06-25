using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
	public bool isNewExceise;

	// Use this for initialization
	void Start ()
	{
		Object.DontDestroyOnLoad (this);

		MenuMain[] list = FindObjectsOfType<MenuMain> ();
		if (list.Length > 1) {
			Destroy (list[0]);
			return;
		}

		RankManager.Load ();
		WrongManager.Load ();
	}

	public void OnNewExceise ()
	{
		isNewExceise = true;
		SceneManager.LoadScene ("Level0");
	}
		
	public void OnRank ()
	{
		SceneManager.LoadScene ("Rank");
	}

	public void OnWrongExceise ()
	{
		isNewExceise = false;
		SceneManager.LoadScene ("Level0");
	}
}
