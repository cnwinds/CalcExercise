using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RankManager.Load();
        WrongManager.Load();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnNormalExceise()
    {
        SceneManager.LoadScene("Level0");
    }

    public void OnRank()
    {
        SceneManager.LoadScene("Rank");
    }
}
