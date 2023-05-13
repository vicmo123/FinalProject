using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScorePage : MonoBehaviour
{
    PlayerScore player;
    public TMP_Text indexPlayerText;

    public Image winnerImg;
    public Image loserImg;

    public TMP_Text nbBucketsText;
    public TMP_Text totalBocuketText;

    public TMP_Text nbCansText;
    public TMP_Text totalCansText;

    public TMP_Text totalBonusText;
    public TMP_Text TotalScoreText;
    

    public void DisplayScore(PlayerScore player, bool isWinner)
    {
        this.player = player;
        //Show winner or loser image
        if (isWinner == true)
            Winner();
        else
            Loser();

        indexPlayerText.text = "Player " + player.GetPlayerIndex().ToString();

        nbBucketsText.text = player.claimedBuckets.ToString();
        totalBocuketText.text = player.CalculateBuckets().ToString();

        nbCansText.text = player.syrupCans.ToString();
        totalCansText.text = player.CalculateSyrupCans().ToString();

        totalBonusText.text = player.bonusPoints.ToString();
        TotalScoreText.text = player.CalculateScore().ToString();

    }

    private void Winner()
    {
        winnerImg.gameObject.SetActive(true);
        loserImg.gameObject.SetActive(false);
    }

    private void Loser()
    {
        winnerImg.gameObject.SetActive(false);
        loserImg.gameObject.SetActive(true);
    }
}
