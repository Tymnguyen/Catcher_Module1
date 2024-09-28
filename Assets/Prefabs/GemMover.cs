using UnityEngine;

public class GemMover : MonoBehaviour
{
    public float speed = 2f; 

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Phát hiện va chạm giữa viên ngọc này và một game object collider khác!");
        Debug.Log("đang xử lý va chạm...");

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Viên ngọc đã va chạm với game object có nhãn player");
            Destroy(gameObject);
            ScoreManager.instance.AddScore(1); 
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Viên ngọc đã va chạm với game object có nhãn ground");
            Destroy(gameObject);
            Debug.Log("Đã xóa viên ngọc này");
        }
    }
}
