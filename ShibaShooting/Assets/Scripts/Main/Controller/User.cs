using UnityEngine;
[System.Serializable]
public class User
{
    [SerializeField] private int highScore;
    [SerializeField] private int coin;
    [SerializeField] private bool isCompleteTutorial;
    public bool isVibrate;

    public void SetIsCompleteTutorial(bool isComplete)
    {
        isCompleteTutorial = isComplete;
    }
    public bool GetIsCompleteTutorial()
    {
        return isCompleteTutorial;
    }

    public int GetHighScore()
    {
        return highScore;
    }
    public void SetHighScore(int highScore)
     => this.highScore = highScore;

    public int GetCoin()
    {
        return coin;
    }

    public void AddCoin(int addCoin)
    {
        coin += addCoin;
    }
}