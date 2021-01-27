/* The following script will be used to spawn each of the tiles that will make up a given room in the game. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    public GameObject [] gameObjects; // this will be the array that contains each of the tile sprites that can be spawned at almost any given point on the boarder of the room


    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, gameObjects.Length);  //the number that is held by 'rand' will be the index of the tile sprite in 'gameObjects' to be used
        Instantiate(gameObjects[rand], transform.position, Quaternion.identity); //a random tile sprite from 'gameObjects' is instantiated at the position of the GameObject this script is attached to
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
