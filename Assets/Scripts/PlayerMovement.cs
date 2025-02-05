using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D marioBody;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true; //by default Mario is facing right
    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public GameManager gameManager;

    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        //Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        //ResetGame(); // Call this method to set everything back when player spawn
    }

    // Update is called once per frame
    void Update()
    {
        //toggle state
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true; //flip sprite to face left
        }
        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false; //flip sprite to face right
        }
    }

    // Collider callback function, OnCollision2D.
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    // FixedUpdate is called 50 times a second and it is about Physics Engine
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            //check if it doesn't go beyond maxSpeed
            if (marioBody.velocity.magnitude < maxSpeed)
            {
                marioBody.AddForce(movement * speed);
            }

            //stop
            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                //stop
                marioBody.velocity = Vector2.zero;
            }
        }

        //when spacebar is pressed, add impulse force upwards
        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isDead)
        {
            isDead = true;
            Time.timeScale = 0.0f; //freeze time when mario hit goomba
            gameManager.gameOver();
        }
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }

    public JumpOverGoomba jumpOverGoomba;

    private void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-9.26f, -4.9f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // reset score
        scoreText.text = "Score: 0";
        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }
        //reset score
        jumpOverGoomba.score = 0;
    }

}