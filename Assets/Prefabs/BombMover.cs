using UnityEngine;

public class BombMover : MonoBehaviour
{
    public float speed = 2f; 

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bom đã va chạm với người chơi!");
            FindObjectOfType<ScoreManager>().GameOver(); 
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Bom đã chạm đất");
            Destroy(gameObject); 
        }
    }
}
