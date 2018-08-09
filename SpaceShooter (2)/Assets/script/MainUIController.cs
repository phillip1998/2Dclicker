using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainUIController : MonoBehaviour {

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text hpText, gameoverText ,restartText,lifeText;
	// Use this for initialization
	void Start () {
		
	}
    public void setHP(int value)
    {
        string str = "";
        for (int i = 0; i < value; i++) str += "♥ ";
        hpText.text = str;
    }
	public void setScore(int value)
    {
        scoreText.text = string.Format("Score : {0}", value.ToString());
    }
    public void setLife(int value)
    {
        lifeText.text = string.Format("x {0}", value.ToString());
    }
    public void setGameOver()
    {
        gameoverText.text = "Game Over";
        restartText.gameObject.SetActive(true);
    }
    public void hideGameOver()
    {
        gameoverText.text = "";
        restartText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
