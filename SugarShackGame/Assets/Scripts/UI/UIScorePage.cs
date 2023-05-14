using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScorePage : MonoBehaviour
{
    public TMP_Text indexPlayerText;

    public Image winnerImg;
    public Image loserImg;

    public TMP_Text nbBucketsText;
    public TMP_Text totalBocuketText;

    public TMP_Text nbCansText;
    public TMP_Text totalCansText;

    public TMP_Text totalBonusText;
    public TMP_Text TotalScoreText;
    

    public void DisplayScore(PlayerScore playerScore, bool isWinner)
    {
        //Show winner or loser image
        if (isWinner == true)
            Winner();
        else
            Loser();

        indexPlayerText.text = "Player " + playerScore.GetPlayerIndex().ToString();

        //Calculate total of buckets BEFORE getting the number. Important.
        totalBocuketText.text = playerScore.totalClaimedBuckets.ToString();
        nbBucketsText.text = playerScore.claimedBuckets.ToString();

        totalCansText.text = playerScore.totalSyrupCan.ToString();
        nbCansText.text = playerScore.syrupCans.ToString();

        totalBonusText.text = playerScore.bonusPoints.ToString();
        TotalScoreText.text = playerScore.totalScore.ToString();

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
