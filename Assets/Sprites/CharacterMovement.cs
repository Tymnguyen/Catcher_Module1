using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 7.0f;
    public int maxJumpCount = 2;

    private Rigidbody2D rb;
    private int jumpCount;
    private bool isGrounded;

    public AudioSource jumpSound;

    private float leftBoundary = -11.0f; 
    private float rightBoundary = 11.0f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (jumpSound == null)
        {
            Debug.LogWarning("AudioSource cho jumpSound chưa được gán trên nhân vật.");
        }
        else if (jumpSound.clip == null)
        {
            Debug.LogWarning("AudioClip của jumpSound chưa được gán.");
        }
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        // Giới hạn nhân vật không chạy ra ngoài màn hình
        float clampedX = Mathf.Clamp(transform.position.x, leftBoundary, rightBoundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;

            if (jumpSound != null && jumpSound.clip != null)
            {
                jumpSound.PlayOneShot(jumpSound.clip);
            }
            else
            {
                Debug.LogWarning("Không thể phát âm thanh nhảy vì AudioSource chưa được gán.");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ScoreManager.instance.GameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gem") && (isGrounded || maxJumpCount > 0))
        {
            CollectGem(collision.gameObject);
        }
        else if (collision.CompareTag("BadItem"))
        {
            CollectBadItem(collision.gameObject);
        }
    }

    void CollectGem(GameObject gem)
    {
        Destroy(gem);
        ScoreManager.instance.AddScore(1);
    }

    void CollectBadItem(GameObject badItem)
    {
        Destroy(badItem);
        ScoreManager.instance.DecreaseScore(1);
    }
}
