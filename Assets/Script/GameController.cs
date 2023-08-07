using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
   public float gameDuration = 120f; // Thời lượng game (đơn vị: giây)
   public float readyTime = 10f;
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
    public GameObject Door1;
    public GameObject Door2;

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
        SceneManager.LoadScene(1);
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
    public IEnumerator DelayedFunction1()
    {
        yield return new WaitForSeconds(3f);
         if (!isWinToPnl){
            //StartCoroutine(DelayedFunction1());
            Debug.Log("AreCatched");
            pnlCatchedGame.SetActive(true);
            PlayState.SetActive(false);       
        }
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
       StartCoroutine(DelayedFunction1());
        
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
     public IEnumerator ReadyCountdown()
    {
        while (remainingTime > 0 && !isWinToPnl)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
            UpdateCountdownText();
        }
        GameManagement.Instance.inReadyTime = false;
        Door1.SetActive(false);
        Door2.SetActive(false);
        remainingTime = gameDuration;
        StartCoroutine(StartCountdown());
  
    }

    private void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartGame() {
        remainingTime = readyTime;
        StartCoroutine(ReadyCountdown());
        
    }
    public void OnPlayState() {

        PlayState.SetActive(true);
        StartGame();
        tutorial.SetActive(false);
    }

}
