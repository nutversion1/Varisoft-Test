using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogStick : MonoBehaviour
{
    [SerializeField] int dragPadding = 30;
    [SerializeField] int stickMoveDistance = 30;

    RectTransform stick;
    Vector2 mouseStartPos;
    Vector2 mouseCurrentPos;

    public delegate void MoveEvent(int direction);
    public static MoveEvent moveEvent;

    public delegate void StopMoveEvent();
    public static StopMoveEvent stopMoveEvent;

    void Awake()
    {
        stick = transform.Find("Stick").GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartingDrag()
    {
        mouseStartPos = Input.mousePosition;
    }

    public void Dragging()
    {
        ////Get horizontalInput & verticalInput
        float horizontalInput = 0;
        float verticalInput = 0;
        mouseCurrentPos = Input.mousePosition;

        if(mouseCurrentPos.x < mouseStartPos.x - dragPadding)
        {
            horizontalInput = -1;
        }
        else if (mouseCurrentPos.x > mouseStartPos.x + dragPadding)
        {
            horizontalInput = 1;
        }

        if (mouseCurrentPos.y < mouseStartPos.y - dragPadding)
        {
            verticalInput = -1;
        }
        else if (mouseCurrentPos.y > mouseStartPos.y + dragPadding)
        {
            verticalInput = 1;
        }

        float xPos = horizontalInput * stickMoveDistance;
        float yPos = verticalInput * stickMoveDistance;
        stick.anchoredPosition = new Vector2(xPos, yPos);

        ////Set direction
        int direction = 0;

        //North
        if (horizontalInput == 0 && verticalInput == 1)
        {
            direction = 0; 
        }
        //North-West
        else if (horizontalInput == -1 && verticalInput == 1)
        {
            direction = 1;
        }
        //West
        else if (horizontalInput == -1 && verticalInput == 0)
        {
            direction = 2;
        }
        //South-West
        else if (horizontalInput == -1 && verticalInput == -1)
        {
            direction = 3;
        }
        //South
        else if (horizontalInput == 0 && verticalInput == -1)
        {
            direction = 4;
        }
        //South-East
        else if (horizontalInput == 1 && verticalInput == -1)
        {
            direction = 5;
        }
        //East
        else if (horizontalInput == 1 && verticalInput == 0)
        {
            direction = 6;
        }
        //North-East
        else if (horizontalInput == 1 && verticalInput == 1)
        {
            direction = 7;
        }

        //Generate stop move event
        if (horizontalInput == 0 && verticalInput == 0)
        {
            stopMoveEvent?.Invoke();
        }
        ////Generate move event
        else
        {
            moveEvent?.Invoke(direction);
        }
      
    }

    public void StoppedDragging()
    {
        stick.anchoredPosition = Vector2.zero;

        //Generate stop move event
        stopMoveEvent?.Invoke();
    }

  
}
