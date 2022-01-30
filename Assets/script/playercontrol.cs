using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontrol : MonoBehaviour
{
    [SerializeField] Image skilltimegauge1;
    [SerializeField] Image skilltimegauge2;
    [SerializeField] Image skillicon;
    public Rigidbody myRigidbody;
    public GameObject skillcounterText;
    public new GameObject camera;
    public float velocityY = 10f;
    public float x_sensi = 100f;
    public float y_sensi = 100f;
    public float mainSPEED = 0.2f ;
    public float inputVelocityX;
    public float inputVelocityY = 0;
    public float inputVelocityZ;
    public float skilltime = 450;
    public float skilltimecounter;
    public int skillrigidity = 25;
    public int skillrigiditycounter;
    public int fastskillrigiditycounter;
    public bool grounded;
    public bool collision;
    public Vector3 cameraAngle;
    void Start()
    {
        Application.targetFrameRate = 60;
        this.skillcounterText = GameObject.Find("skillcounter");
        skilltimecounter = skilltime;
    }

    // Update is called once per frame
    void Update()
    {
        playermove();
        
        playerskill();
    }
    void LateUpdate()
    {
        playercamera();
    }
    void playermove()       //プレイヤーの動き
    {
        Vector3 myscale;
        myscale = gameObject.transform.localScale;
        Transform trans = transform;
        transform.position = trans.position;

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
    }

    void playercamera()     //カメラの動き
    {
        if (skillrigiditycounter == 0)
        {
            float x_Rotation = Input.GetAxis("Mouse X");
            float y_Rotation = Input.GetAxis("Mouse Y");
            x_Rotation = x_Rotation * x_sensi;
            y_Rotation = y_Rotation * y_sensi;
            this.transform.Rotate(0, x_Rotation, 0);
            camera.transform.Rotate(-y_Rotation, 0, 0);
        }
        cameraAngle = camera.transform.localEulerAngles;
        if (cameraAngle.x < 280 && cameraAngle.x > 180)
        {
            cameraAngle.x = 280;
        }
        if (cameraAngle.x > 70 && cameraAngle.x < 180)
        {
            cameraAngle.x = 70;
        }
        cameraAngle.y = 0;
        cameraAngle.z = 0;
        camera.transform.localEulerAngles = cameraAngle;
    }
    void playerskill()      //スキルの動き
    {
        Transform trans = camera.transform;
        fastskillrigiditycounter = skillrigiditycounter;
        if (Input.GetButtonDown("skill1") && skilltimecounter >= skilltime / 3)
        {
            skillrigiditycounter = skillrigidity;
            skilltimecounter -= skilltime / 3;
        }
        if (skillrigiditycounter > 0)
        {
            skillrigiditycounter -= 1;
            this.myRigidbody.velocity = trans.TransformDirection(new Vector3(0,0,30));
            skillicon.color = new Color32(255, 127, 0, 150);
        }
        else
        {
            skillicon.color = new Color32(255, 255, 255, 150);
        }
        if (fastskillrigiditycounter - skillrigiditycounter == 1 && skillrigiditycounter < 1)
        {
            this.myRigidbody.velocity = new Vector3(0, 0, 0);
        }
        if (skilltimecounter < skilltime)
        {
            skilltimecounter += 1;
        }
        if (skilltimecounter >= skilltime)
        {
            this.skillcounterText.GetComponent<Text>().color = new Color32(0, 255, 0, 150);
            skilltimegauge1.color = new Color32(0, 255, 0, 150);
            skilltimegauge2.color = new Color32(255, 255, 0, 150);
            skilltimegauge1.fillAmount = 1;
        }
        else if (skilltimecounter >= (skilltime / 3 * 2))
        {
            this.skillcounterText.GetComponent<Text>().color = new Color32(0, 255, 0, 150);
            skilltimegauge1.color = new Color32(0, 255, 0, 150);
            skilltimegauge2.color = new Color32(255, 255, 0, 150);
            skilltimegauge1.fillAmount = (skilltimecounter - (skilltime / 3 * 2)) / (skilltime / 3);
        }
        else if (skilltimecounter >= skilltime / 3)
        {
            this.skillcounterText.GetComponent<Text>().color = new Color32(255, 255, 0, 150);
            skilltimegauge1.color = new Color32(255, 255, 0, 150);
            skilltimegauge2.color = new Color32(255, 0, 0, 150);
            skilltimegauge1.fillAmount = (skilltimecounter - skilltime / 3) / (skilltime / 3);
        }
        else if (skilltimecounter >= 0)
        {
            this.skillcounterText.GetComponent<Text>().color = new Color32(255, 0, 0, 150);
            skilltimegauge1.color = new Color32(255, 0, 0, 150);
            skilltimegauge2.color = new Color32(0, 0, 0, 150);
            skilltimegauge1.fillAmount = skilltimecounter / (skilltime / 3);
        }
        this.skillcounterText.GetComponent<Text>().text = "" + (int)skilltimecounter / (int)(skilltime / 3);
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
