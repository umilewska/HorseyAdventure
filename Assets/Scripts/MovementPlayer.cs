using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float jump = 2f;
    public float speed = 3f;
    private float move;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
         
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isJumping = true;
        }
        
        if (transform.position.y < -10) // ograniczenie, aby gracz spadł z powrotem na ziemię
        {
            isJumping = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("Wyjście z gry...");
            SceneManager.LoadScene("Dupa");
            //Application.Quit(); //wyjście z aplikacji
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // gracz jest na ziemi, więc może skoczyć ponownie
        }
    }

    // private void FixedUpdate()
    // {
    //     float horizontal = Input.GetAxis("Horizontal");
    //     Vector2 translation = Vector2.right * (horizontal * speed * Time.fixedDeltaTime);
    //
    //     if (translation == Vector2.zero)
    //     {
    //         return;
    //     }
    //
    //     //rb.AddForce(translation);
    //     rb.position += translation;
    //
    // }
    
    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         isJumping = false;
    //     }
    // }
}


