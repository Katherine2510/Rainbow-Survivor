using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
     public float gameDuration = 60f; // Thời lượng game (đơn vị: giây)
    private float remainingTime; 
    public GameObject pnlWinGame;
    public GameObject pnlTimeUpGame;
    public GameObject pnlCatchedGame;
    public GameObject tutorial;
    public GameObject PlayState;
    public Text countdownText;
    public Button gotItBtn;
    public Button replayAfterCatched;
    public Button replayAfterTimeUp;

    public Button rePlayAfterWin;
    bool isWinToPnl;
    public GameObject CharacterMovement;

    void Start()
    {

        tutorial.SetActive(true);
        pnlWinGame.SetActive(false);
        pnlTimeUpGame.SetActive(false);
        PlayState.SetActive(false);
        pnlCatchedGame.SetActive(false);
        //Các sự kiện click chuột
		gotItBtn.onClick.AddListener(OnPlayState);
        replayAfterCatched.onClick.AddListener(Re_AreCatched);
        replayAfterTimeUp.onClick.AddListener(Re_Lose);
        rePlayAfterWin.onClick.AddListener(Re_Win);
       

    }

    void Update()
    {
         isWinToPnl = CharacterMovement.GetComponent<CharacterMovement>().isWin;
    }
    public void ResetGame() {
        SceneManager.LoadScene(0);
        tutorial.SetActive(false);
        pnlWinGame.SetActive(false);
        pnlTimeUpGame.SetActive(false);
        PlayState.SetActive(true);
        pnlCatchedGame.SetActive(false);
        StartGame();
    }
    public void Win () {
        Debug.Log("You are Surviver");
        pnlWinGame.SetActive(true);
        PlayState.SetActive(false);
        //isWinToPnl = true;

    }
    public IEnumerator DelayedFunction()
    {
        yield return new WaitForSeconds(3f);
        Win();
    }

    public void StartDelayBeforeWin() {
        StartCoroutine(DelayedFunction());
    }

  
    public void Re_Win() {
        ResetGame();
    }

    public void Lose () {
        if (!isWinToPnl){
            Debug.Log("Time up!!!!");
            pnlTimeUpGame.SetActive(true);
            PlayState.SetActive(false);
            
        }
        
    }

    public void Re_Lose() {
        ResetGame();
    }

    public void AreCatched() {
        if (!isWinToPnl){
            Debug.Log("Time up!!!!");
            pnlCatchedGame.SetActive(true);
            PlayState.SetActive(false);
           
        }
        
    }

    public void Re_AreCatched () {
       ResetGame();
    }
     public IEnumerator StartCountdown()
    {
        while (remainingTime > 0 && !isWinToPnl)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
            UpdateCountdownText();
        }
        Lose();
    }

    private void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartGame() {
        
        remainingTime = gameDuration;
        StartCoroutine(StartCountdown());
    }
    public void OnPlayState() {

        PlayState.SetActive(true);
        StartGame();
        tutorial.SetActive(false);
    }

}
