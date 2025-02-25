using UnityEngine;

public class Coin : MonoBehaviour{
    private GameManager gameManager;
    [SerializeField] private float rotationSpeed = 50f;
    void Start(){
        if (gameManager == null){
            gameManager = Object.FindFirstObjectByType<GameManager>();
        }   
    }
    void Update(){
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("player")){
            Destroy(gameObject);
            gameManager.IncreaseScore();
        }
    }
}
