using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontrol : MonoBehaviour
{
    [SerializeField] Image skilltimegauge1;
    [SerializeField] Image skilltimegauge2;
    [SerializeField] Image skillicon;
    [SerializeField] Image HPgauge;
    [SerializeField] Image gameoverbackscreen;
    public Rigidbody myRigidbody;
    public GameObject skillcounterText;
    public GameObject playerHPText;
    public GameObject gameoverText;
    public new GameObject camera;
    public float velocityY = 10f;
    public float x_sensi = 1f;
    public float y_sensi = 1f;
    public float mainSPEED = 0.2f ;
    public float inputVelocityX;
    public float inputVelocityY = 0;
    public float inputVelocityZ;
    public float skilltime = 450;
    public float skilltimecounter;
    public float playerHP = 100;
    public float HPr;
    public float HPg;
    public byte gameoverbackscreencoler;
    public int skillrigidity = 25;
    public int skillrigiditycounter;
    public int fastskillrigiditycounter;
    public bool grounded;
    public bool collision;
    public bool gameover;
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
        if (gameover == false)
        {
            playermove();
            playerskill();
            playercamera();
        }
        HPmove();
    }
    void playermove()       //プレイヤーの動き
    {
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
            skilltimegauge1.color = new Color32(0, 255, 0, 200);
            skilltimegauge2.color = new Color32(255, 255, 0, 200);
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
    void HPmove()           //HPが減った時の処理
    {
        HPgauge.fillAmount = (playerHP / 100);
        if (playerHP >= 75)
        {
            HPg = 1;
            HPr = 4 - playerHP / 25;
        }
        else if (playerHP >= 25)
        {
            HPg = (playerHP-25) / 50;
            HPr = 1;
        }
        else
        {
            HPg = 0;
            HPr = 1;
        }
        if (playerHP < 0)
        {
            playerHP = -1;
            if (gameoverbackscreencoler < 255)
            {
                gameoverbackscreencoler += 1;
            }
            else
            {
                gameoverText.GetComponent<Text>().text = "GAMEOVER";
            }
            gameoverbackscreen.color = new Color32(0, 0, 0, gameoverbackscreencoler);
            camera.GetComponent<gunshoscript>().Gameover();
            gameover = true;
        }
        else
        {
            gameoverbackscreencoler = 0;
            gameover = false;
        }
        HPgauge.color = new Color(HPr, HPg, 0);
        playerHPText.GetComponent<Text>().color = new Color(HPr, HPg, 0);
    }
    public void irondamage(float Lv)
    {
        playerHP -= 10 + Lv * 5;
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
    void OnCollisionExit(Collision other)
    {
        grounded = false;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "shell")
        {
            playerHP -= 10;
        }
    }
}
