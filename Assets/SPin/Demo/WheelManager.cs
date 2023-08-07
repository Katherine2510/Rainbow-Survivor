using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WheelManager : MonoBehaviour {

    SpinWheel wheel = new SpinWheel(8);
    int gold;
    int diamond;
    public TMP_Text text;
    public GameObject go;
    public GameObject win;
    public TMP_Text winT;
	void Start () {
        gold = PlayerPrefs.GetInt("gold");
        diamond = PlayerPrefs.GetInt("diamond");
        UpdateText();
        wheel.setWheel(gameObject);
        wheel.AddCallback((index) => {
            switch (index)
            {
                case 1:
                    gold += 100;
                    PlayerPrefs.SetInt("gold", gold);
                    win.SetActive(true);
                    winT.text = "100 gold";
                    break;
                case 2:
                    diamond += 5;
                    PlayerPrefs.SetInt("diamond", diamond);
                    win.SetActive(true);
                    winT.text = "5 diamond";
                    break;
                case 3:
                    gold += 100;
                    PlayerPrefs.SetInt("gold", gold);
                    win.SetActive(true);
                    winT.text = "100";
                    break;
                case 4:
                    diamond += 1;
                    PlayerPrefs.SetInt("diamond", diamond);
                    win.SetActive(true);
                    winT.text = "1 diamond";
                    break;
                case 5:
                    gold += 200;
                    PlayerPrefs.SetInt("gold", gold);
                    win.SetActive(true);
                    winT.text = "200";
                    break;
                case 6:
                    diamond += 2;
                    PlayerPrefs.SetInt("diamond", diamond);
                    win.SetActive(true);
                    winT.text = "2 diamond";
                    break;
                case 7:
                    gold += 200;
                    PlayerPrefs.SetInt("gold", gold);
                    win.SetActive(true);
                    winT.text = "200";
                    break;
                case 8:
                    diamond += 3;
                    PlayerPrefs.SetInt("diamond", diamond);
                    win.SetActive(true);
                    winT.text = "3 diamond";
                    break;
            }
            UpdateText();
        });
	}

    public void UpdateText()
    {
        text.text = gold + "";
    }


    public void OffWin() {
        win.SetActive(false);
    }
    public void OkWin()
    {
        win.SetActive(false);
    }

    public void Spin()
    {
        StartCoroutine(wheel.StartNewRun());
        if (gold >= 300)
        {
            //gold -= 300;
            PlayerPrefs.SetInt("gold", gold);

            
            UpdateText();
        } else
        {
            go.SetActive(true);
        }
    }

    public void ok()
    {
        go.SetActive(false);
    }
}
