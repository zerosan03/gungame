using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironscript : MonoBehaviour
{
    public GameObject targetObject;
    public Rigidbody myRigidbody;
    public float velocityZ = 0;
    private void Start()
    {
        targetObject = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        Transform trans = transform;
        transform.position = trans.position;
        this.transform.LookAt(targetObject.transform);
        myRigidbody.velocity = trans.TransformDirection(new Vector3(0, 0, velocityZ));
        if (velocityZ < 5)
        {
            velocityZ += 1;
        }
        else
        {
            velocityZ = 5;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            velocityZ = -20;
        }
    }
}
