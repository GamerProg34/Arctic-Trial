/* After the critical path of the dungeon has been generated, this script will be used to fill in the rest of the level(s) */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotMainSpawn : MonoBehaviour
{
    public LayerMask whatIsRoom;    // this will represent the layer in which rooms will be searched for
    public LevelGeneration levelGen;    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);    // this will be used to detect if there is a room at a given position 
                                                                                                  // and layer, as well as within a radius of 1 unit

        if (roomDetection == null && levelGen.stopGeneration == true)   // if there is no room at the given space and the critical path has been formed
        {
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);     // generate a random room at the given position
        }
    }
}
