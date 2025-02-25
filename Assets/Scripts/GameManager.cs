using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour{

    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void IncreaseScore(){
        score += 1;
        scoreText.text = $"Score: {score}";
    }
}
