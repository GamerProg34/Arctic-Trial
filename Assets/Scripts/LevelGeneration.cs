/* The following is a script that will generate a random, connected path from the top end of the dungeon to the bottom end */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions; //this will be the array that contains all the possible starting positions of the critical path through the dungeon
    public GameObject[] rooms; // index 0 will have LR rooms, index 1 will have LRB openings, index 2 will ahve LRT openings, and index 3 LRBT will have LRTB openings

    private int direction; // this will be the variable that contains the value which is needed to know which direction the random room generation will occur in
    public float moveAmount; // this will be the variable used to calculate how much the random generation of rooms will occur in a given direction
    private float timeBtwRoom; // timeBtwRoom and startTimeBtwRoom are explained in the Update() function
    public float startTimeBtwRoom = 0.25f;
    public float minX; // this value will represent the limit to which the random room generation can occur to the left
    public float maxX; // this value will represent the limit to which the random room generation can occur to the right
    public float minY; // this value will represent the limit to which the random room generation can occur downwards
    public bool stopGeneration; // if this variable is true, then the critical path room generation stops and an exit is placed; if false, critical path room generation continues
    public LayerMask room;    // 'room' will be used to detect objects of the same layer as 'room'
    private int downCounter;  // this will be used to keep track of how many times in a row the random dungeon generator moves down

    // Start is called before the first frame update
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;  //the start of the critical path through the dungeon will be marked by a random position from startingPositions
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwRoom <= 0 && stopGeneration == false)  // if timeBtwRoom is less than or equal to zero, which it will be at the beginning of the first frame, the random generation will move in a random direction
                               // and timeBtwRoom will be set to the value of startTimeBtwRoom so that there is a slight wait between the generation of one room and the next
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else 
        {
            timeBtwRoom -= Time.deltaTime;     
        }
    }

    // This is the function in which the value of 'direction' will be used to determine the direction in which the random level generation will happen
    private void Move()
    {
        if (direction == 1 || direction == 2) // Move right
        {
            if (transform.position.x < maxX) // rooms can only be generated to the right if the room generator is not right beside the position indicated by maxX
            {
                downCounter = 0;  // downCounter is reset because the number of downward movements in a row has stopped
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);  // if the random generation is moving right, spawn a random room from all the types of rooms in 'rooms'
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);  // the random room generator now has a chance to move down
                if (direction == 3)   // if the direction to be moved is left, change it to right or down by changing the value of 'direction'
                {
                    direction = 2;   
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5; // if the room generator has reached a horizontal boundary of the level, make sure the next movement is downwards
            }
        }
        else if (direction == 3 || direction == 4) // Move left
        {
            if (transform.position.x > minX) // rooms can only be generated to the left if the room generator is not right beside the position indicated by minX
            {
                downCounter = 0;  
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);  // if the random generation is moving left, spawn a random room from all the types of rooms in 'rooms'
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);  // the random room generator cannot move reverse direction (we could not do something this simple previously because there are interruptions between the numbers 2 and 5,
                                                 // whereas moving from 3 to 6 is a continuous action
                
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5) // Move down; there is no specific reason why I have made it so that the chances of moving down are slightly lower than moving left or right
        {
            downCounter++; // 'downCounter' will be incremented every time the random generator moves down
            if (transform.position.y > minY)  // rooms can only be generated in the downward direction if the room generator is not below the position indicated by minY
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room); // this object will detect the room that the random generator is currently at
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3) // if the room in question does not have a bottom opening
                {
                    if (downCounter >= 2)    // if the random generator has moved down twice or more in a row, instantiate a room with openings in all directions instead of a room that will cause a dead end in the 
                                             // critical path 
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction(); // destroy the room
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction(); // destroy the room

                        int randBottomRoom = Random.Range(1, 4);   // after the room is destroyed,  obtain a random number that refers to the indices of non-bottom-opening rooms
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);  // instantiate a room with the obtained index value
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);  // the range here is different than for left and right movement because future rooms being spawned in the downward direction
                                                // need a top opening for the player to progress into that room
                Instantiate(rooms[rand], transform.position, Quaternion.identity);



                direction = Random.Range(1, 6);  // it doesn't matter what number is generated here, since there is no chance of any rooms being spawned upwards
            }
            else // if the level generation has reached the bottom boundary, stop the room generation and place the exit of the level in that given room 
            {
                stopGeneration = true;
            }
        }
    }
}
