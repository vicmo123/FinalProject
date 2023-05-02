using System;

[Serializable]
public class PlayerScore
{
    string playerName;

    Player player;
    public int syrupCans, claimedBuckets, bonusPoints;
    [NonSerialized]
    readonly int syrupCanValue = 1000;
    [NonSerialized]
    readonly int claimedBucketValue = 100;

    public PlayerScore(Player _player) {
        player = _player;

        bonusPoints = 0;
        claimedBuckets = 0;
    }

    public void AddBonus(int bonus) {
        bonusPoints += bonus;
    }

    public int Calculate() {
        syrupCans = player.syrupCanManager.GetCanCount();
        foreach (var bucket in BucketManager.Instance.buckets) {
            if (bucket.player == player)
                claimedBuckets++;
        }

        return syrupCans * syrupCanValue + claimedBuckets * claimedBucketValue + bonusPoints;
    }

    public static Player GetWinner(Player _player1, Player _player2) {
        int player1Score = _player1.playerScore.Calculate();
        int player2Score = _player2.playerScore.Calculate();

        if (player1Score > player2Score)
            return _player1;
        if (player2Score > player1Score)
            return _player2;
        else
            return null;
    }
}
