/* The following script will generate an appropriate room around the spawn point it is attached to */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need south door
    // 2 --> need north door
    // 3 --> need west door
    // 4 --> need east door

    private int rand; // the following variable will be used to obtain a random room from an array in 'RoomTemplates'
    private RoomTemplates templates; // this variable lets us reference the 'RoomTemplates' script and its contents
    private bool spawned = false; // when this variable is false, rooms can be spawned

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    // Update is called once per frame
    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                // need to spawn a room with a door on the south side
                rand = Random.Range(0, templates.southRooms.Length);
            Instantiate(templates.southRooms[rand], transform.position, Quaternion.identity); // 'Quaternion.identity' means that the room that is generated will have no rotation
            }

            else if (openingDirection == 2)
            {
                // need to spawn a room with a door on the north side
                rand = Random.Range(0, templates.northRooms.Length);
                Instantiate(templates.northRooms[rand], transform.position, Quaternion.identity);
            }

            else if (openingDirection == 3)
            {
                // need to spawn a room with a door on the west side
                rand = Random.Range(0, templates.westRooms.Length);
                Instantiate(templates.westRooms[rand], transform.position, Quaternion.identity);
            }

            else
            {
                //need to spawn a room with a door on the east side
                rand = Random.Range(0, templates.eastRooms.Length);
                Instantiate(templates.eastRooms[rand], transform.position, Quaternion.identity);
            }

            spawned = true; // because 'spawned' is now true, rooms will not continuously spawn 
        }
    }

    // The following method makes sure that a room cannot spawn on top of another; 'other' will most likely be referring to a spawn point that 'gameObject', which is another spawn point, 
    // has collided with
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<SpawnRoom>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity); // spawn walls blocking off any openings
                Destroy(gameObject);  // the spawn point represented by 'gameObject' is destroyed if there is already a spawn point at the given location and neither spawn
                                      // point has generated a room around it yet
            }
            spawned = true;
        }
    }
}
