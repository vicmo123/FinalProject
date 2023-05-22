using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    readonly int syrupCanValue = 1000;
    readonly int claimedBucketValue = 250;
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

    public int CheckForBestScores() {
        Debug.Log("TEST");
        BestScores scores = LoadScores();
        int pos = scores.CheckScore(CalculateScore());
        if (pos != -1)
            scores.NewBest(pos, new ScoreInfo(this));
        SaveScores(scores);
        return pos;
    }

    public void SaveScores(BestScores scores) {
        BestScores bestScores = SerializeObject(scores) as BestScores;

        string jsonSerializationOfNewClass = JsonUtility.ToJson(bestScores);

        string directoryPath = Path.Combine(Application.streamingAssetsPath, "Leaderboard/");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string filePath;
        filePath = Path.Combine(directoryPath, "bestScores.txt");

        File.WriteAllText(filePath, jsonSerializationOfNewClass);
    }

    private object SerializeObject(BestScores scores) {
        return scores;
    }

    public BestScores LoadScores() {
        string directoryPath = Path.Combine(Application.streamingAssetsPath, "Leaderboard/");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        string filePath;
        filePath = Path.Combine(directoryPath, "bestScores.txt");

        if (File.Exists(filePath)) {
            string jsonDeserialized = File.ReadAllText(filePath);

            BestScores newClassLoadedFromJson = JsonUtility.FromJson<BestScores>(jsonDeserialized);
            DeserializeObject(newClassLoadedFromJson);
            return newClassLoadedFromJson;
        }
        else {
            Debug.Log("File not found.");
            return new BestScores();
        }
    }

    private void DeserializeObject(object o) {
        BestScores scores = o as BestScores;
        scores.CheckLength();
    }
}

[Serializable]
public class ScoreInfo
{
    public int nbClaimedBuckets, nbSyrupCans, bucketPoints, syrupCanPoints, bonusPoints, totalScore;

    public ScoreInfo(PlayerScore playerScore) {

        Debug.Log("Constructeur de ScoreInfo");
        totalScore = playerScore.CalculateScore();
        nbClaimedBuckets = playerScore.claimedBuckets;
        bucketPoints = playerScore.CalculateBuckets();

        nbSyrupCans = playerScore.syrupCans;
        syrupCanPoints = playerScore.CalculateSyrupCans();

        bonusPoints = playerScore.bonusPoints;
    }

    public ScoreInfo() {
        totalScore = 0;

        nbClaimedBuckets = 0;
        bucketPoints = 0;

        nbSyrupCans = 0;
        syrupCanPoints = 0;

        bonusPoints = 0;
    }
}

[Serializable]
public class BestScores
{
    public BestScores() {
        scores = new ScoreInfo[0];
        CheckLength();
    }
    public ScoreInfo[] scores;
    public void CheckLength() {
        int length = scores.Length;
        if (length == 5)
            return;
        else {
            ScoreInfo[] temp = new ScoreInfo[5];
            for (int i = 0; i < length; i++) {
                temp[i] = scores[i];
            }
            for (int i = length; i < 5; i++) {
                temp[i] = new ScoreInfo();
            }
            scores = temp;
        }
    }
    public int CheckScore(int score) {
        for (int i = 0; i < scores.Length; i++) {
            if (scores[i].totalScore < score)
                return i;
        }
        return -1;
    }
    public void NewBest(int position, ScoreInfo info) {
        scores[position] = info;
    }
}