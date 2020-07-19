using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] int damage = 10;
    [SerializeField] AudioClip destroyedSound;
    [SerializeField] ParticleSystem burstFXPrefab;

    public int direction;
    Rigidbody2D rb2D;
    Vector2[] directionVectors = { new Vector2(0, 1), //North
                                   new Vector2(-0.7f, 0.7f), //North-West
                                   new Vector2(-1, 0), //West
                                   new Vector2(-0.7f, -0.7f), //South-West
                                   new Vector2(0, -1), //South
                                   new Vector2(0.7f, -0.7f), //South-East
                                   new Vector2(1, 0), //East
                                   new Vector2(0.7f, 0.7f) //North-East
                                  };

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(int direction, Vector2 playerPos)
    {
        this.direction = direction;

        float offsetX = directionVectors[direction].x / 2f;
        float offsetY = directionVectors[direction].y / 2f;
        transform.position = new Vector2(playerPos.x + offsetX, playerPos.y + offsetY);

        Vector2 v = directionVectors[direction] * moveSpeed;

        rb2D.velocity = v;
    }

    public void Destroyed()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(destroyedSound, Camera.main.transform.position, 0.5f);
        CreateBurstFX();
    }

    private void CreateBurstFX()
    {
        ParticleSystem burstFX = Instantiate(burstFXPrefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        Destroy(burstFX.gameObject, 2f);
    }

    public int GetDamage()
    {
        return damage;
    }
    
}
