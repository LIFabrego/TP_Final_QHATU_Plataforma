using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitMovePlayer : MonoBehaviour
{
    private float desplazamientoHorizontal;
    //private float velocidad = 10f;
    private float rangoX = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -rangoX)
        {
            transform.position = new Vector3(-rangoX, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rangoX)
        {
            transform.position = new Vector3(rangoX, transform.position.y, transform.position.z);
        }
    }
}
