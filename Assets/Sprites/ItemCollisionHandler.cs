using UnityEngine;

public class ItemCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; 
                Debug.Log($"{gameObject.name} đã chạm đất và dừng lại.");
            }
        }
        else if (collision.CompareTag("Player"))
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.DecreaseScore(1); 
            }
            Destroy(gameObject); 
            Debug.Log($"{gameObject.name} đã chạm vào nhân vật và bị hủy.");
        }
    }
}
