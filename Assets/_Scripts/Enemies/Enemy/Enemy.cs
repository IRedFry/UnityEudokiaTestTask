using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    private EnemyBase enemyBase;

    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioSource audioSource;


    public void SetEnemyBase(EnemyBase eBase)
    {
        enemyBase = eBase;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CanonBall")
        {
            ReceiveDamage(collision.gameObject.GetComponent<Canonball>().GetDamage());
        }
    }

    private void ReceiveDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {  
            Events.EnemyDeath?.Invoke(enemyBase);
            audioSource.PlayOneShot(deathSound, 1.0f);
            StartCoroutine(Disable());
        }
        else
            audioSource.PlayOneShot(hurtSound, 1.0f);
    }

    public void Restart()
    {
        GetComponent<BoxCollider>().enabled = true;
        health = enemyBase.maxHealth;
    }
    private IEnumerator Disable()
    {
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(deathSound.length);
        gameObject.SetActive(false);
    }    
}
