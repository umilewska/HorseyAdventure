using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float jump;
    [SerializeField] public float speed;
    private float initialSpeed;
    private float move;
    private bool isJumping;
    private Vector3 respawnPoint;
    public bool canMove;
    private bool hasFinished;
    private float turboTimer;
    private float turboDuration;
    private bool isCooldown;
    private float cooldownTimer;
    [SerializeField] private Animator animator;
    [SerializeField] private Button turboBar;
    [SerializeField] private Button life1;
    [SerializeField] private Button life2;
    [SerializeField] private Button life3;
    [SerializeField] private Text lifesText;
    [SerializeField] private Text turboText;
    private int lifes;
    public AudioSource finishAudio;
    public AudioSource collisionAudio;
    public AudioSource cherryAudio;
    public AudioSource lickingAudio;
    public AudioSource turboAudio;
    
    private float initialWidthTurboBar; // pierwotna szerokość turboBaru

    // Start is called before the first frame update
    IEnumerator Start()
    {
        PlayerPrefs.SetInt("FinishFinish", 0);
        PlayerPrefs.SetInt("TimerStop", 0);
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        turboBar.GameObject().SetActive(false);
        life1.GameObject().SetActive(false);
        life2.GameObject().SetActive(false);
        life3.GameObject().SetActive(false);
        lifesText.GameObject().SetActive(false);
        turboText.GameObject().SetActive(false);
        yield return new WaitForSeconds(4f);
        turboBar.GameObject().SetActive(true);
        life1.GameObject().SetActive(true);
        life2.GameObject().SetActive(true);
        life3.GameObject().SetActive(true);
        lifesText.GameObject().SetActive(true);
        turboText.GameObject().SetActive(true);
        lifes = 3;
        canMove = true;
        turboTimer = 3;
        turboDuration = 3;
        cooldownTimer = 0;
        isCooldown = false;
        initialSpeed = speed;


        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
        initialWidthTurboBar = turboBar.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            speed = initialSpeed;
            move = 1;
            animator.SetBool("WalkBool", true);

            // zmiana toru
            if (Input.GetKeyDown(KeyCode.W) && transform.position.y < 0.4f)
            {
                Vector3 currentPosition = transform.position;
                currentPosition.y += 2.6f;
                transform.position = currentPosition;
            }
            if (Input.GetKeyDown(KeyCode.S) && transform.position.y > -8f)
            {
                Vector3 currentPosition = transform.position;
                currentPosition.y -= 2.6f;
                transform.position = currentPosition;
            }

            // turbo
            if (turboTimer >= turboDuration)
            {
                isCooldown = true;
            }
            if (turboTimer < turboDuration)
            {
                isCooldown = false;
            }
            if (!isCooldown && canMove && Input.GetKey(KeyCode.T))
            {
                if (!turboAudio.isPlaying)
                {
                    turboAudio.Play();
                }
                speed = 1.5f * speed;
                animator.SetBool("TurboBool", true);
                float targetWidth = Mathf.Lerp(160f, 10f, turboTimer / turboDuration);
                turboBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetWidth);
                turboTimer += Time.deltaTime;
                Debug.Log(turboTimer + " " + Input.GetKey(KeyCode.T));
            }
            if (isCooldown || !Input.GetKey(KeyCode.T))
            {
                animator.SetBool("TurboBool", false);
                turboAudio.Stop();
            }
            
            
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
            
            // jump
            // if (Input.GetButtonDown("Jump") && !isJumping)
            // {
            //     animator.SetTrigger("JumpTrigger");
            //     rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            //     isJumping = true;
            // }

            // if (transform.position.y < -10) // ograniczenie, aby gracz spadł z powrotem na ziemię
            // {
            //     isJumping = false;
            // }
        }
        
        // exit on ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetFloat("Level1Time", 0);
            PlayerPrefs.SetFloat("Level2Time", 0);
            PlayerPrefs.SetFloat("Level3Time", 0);
            Debug.Log("Wyjście z gry...");
            SceneManager.LoadScene("Menu");
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
        // water obstacle
        if (col.gameObject.CompareTag("Obstacle") && !isJumping)
        {
            lickingAudio.Play();
            var respawnPoint2 = transform.position;
            transform.position = respawnPoint2;
            rb.Sleep(); // rigidbobdy sleep
            canMove = false;
            animator.SetBool("LickBool", true);
            yield return new WaitForSeconds(3); // wstrzymaj ruch gracza na 3 sek
            rb.WakeUp();
            lickingAudio.Stop();
            animator.SetBool("LickBool", false);
            canMove = true;
        }

        // other obstacles
        if (col.gameObject.CompareTag("Obstacle2") && !isJumping)
        {
            collisionAudio.Play();
            transform.position = respawnPoint;
            lifes -= 1;
            if (lifes == 2)
            {
                life3.GameObject().SetActive(false);
            }

            if (lifes == 1)
            {
                life2.GameObject().SetActive(false);
            }

            if (lifes == 0)
            {
                life3.GameObject().SetActive(false);
                SceneManager.LoadScene("Gameover");
            }
        }

        // eat cherry get turbo
        if (col.gameObject.CompareTag("Cherry") &&
            turboBar.GetComponent<RectTransform>().rect.width <= 140 && turboTimer > 0)
        {
            turboBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 
                turboBar.GetComponent<RectTransform>().rect.width + 20f);
            turboTimer -= 0.5f;
        }
        
        if (col.gameObject.CompareTag("Cherry"))
        {
            cherryAudio.Play();
        }
        
        // finish level 1
        if (col.gameObject.CompareTag("FinishLevel1"))
        {
            if (turboAudio.isPlaying)
            {
                turboAudio.Stop();
            }
            finishAudio.Play();
            canMove = false;
            hasFinished = true;
            PlayerPrefs.SetInt("PlayerHasFinished", 1);
            PlayerPrefs.SetInt("TimerStop", 1);
            animator.SetTrigger("FinishTrigger");
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("Level2");
        }
        
        // finish level 2
        if (col.gameObject.CompareTag("FinishLevel2"))
        {
            if (turboAudio.isPlaying)
            {
                turboAudio.Stop();
            }
            finishAudio.Play();
            canMove = false;
            hasFinished = true;
            PlayerPrefs.SetInt("PlayerHasFinished", 1);
            PlayerPrefs.SetInt("TimerStop", 1);
            animator.SetTrigger("FinishTrigger");
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("Level3");
        }
        
        // finish
        if (col.gameObject.CompareTag("Finish"))
        {
            if (turboAudio.isPlaying)
            {
                turboAudio.Stop();
            }
            finishAudio.Play();
            canMove = false;
            hasFinished = true;
            PlayerPrefs.SetInt("PlayerHasFinished", 1);
            PlayerPrefs.SetInt("TimerStop", 1);
            animator.SetTrigger("FinishTrigger");
            yield return new WaitForSeconds(3);
            PlayerPrefs.SetInt("FinishFinish", 1);
            SceneManager.LoadScene("Menu");
        }

        else
        {
            PlayerPrefs.SetInt("PlayerHasFinished", 0);
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
    
    
}


