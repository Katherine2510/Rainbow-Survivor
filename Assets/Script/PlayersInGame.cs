using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersInGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Text PlayersUI;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CountAliveBots();
    }
      private  void CountAliveBots()
    {
        GameObject[] botObjects = GameObject.FindGameObjectsWithTag("bot");
        int aliveBotsCount = 0;

        foreach (GameObject bot in botObjects)
        {
            // Check if the bot is active (not destroyed)
            if (bot.activeSelf)
            {
                aliveBotsCount++;
            }
        }
        int players = aliveBotsCount + 1;

       PlayersUI.text = players + "/10";
    }
}
