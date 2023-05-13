using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class UIScoreMenu : MonoBehaviour
{
    private bool DEBUG = true;
    public TMP_Text gameDurationText;
    public UIScorePage[] scorePages;

    private void Start()
    {
        PlayerScore[] playerScores;
        if (DEBUG)        
            playerScores = FakeFillPlayerScores();
        
        else        
            playerScores = UIManager.Instance.GetScores();
               
        FillScorePages(playerScores);
    }
    private void DisplayGameDuration(float duration)
    {
        gameDurationText.text = duration.ToString();
    }

    private void FillScorePages(PlayerScore[] players)
    {
        int winnerIndex = GetWinnerIndex(players);

        for (int i = 0; i < scorePages.Length; i++)
        {
            //if score is equal : both players are winners
            winnerIndex = winnerIndex == -1 ? i : winnerIndex;

            scorePages[i].DisplayScore(players[i], (winnerIndex == i));
        }
    }

    private int GetWinnerIndex(PlayerScore[] players)
    {
        if (players.Length > 1)
        {
            //Are the score equals ?
            if (players[0].CalculateScore() == players[1].CalculateScore())
                return -1;
            else
                return players[0].CalculateScore() > players[1].CalculateScore() ? 0 : 1;
        }
        else
        {
            throw new System.Exception("PlayerScore is null in UIScoreMenu");
        }
    }

    private PlayerScore[] FakeFillPlayerScores()
    {
        PlayerScore[] playerScores = new PlayerScore[2];
        for (int i = 0; i < playerScores.Length; i++)
        {
            //
        }

        throw new System.Exception("Fake fill playerScore is not done yet!");
        return playerScores;
    }
}