using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    public Animator animator;
    public AudioSource hurt;


    int currentHealth; 
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator.SetBool("isDead", false);
    }

    public int TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        hurt.Play();

        if (currentHealth <= 0)
        {
            Die();
            Destroy(this.gameObject);
            return 1;
        }
        return 0;
    }

    void Die()
    {
        Debug.Log("Enemy Die");
        //Die animation
        animator.SetBool("isDead", true);
        //Disable Enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;        
    }

}
