using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class UIScoreMenu : MonoBehaviour
{
    public TMP_Text gameDurationText;
    public UIScorePage[] scorePages;

    private void Start()
    {
        PlayerScore[] playerScores;
        playerScores = UIManager.Instance.GetScores();

        FillScorePages(playerScores);
        DisplayGameDuration(UIManager.Instance.gameDuration);
    }
    private void DisplayGameDuration(float duration)
    {
        float durationInMinutes = duration / 60.0f;
        gameDurationText.text = durationInMinutes.ToString() + " min";
    }

    private void FillScorePages(PlayerScore[] players)
    {
        int winnerIndex = GetWinnerIndex(players);
        Debug.Log("Winner  is :" + winnerIndex);

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
            if (players[0].totalScore == players[1].totalScore)
                return -1;
            else
                return players[0].totalScore > players[1].totalScore ? 0 : 1;
        }
        else
        {
            throw new System.Exception("PlayerScore is null in UIScoreMenu");
        }
    }


}