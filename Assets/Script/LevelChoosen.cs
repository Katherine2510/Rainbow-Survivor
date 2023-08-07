using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelChoosen : MonoBehaviour
{
    // Start is called before the first frame update4
    public GameObject MainScreen;
    public GameObject Spin;
    public GameObject DailyReward;
    

    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text[] textMeshProObjects = FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text textMeshPro in textMeshProObjects)
        {
            if (textMeshPro.CompareTag("Gold"))
            {
                textMeshPro.text = PlayerPrefs.GetInt("gold").ToString();
            }
        }
        TMP_Text[] textMeshProObjects2 = FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text textMeshPro in textMeshProObjects2)
        {
            if (textMeshPro.CompareTag("Diamond"))
            {
                textMeshPro.text = PlayerPrefs.GetInt("diamond").ToString();
            }
        }
        
    }
    public void Level1() {
        if (GameManagement.Instance == null)
        {
            GameObject gameManagementObj = new GameObject("GameManagement");
            gameManagementObj.AddComponent<GameManagement>();
        }

        SceneManager.LoadScene(1);
        GameManagement.Instance.numOfEnemys = 1;
    }

    public void Level2() {
        if (GameManagement.Instance == null)
        {
            GameObject gameManagementObj = new GameObject("GameManagement");
            gameManagementObj.AddComponent<GameManagement>();
        }

        SceneManager.LoadScene(1);
        GameManagement.Instance.numOfEnemys = 2;
    }
    

    public void Level3() {
        if (GameManagement.Instance == null)
        {
            GameObject gameManagementObj = new GameObject("GameManagement");
            gameManagementObj.AddComponent<GameManagement>();
        }

        SceneManager.LoadScene(1);
        GameManagement.Instance.numOfEnemys = 3;
    }
    

    public void Level4() {
        if (GameManagement.Instance == null)
        {
            GameObject gameManagementObj = new GameObject("GameManagement");
            gameManagementObj.AddComponent<GameManagement>();
        }
        SceneManager.LoadScene(1);
        GameManagement.Instance.numOfEnemys = 4;
    
    }

    

    public void ToSpin() {
        MainScreen.SetActive(false);
        Spin.SetActive(true);

    }

    public void BackFromSpin() {
        MainScreen.SetActive(true);
        Spin.SetActive(false);
    }
     public void ToReward() {
        MainScreen.SetActive(false);
        DailyReward.SetActive(true);

    }

    public void BackFromReward() {
        MainScreen.SetActive(true);
        DailyReward.SetActive(false);
    }


}
