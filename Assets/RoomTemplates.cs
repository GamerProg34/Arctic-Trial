/* This script will contain the various rooms that will be spawned around the spawn points */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    // the following arrays will contain the rooms that will be spawned around the spawn points
    public GameObject [] southRooms;
    public GameObject [] northRooms;
    public GameObject [] eastRooms;
    public GameObject [] westRooms;

    public GameObject closedRoom; // this GameObject is a closed room; if it is invoked, there is no need for anything other than a closed room, so there is no need to make 'closedRoom'
                                  // an array

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
