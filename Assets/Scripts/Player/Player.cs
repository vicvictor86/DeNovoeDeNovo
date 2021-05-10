using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D rig;
    public Transform groundCheck;
    public LayerMask isGround;
    public Animator animator;
    public Light2D lightPost;
    public GameObject lampPost;
    public GameObject button;

    [SerializeField] private float speed = 4;
    [SerializeField] private float jumpForce = 270;

    [SerializeField]private bool isGrounded = false;
    private bool lookingRight = true;

    private int contador = 0;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck").GetComponent<Transform>();
        //isGround = LayerMask.GetMask("Ground");

        lampPost = GameObject.Find("LampPost");
        button = GameObject.Find("Button");
        lightPost = lampPost.GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPlayer();
        }
        CheckJumping();
        
    }

    private void CheckJumping()
    {
        if (!isGrounded)
        {
            animator.SetBool("Jumping", true);

            if (rig.velocity.y > 0)
            {
                animator.SetFloat("TimeJumping", 0);
            }
            else
            {
                animator.SetFloat("TimeJumping", 1);
            }
        }
        else
        {
            animator.SetBool("Jumping", false);
        }
    }

    private void JumpPlayer()
    {
        if (isGrounded)
        {
            rig.AddForce(new Vector2(0, jumpForce),ForceMode2D.Force);
        }    
    }

    private void MovePlayer()
    {
        float movimentoH = 0;

        if (SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 10)
        {
            if (Input.GetKey(KeyCode.A))
            {
                movimentoH = 1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movimentoH = -1;
            }
        }
        else
        {
            movimentoH = Input.GetAxis("Horizontal");
        }

        if (Input.GetKey(KeyCode.S) && (SceneManager.GetActiveScene().buildIndex == 8 || SceneManager.GetActiveScene().buildIndex == 10))
        {
            rig.AddForce(new Vector2(rig.velocity.x, -12f), ForceMode2D.Force);
        }

        rig.velocity = new Vector2(movimentoH * speed, rig.velocity.y);
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, isGround);
        
        if(movimentoH != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        
        if ((movimentoH > 0 && !lookingRight) || (movimentoH < 0 && lookingRight))
        {
            Flip();
        }
        
    }

    private void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void TakeDamage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("damage"))
        {
            TakeDamage();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("nextLevel"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 10)
            {
                rig.gravityScale = 1;
                lightPost.intensity = 0.51f;
                transform.Find("Point Light 2D").GetComponent<Light2D>().intensity = 0;
                GameObject.Find("Main Camera").GetComponent<Animator>().Play("CameraEnd");
                return;
            }
            GameManager.instance.WinLevel((OndeEstou.instance.fase + 1));
        }

        verifySolution(collision);
    }

    public void EndGame()
    {
        lightPost.intensity = 0f;
        Invoke("BackToStart", 1);
    }

    void BackToStart()
    {
        SceneManager.LoadScene("FirstScene");
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.5f);
    }

    public void SolutionDone()
    {
        lightPost.intensity = 1;
        lampPost.GetComponent<BoxCollider2D>().enabled = true;
    }

    void verifySolution(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("button"))
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 2:
                    SolutionDone();
                    break;
                case 3:
                    if(GameObject.Find("MusicBackground").GetComponent<AudioSource>().mute == true)
                    {
                        SolutionDone();
                    }
                    break;
                case 4:
                    contador++;
                    if(contador >= 3)
                    {
                        SolutionDone();
                        contador = 0;
                    }
                    break;
                case 5:
                    break;
                case 10:
                    contador++;
                    if (contador >= 3)
                    {
                        SolutionDone();
                        contador = 0;
                    }
                    break;
                default:
                    SolutionDone();
                    break;
            }
            
        }

        if (collision.gameObject.CompareTag("highGround"))
        {
            SolutionDone();
        }
        if (collision.gameObject.CompareTag("button"))
        {
            button.GetComponent<AudioSource>().Play();
        }
    }
}
