using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanelController : MonoBehaviour
{
    public Text Title;

    private bool win = false;
    private Button button;

    void Awake()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(Continue);
    }
    private void Continue()
    {
        if (win)
            GameManager.Instance.NextLevel();
        else
            GameManager.Instance.TryAgain();

        this.gameObject.SetActive(false);
    }
    public void Win(bool win)
    {
        this.win = win;
        
        gameObject.SetActive(true);
        if (win)
            Title.text = "You Win";
        else
            Title.text = "You Lose";
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
