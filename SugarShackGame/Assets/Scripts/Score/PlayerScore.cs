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
    int playerIndex;

    Player player;
    private FloatingPointsHandler floatPointsEffect;
    public int syrupCans, claimedBuckets, bonusPoints;
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

    public PlayerScore(Player _player, FloatingPointsHandler _floatPointsEffect) {
        player = _player;
        playerIndex = player.playerInput.playerIndex;
        floatPointsEffect = _floatPointsEffect;
        bonusPoints = 0;
    }

    public int GetPlayerIndex()
    {
        return this.playerIndex;
    }

    public void AddBonus(Bonus bonusType) {
        bonusPoints += bonus[bonusType];

        //FloatingPoints Effect here with color and bonus points
        floatPointsEffect.makePointsEffect.Invoke(bonus[bonusType].ToString());
    }

    public int CalculateBuckets() {
        claimedBuckets = 0;
        foreach (var bucket in BucketManager.Instance.buckets) {
            if (bucket.player == player)
                claimedBuckets++;
        }
        return claimedBuckets* claimedBucketValue;
    }

    public int CalculateSyrupCans() {
        syrupCans = player.syrupCanManager.GetCanCount();
        return syrupCans * syrupCanValue;
    }

    public int CalculateScore() {
        return CalculateSyrupCans() + CalculateBuckets() + bonusPoints;
    }

    public static Player GetWinner(Player _player1, Player _player2) {
        int player1Score = _player1.playerScore.CalculateScore();
        int player2Score = _player2.playerScore.CalculateScore();

        if (player1Score > player2Score)
            return _player1;
        if (player2Score > player1Score)
            return _player2;
        else
            return null;
    }

    public ScoreInfo GetScoreInfo() {
        return new ScoreInfo(this);
    }
}

[Serializable]
public class ScoreInfo
{
    public int nbClaimedBuckets, nbSyrupCans, bucketPoints, syrupCanPoints, bonusPoints, totalScore;

    public ScoreInfo(PlayerScore playerScore) {
        totalScore = playerScore.CalculateScore();

        nbClaimedBuckets = playerScore.claimedBuckets;
        bucketPoints = playerScore.CalculateBuckets();

        nbSyrupCans = playerScore.syrupCans;
        syrupCanPoints = playerScore.CalculateSyrupCans();

        bonusPoints = playerScore.bonusPoints;
    }
}