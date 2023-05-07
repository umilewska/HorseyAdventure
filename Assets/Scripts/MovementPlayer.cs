using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float jump = 3f;
    public float speed = 3f;
    private float move;
    private bool isJumping;
    private Vector3 respawnPoint;
    public bool canMove;
    private bool hasFinished;
    private float turboTimer;
    private bool isCooldown;
    private float cooldownTimer;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        yield return new WaitForSeconds(4f); 
        canMove = true;
        turboTimer = 0;
        cooldownTimer = 0;
        isCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            speed = 3f;
            move = Input.GetAxis("Horizontal");
            
            if (!isCooldown && Input.GetKey(KeyCode.T))
            {
                speed = 6f;
                turboTimer += Time.deltaTime;
            }
            if (turboTimer >= 3)
            {
                isCooldown = true;
                cooldownTimer = 0;
                speed = 3f;
            }
            
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
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Wyjście z gry...");
            SceneManager.LoadScene("Menu");
            //Application.Quit(); //wyjście z aplikacji
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // gracz jest na ziemi, więc może skoczyć ponownie
        }

    }

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle") && !isJumping)
        {
            respawnPoint = transform.position; //+ new Vector3(1.5f,0,0); // wznów zza przeszkody
            transform.position = respawnPoint;
            canMove = false;
            yield return new WaitForSeconds(3); // wstrzymaj ruch gracza na 3 sek
            canMove = true;
        }

        if (col.gameObject.CompareTag("Finish"))
        {
            yield return new WaitForSeconds(1);
            canMove = false;
            hasFinished = true;
            SceneManager.LoadScene("Menu");
        }
    }

    public bool HasFinished()
    {
        return hasFinished;
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


