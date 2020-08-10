using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float maxhealth = 100;
    private float currentHealth;

    public GameObject deathScreen;


    public Text txt; 

    public Animator anim;
    Vector3 movement;
    private float moveSpeed;

    public int count_death;

    private bool facingRight;
    private Rigidbody2D body;
    private float jumpForce = 7f;

    private bool onGround;

    public Transform groundCheck;
    public float groundRadiusCheck;
    public LayerMask groundItem;

    public Transform attackPoint;
    private float attackRange = 0.7f;
    public LayerMask enemyLayers;

    private CapsuleCollider2D capsule;
    private Vector2 standSize;
    private Vector2 crouchSize;

    private bool check = true;


    private int attackDamage;
    public HealthBar healthBar;


    public AudioSource attack;
    public AudioSource jump;
    public AudioSource hurt;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        count_death = 0;
        healthBar.SetMaxHealth((int) maxhealth);
        currentHealth = maxhealth;
        moveSpeed = 5f;
        attackDamage = 20;
        anim = GetComponent<Animator>();
        facingRight = true;
        body = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        standSize = capsule.size;
        crouchSize = new Vector2(capsule.size.x, capsule.size.y / 2f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
        }
        healthBar.SetHealth((int)currentHealth);
        float movement = Input.GetAxis("Horizontal");
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundRadiusCheck, groundItem);
        Movement(movement);
        Flip(movement);
        if (movement == 0 && currentHealth < maxhealth)
        {
            PlayerRegenerate(0.05f);
        }

        Jump();
        Crouch();
        Attack();
    }

    private void Movement(float horizontal)
    {
        body.velocity = new Vector2(horizontal * moveSpeed, body.velocity.y);

        anim.SetFloat("MovementSpeed", Mathf.Abs(horizontal));
    }

    private void Flip(float horizontal)
    /*
     * Function for the sprite to be flipped on
     */
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            /*
             * Condition to Flip
             * 
             * If the player presses the 'D' key while facing left, sprite will flip
             * Similarly, if the player presses 'A' key while facing right, sprite wwill flip
             */

            facingRight = !facingRight;

            Vector3 flip_sprite = transform.localScale;

            flip_sprite.x *= -1;
            transform.localScale = flip_sprite;
        }
    }


    private void Jump()
    /*
     * Function for the player to jump. This function also includes the double jump mechanism
     * 
     * Agrs:
     * None
     * Returns:
     * None
     */
    {

        if (onGround == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                body.velocity = Vector2.up * jumpForce;
                anim.SetBool("Jump", true);
                jump.Play();
            }
        }
        else { anim.SetBool("Jump", false); }

    }

    private void Crouch()

    {

        if (onGround == true)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                capsule.size = crouchSize;
                anim.SetBool("Crouch", true);
            }
            else
            {
                capsule.size = standSize;
                anim.SetBool("Crouch", false);
            }
        }

    }

    private void Attack()

    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Play attack animation
            anim.SetTrigger("Attack");
            attack.Play();

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {

               count_death += enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                txt.text = "Kill Count : " + count_death.ToString();
            }
        }


    }

    public void PlayerTakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            PlayerDie();
            Time.timeScale = 0f;
        }
        else
        {
            anim.SetTrigger("Hurt");
            hurt.Play();
            Debug.Log("Player is Hurt");
        }
    }

    void PlayerRegenerate(float regen)
    {
        currentHealth += regen;
        Debug.Log(currentHealth);
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        { return; }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void OnBecameInvisible()
    {
        Debug.Log("Out of Bounds");
        PlayerDie();
        Time.timeScale = 0f;
    }

    void PlayerDie()

    {
        currentHealth = 0;
        anim.SetTrigger("Dead");
        hurt.Play();
        Debug.Log("Player is Dead");
        this.enabled = false;
        
        deathScreen.SetActive(true);
        deathScreen.GetComponentInChildren<Text>().text = "Congratulations you have killed \n" + count_death.ToString() + " enemies";
        

    }
}
