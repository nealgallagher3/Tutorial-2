using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text winText;
    public Text livesText;
    private int lives = 3;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    Animator anim;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        SetScoreText();
        scoreValue = 0;
        winText.text = "";
        livesText.text = "Lives: " + lives.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        /*
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Jumping",true);
        }
        */
        if (vertMovement != 0)
        {
            anim.SetBool("Jumping", true);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (hozMovement != 0)
        {
            anim.SetInteger("State", 1);
        }
        else
        {
            anim.SetInteger("State", 0);
        }
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreText();
            Destroy(collision.collider.gameObject);
            
        }
        if (collision.collider.tag == "UFO")
        {
            lives = lives - 1;
            livesText.text = "Lives: " + lives.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (lives == 0)
        {
            Destroy(this);
            winText.text = "You Lose!";
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
            anim.SetBool("Jumping", false);
        }
    }
    void SetScoreText()
    {
        score.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 4)
        {
            transform.position = new Vector2(50.0f, 0.0f);
            lives = 3;
            livesText.text = "Lives: " + lives.ToString();
        }
        if (scoreValue == 8)
        {
            winText.text = "You win! Game created by Neal Gallagher!";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}