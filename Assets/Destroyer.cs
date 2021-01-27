/* This script is specifically for the spawn point called 'Destroyer'; anything that collides with it is destroyed, so that multiple rooms cannot spawn on top of each other */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider other) // This would be the function that destroys anything that collides with 'Destroyer'
    {
        Destroy(other.gameObject);
    }
}
