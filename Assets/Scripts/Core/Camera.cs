using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float panSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") == 1 && transform.position.x < maxX)
        {
            transform.Translate(Vector3.right * panSpeed);
        } else if (Input.GetAxis("Horizontal") == -1 && transform.position.x > minX)
        {
            transform.Translate(Vector3.left * panSpeed);
        }
    }
}
