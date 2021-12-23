using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public ImagePosition myposition;
    public new GameObject camera;
    public float velocityXf;
    public float velocityY = 10f;
    public float velocityZ;
    public float x_sensi = 100f;
    public float y_sensi = 100f;
    public float mainSPEED = 0.2f ;
    public float inputVelocityX;
    public float inputVelocityY = 0;
    public float inputVelocityZ;
    public bool grounded;
    public bool collision;
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        playermove();
        playercamera();
    }
    void playermove()       //プレイヤーの動き
    {
        Vector3 myscale;
        myscale = gameObject.transform.localScale;
        Transform trans = transform;
        transform.position = trans.position;
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            myscale.y -= 0.3f;
            this.mainSPEED -= 2f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            myscale.y += 0.3f;
            this.mainSPEED += 2f;
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            inputVelocityY = this.velocityY;
        }
        else
        {
            inputVelocityY = this.myRigidbody.velocity.y;
        }
        if (collision && grounded == false)
        {
            inputVelocityX = 0;
            inputVelocityZ = 0;
        }
        else
        {
            inputVelocityX = Input.GetAxis("Horizontal") * mainSPEED;
            inputVelocityZ = Input.GetAxis("Vertical") * mainSPEED;
        }
        this.myRigidbody.velocity = trans.TransformDirection(new Vector3(inputVelocityX, inputVelocityY, inputVelocityZ));
        gameObject.transform.localScale = myscale;
    }
    void playercamera()     //カメラの動き
    {
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");
        x_Rotation = x_Rotation * x_sensi;
        y_Rotation = y_Rotation * y_sensi;
        this.transform.Rotate(0, x_Rotation, 0);
        camera.transform.Rotate(-y_Rotation, 0, 0);
    }
    
    void OnCollisionStay(Collision other)
    {
        foreach (ContactPoint contact in other.contacts)
        {
            if (Vector2.Angle(contact.normal, Vector3.up) < 60)
            {
                grounded = true;
            }
        }
        if (other.collider.tag == "building")
        {
            collision = true;
        }
    }
    void OnCollisionExit(Collision other)
    {
        grounded = false;
        if (other.collider.tag == "building")
        {
            collision = false;
        }
    }
}
