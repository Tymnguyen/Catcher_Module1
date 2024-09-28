using System.Collections;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float speedBoost = 2.0f; 
    public float duration = 5.0f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Pickup(collision));
        }
        else if (collision.CompareTag("Ground"))
        {
            Debug.Log("Power-Up đã va chạm với mặt đất và sẽ bị hủy.");
            Destroy(gameObject);
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        CharacterMovement movement = player.GetComponent<CharacterMovement>();

        movement.speed *= speedBoost;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        movement.speed /= speedBoost;

        Destroy(gameObject);
    }
}
