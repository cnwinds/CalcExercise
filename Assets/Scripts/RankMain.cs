using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int count = RankManager.Count();
        for(int i = 1; i <= 10; i++)
        {
            GameObject o = GameObject.Find("RankItem" + i);
            if (i < count)
            {
                Examine e = RankManager.Item(i - 1);
                Text rank = o.transform.FindChild("Rank").GetComponent<Text>();
                Text date = o.transform.FindChild("Date").GetComponent<Text>();
                Text rate = o.transform.FindChild("Rate").GetComponent<Text>();
                Text score = o.transform.FindChild("Score").GetComponent<Text>();
                rank.text = i.ToString();
                date.text = e.endTime.ToShortDateString() + " " + e.endTime.ToShortTimeString();
                rate.text = e.Rate().ToString();
                score.text = e.Score().ToString();
                o.SetActive(true);
            }
            else
            {
                o.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClose()
    {
        SceneManager.LoadScene("Menu");
    }


}
