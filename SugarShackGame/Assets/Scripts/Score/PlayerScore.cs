using System;
using System.Collections.Generic;

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

    Player player;
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

    public PlayerScore(Player _player) {
        player = _player;

        bonusPoints = 0;
    }

    public void AddBonus(Bonus bonusType) {
        bonusPoints += bonus[bonusType];
    }

    private int CalculateBuckets() {
        claimedBuckets = 0;
        foreach (var bucket in BucketManager.Instance.buckets) {
            if (bucket.player == player)
                claimedBuckets++;
        }
        return claimedBuckets* claimedBucketValue;
    }

    private int CalculateSyrupCans() {
        syrupCans = player.syrupCanManager.GetCanCount();
        return syrupCans * syrupCanValue;
    }

    private int CalculateScore() {
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
    public int claimedBuckets, syrupCans, bucketPoints, syrupCanPoints, bonusPoints, score;

    public ScoreInfo(PlayerScore playerScore) {
        score = playerScore.CalculateScore();

        claimedBuckets = playerScore.claimedBuckets;
        bucketPoints = playerScore.CalculateBuckets();

        syrupCans = playerScore.syrupCans;
        syrupCanPoints = playerScore.CalculateSyrupCans();

        bonusPoints = playerScore.bonusPoints;
    }
}