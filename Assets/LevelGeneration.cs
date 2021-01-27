/* The following is a script that will generate a random, connected path from one end of the dungeon to the opposite end (may need to change this comment later) */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions; //this will be the array that contains all the possible starting positions of the critical path through the dungeon
    // Start is called before the first frame update
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;  //the start of the critical path through the dungeon will be marked by a random position from startingPositions
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
