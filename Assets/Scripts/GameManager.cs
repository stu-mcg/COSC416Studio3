using UnityEngine;

public class GameManager : MonoBehaviour{

    private int score = 0;

    public void IncreaseScore(){
        score += 1;
        Debug.Log(score);
    }

    public int GetScore(){
        return score;
    }
}
