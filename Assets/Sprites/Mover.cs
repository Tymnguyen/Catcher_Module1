using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    void Start()
    {
        
    }
    public float speed = 5.0f;
    void Update()
    {
    float horizontalInput = Input.GetAxis("Horizontal");
    float moveHorizontal = horizontalInput * speed * Time.deltaTime;
    transform.position = new Vector2(transform.position.x + moveHorizontal, transform.position.y);
	}
}