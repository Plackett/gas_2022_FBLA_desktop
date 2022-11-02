using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridsc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z <= 90){
            transform.position = new Vector3(transform.position.x,transform.position.y,110);
        }
        if(transform.position.z >= 110){
            transform.position = new Vector3(transform.position.x,transform.position.y,90);
        }
        Vector3 move = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        transform.Translate(move * Time.deltaTime, Space.World);
    }
}
