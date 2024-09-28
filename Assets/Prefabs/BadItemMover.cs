using UnityEngine;

public class BadItemMover : MonoBehaviour
{
    public float speed = 2f; 
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Vật phẩm xấu đã chạm đất");
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Nhân vật chạm vào vật phẩm xấu. Trừ điểm!");
            ScoreManager.instance.DecreaseScore(1); 
            Destroy(gameObject);
        }
    }
}
