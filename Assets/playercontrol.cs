using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public ImagePosition myposition;
    public new GameObject camera;
    public float velocityX = 10f;
    public float velocityY = 10f;
    public float velocityZ = 10f;
    public float x_sensi = 100f;
    public float y_sensi = 100f;
    public float mainSPEED = 1 ;
    void Start()
    {
        this.myRigidbody = GetComponent<Rigidbody>();
        Transform myTransform = this.transform;
        Vector3 myposition = myTransform.position;
        myposition.x = 0f;
        myposition.y = 1f;
        myposition.z = 0f;
        myTransform.position = myposition;
    }

    // Update is called once per frame
    void Update()
    {
        playermove();
        playercamera();
    }
    void playermove()       //プレイヤーの動き
    {
        float inputVelocityX = 0;
        float inputVelocityY = 0;
        float inputVelocityZ = 0;
        Vector3 myscale;
        myscale = gameObject.transform.localScale;
        Transform myTransform = this.transform;
        Vector3 myposition = myTransform.position;
        Transform trans = transform;
        transform.position = trans.position;
        trans.position += trans.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * mainSPEED;
        trans.position += trans.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * mainSPEED;
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            myscale.y -= 0.3f;
            myposition.y -= 0.3f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            myscale.y += 0.3f;
            myposition.y += 0.3f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 2f)
        {
            inputVelocityY = this.velocityY;
        }
        else
        {
            inputVelocityY = this.myRigidbody.velocity.y;
        }
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, inputVelocityZ);
        gameObject.transform.localScale = myscale;
    }
    void playercamera()
    {
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");
        x_Rotation = x_Rotation * x_sensi;
        y_Rotation = y_Rotation * y_sensi;
        this.transform.Rotate(0, x_Rotation, 0);
        camera.transform.Rotate(-y_Rotation, 0, 0);
    }
}
