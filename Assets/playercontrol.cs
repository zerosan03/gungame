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
        trans.position += trans.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * this.mainSPEED;
        trans.position += trans.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * this.mainSPEED;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            myscale.y -= 0.3f;
            this.mainSPEED -= 0.05f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            myscale.y += 0.3f;
            this.mainSPEED += 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            inputVelocityY = this.velocityY;
        }
        else
        {
            inputVelocityY = this.myRigidbody.velocity.y;
        }
        this.myRigidbody.velocity = new Vector3(0, inputVelocityY,0);
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
    }
    void OnCollisionExit()
    {
        grounded = false;
    }
}
