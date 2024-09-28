using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 2f; 
    private Vector2 direction = Vector2.right;
    private bool isMoving = true; 

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; 
        }
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Right Boundary"))
        {
            direction = Vector2.left;
        }
        else if (collision.CompareTag("Left Boundary"))
        {
            direction = Vector2.right;
        }
        else if (collision.CompareTag("Player"))
        {
            isMoving = false;
            ScoreManager.instance.GameOver(); 
            Debug.Log("Vật cản đã chạm vào nhân vật và dừng lại. Game Over!");
        }
    }
    public void StartMovement(Vector2 initialDirection)
    {
        direction = initialDirection;
    }

    public void StopMovement()
    {
        isMoving = false;
    }
}
