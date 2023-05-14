using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore
{
    public enum Bonus
    {
        SNOWBALL_HIT_PLAYER,
        SNOWBALL_HIT_ANIMAL,
        ICEBALL_HIT_PLAYER,
        ICEBALL_HIT_ANIMAL,
        GIANT_SNOWBALL_HIT_PLAYER,
        GIANT_SNOWBALL_HIT_ANIMAL,
        APPLE,
        HORN
    };

    string playerName;
    public int playerIndex;

    Player player;
    private FloatingPointsHandler floatPointsEffect;
    public int syrupCans, totalSyrupCan, claimedBuckets, totalClaimedBuckets, bonusPoints, totalScore;
    [NonSerialized]
    readonly int syrupCanValue = 1000;
    [NonSerialized]
    readonly int claimedBucketValue = 250;
    [NonSerialized]
    readonly Dictionary<Bonus, int> bonus = new Dictionary<Bonus, int>() {
        { Bonus.SNOWBALL_HIT_PLAYER, 50},
        { Bonus.SNOWBALL_HIT_ANIMAL, 40},
        { Bonus.ICEBALL_HIT_PLAYER, 80},
        { Bonus.ICEBALL_HIT_ANIMAL, 60},
        { Bonus.GIANT_SNOWBALL_HIT_PLAYER, 80},
        { Bonus.GIANT_SNOWBALL_HIT_ANIMAL, 60},
        { Bonus.APPLE, 40},
        { Bonus.HORN, 40},
    };

    public PlayerScore(Player _player, FloatingPointsHandler _floatPointsEffect)
    {
        Debug.Log("Constructor of PLayerScore of player : " + _player.index);
        player = _player;

        if (player == null)
            throw new Exception("Player is null in PlayerScore");
        else
            playerIndex = player.index;

        floatPointsEffect = _floatPointsEffect;
        bonusPoints = 0;
    }

    public int GetPlayerIndex()
    {
        return this.playerIndex;
    }

    public void AddBonus(Bonus bonusType)
    {
        Debug.Log("Add Bonus function");
        bonusPoints += bonus[bonusType];

        //FloatingPoints Effect here with color and bonus points
        floatPointsEffect.makePointsEffect.Invoke(bonus[bonusType].ToString());
    }

    public int CalculateBuckets()
    {
        claimedBuckets = 0;
        int i = 0;
        foreach (var bucket in BucketManager.Instance.buckets)
        {
            if (bucket.player)
                if(bucket.player.index == playerIndex)
                claimedBuckets++;
            i++;
        }
        //claimedBuckets = 29 - claimedBuckets;
        Debug.Log("Nb of buckets : " + claimedBuckets + " for player : " + playerIndex);
        totalClaimedBuckets = claimedBuckets * claimedBucketValue;
        return claimedBuckets * claimedBucketValue;
    }

    public int CalculateSyrupCans()
    {
        syrupCans = player.syrupCanManager.GetCanCount();
        totalSyrupCan = syrupCans * syrupCanValue;
        return syrupCans * syrupCanValue;
    }

    public int CalculateScore()
    {
        totalScore = CalculateSyrupCans() + CalculateBuckets() + bonusPoints;
        return CalculateSyrupCans() + CalculateBuckets() + bonusPoints;
    }

    public static Player GetWinner(Player _player1, Player _player2)
    {
        int player1Score = _player1.playerScore.CalculateScore();
        int player2Score = _player2.playerScore.CalculateScore();

        if (player1Score > player2Score)
            return _player1;
        if (player2Score > player1Score)
            return _player2;
        else
            return null;
    }

    public PlayerScore GetSavedPlayerScore()
    {
        CalculateScore();
        return this;
    }

    //public ScoreInfo GetScoreInfo()
    //{
    //    return new ScoreInfo(this);
    //}
}

//[Serializable]
//public class ScoreInfo
//{
//    public int nbClaimedBuckets, nbSyrupCans, bucketPoints, syrupCanPoints, bonusPoints, totalScore;

//    public ScoreInfo(PlayerScore playerScore)
//    {

//        Debug.Log("Constructeur de ScoreInfo");
//        totalScore = playerScore.CalculateScore();
//        nbClaimedBuckets = playerScore.claimedBuckets;
//        bucketPoints = playerScore.CalculateBuckets();

//        nbSyrupCans = playerScore.syrupCans;
//        syrupCanPoints = playerScore.CalculateSyrupCans();

//        bonusPoints = playerScore.bonusPoints;
//    }
//}