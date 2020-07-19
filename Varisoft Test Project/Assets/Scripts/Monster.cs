using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] int maxHealth = 30;
    [SerializeField] HealthBar healthBarPrefab;

    int health;
    HealthBar healthBar;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        CreateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateHealthBar()
    {
        healthBar = Instantiate(healthBarPrefab);
        healthBar.transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
        healthBar.transform.SetParent(transform);
    }


    private IEnumerator Damaged(int damage)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;

        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }

        float healthScale =  1f * health / maxHealth;
        healthBar.SetSize(healthScale);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Fireball fireball = collision.GetComponent<Fireball>();

        if(fireball != null)
        {
            fireball.Destroyed();
            StartCoroutine(Damaged(fireball.GetDamage()));
        }
    }
}
