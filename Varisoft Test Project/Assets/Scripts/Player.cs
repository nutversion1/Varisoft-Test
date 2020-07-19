using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] AudioClip attackSound;
    [SerializeField] float movementSpeed = 3f;

    IsometricCharacterRenderer isoRenderer;
    Rigidbody2D rb2D;
    Vector2 velocity = Vector2.zero;
    bool canAttack = true;
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
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void OnEnable()
    {
        AnalogStick.moveEvent += Move;
        AnalogStick.stopMoveEvent += StopMove;
    }

    void OnDisable()
    {
        AnalogStick.moveEvent -= Move;
        AnalogStick.stopMoveEvent += StopMove;
    }

    private IEnumerator FireballAttack()
    {
        
        canAttack = false;

        //Play attack sound
        AudioSource.PlayClipAtPoint(attackSound, Camera.main.transform.position, 0.1f);

        //Get player direction
        int direction = isoRenderer.GetLastDirection();

        //Create fireball
        Fireball fireball = Instantiate(fireballPrefab).GetComponent<Fireball>();
        fireball.Setup(direction, transform.position);

        //Delay
        yield return new WaitForSeconds(0.1f);

        canAttack = true; ;

        //Delay
        yield return new WaitForSeconds(3f);

        //Destroy fireball
        if (fireball != null)
        {
            Destroy(fireball.gameObject);
        }
        
    }

    public void HandleAttackButton()
    {
        if (canAttack)
        {
            StartCoroutine(FireballAttack());
        }
    }

    private void Move(int direction)
    {
        //Debug.Log("Move " + direction);

        float velocityX = directionVectors[direction].x;
        float velocityY = directionVectors[direction].y;

        velocity = new Vector2(velocityX, velocityY);
    }

    private void StopMove()
    {
        velocity = Vector2.zero;
    }

    private void HandleMovement()
    {
        Vector2 currentPos = rb2D.position;
        Vector2 inputVector = velocity;
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rb2D.MovePosition(newPos);
    }
}
