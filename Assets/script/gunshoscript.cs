using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunshoscript : MonoBehaviour
{
    //Imageクラス変数を宣言。SerializeFieldでInspector上から定義
    [SerializeField] Image sight;
    [SerializeField] Image hitaction;
    [SerializeField] Image reloading;
    //rayの長さ
    public float rayLength = 20000;
    //rayが当たったオブジェクトの情報を取得する為の変数
    RaycastHit hit;
    //敵に与えるダメージの値
    public int damageScore = 1;
    public int gunbullet = 30;
    public int fastgunbullet;
    public GameObject gunbulletText;
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;
    public int hittime = 0;
    public float fastreloadtime;
    public float reloadtime = 100;
    public int reloadtimecounter = 0;
    public int shotcounter = 0;
    public bool gemestop;
    public AudioClip gunshotsound;
    public AudioClip reloadsound;
    AudioSource gunshot;
    AudioSource reload;
    // Start is called before the first frame update
    void Start()
    {
        this.gunbulletText = GameObject.Find("gunbullet");
        gunshot = gameObject.GetComponent<AudioSource>();
        reload = gameObject.GetComponent<AudioSource>();
        fastgunbullet = gunbullet;
        fastreloadtime = reloadtime;
    }

    // Update is called once per frame
    void Update()
    {
        if (gemestop == false)
        {
            //カメラの原点からレイを飛ばすとプレイヤー自身のタグを取得してしまう為、Z方向にズラす
            Ray ray = new Ray(transform.position + transform.rotation * new Vector3(0, 0, 0.01f), transform.forward);
            //rayの可視化
            //ray上にコライダーが存在する場合、RayCastHit型変数に情報を格納する。out修飾子を付けて、戻り値を取得せずに関数内で変数の値を操作する
            if (Physics.Raycast(ray, out hit, rayLength))
            {
                //hitに取得した情報の内、タグ名を取得
                string hitTagName = hit.transform.gameObject.tag;
                //タグ名がEnemyだった場合
                if (hitTagName == "turret")
                {
                    //Enemyタグのオブジェクトにrayが当たった時の照準の色
                    sight.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                    //マウスの左ボタンがクリックされたら
                    if (Input.GetMouseButton(0) && gunbullet > 0 && shotcounter <= 0)
                    {
                        //Enemyオブジェクトに付いているEnemyHPのReceveDamage関数を呼び出す
                        hit.collider.GetComponent<turretEnemyHP>().ReceveDamage(damageScore);
                        hittime = 5;
                    }
                }
                else if (hitTagName == "iron")
                {
                    //Enemyタグのオブジェクトにrayが当たった時の照準の色
                    sight.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                    //マウスの左ボタンがクリックされたら
                    if (Input.GetMouseButton(0) && gunbullet > 0 && shotcounter <= 0)
                    {
                        //Enemyオブジェクトに付いているEnemyHPのReceveDamage関数を呼び出す
                        hit.collider.GetComponent<ironscript>().ReceveDamage(damageScore);
                        hittime = 5;
                    }
                }
                else if (hitTagName == "summon")
                {
                    //Enemyタグのオブジェクトにrayが当たった時の照準の色
                    sight.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                    //マウスの左ボタンがクリックされたら
                    if (Input.GetMouseButton(0) && gunbullet > 0 && shotcounter <= 0)
                    {
                        //Enemyオブジェクトに付いているEnemyHPのReceveDamage関数を呼び出す
                        hit.collider.GetComponent<summonEnemyHP>().ReceveDamage(damageScore);
                        hittime = 5;
                    }
                }
                else
                {
                    //Enemyタグ以外のオブジェクトにrayが当たった時の照準の色
                    sight.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
                }
            }
            else
            {
                //rayがどのオブジェクトにも当たっていない時(空を向いている時)の照準の色
                sight.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
            }
            if (Input.GetMouseButton(0) && gunbullet > 0 && shotcounter <= 0)
            {
                gunbullet -= 1;
                shotcounter = 5;
                gunshot.PlayOneShot(gunshotsound, 0.3f);
                var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);
            }
            else
            {
                shotcounter -= 1;
            }
            if (hittime >= 0)
            {
                hitaction.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                hittime -= 1;
            }
            else
            {
                hitaction.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
            }
            //reload
            if ((Input.GetButtonDown("Reload") || gunbullet == 0) && reloadtimecounter == 0)
            {
                reloadtimecounter = 1;
                reload.PlayOneShot(reloadsound, 0.3f);
            }
            if (reloadtime < 0)
            {
                reloadtime = 100;
                reloadtimecounter = 0;
                gunbullet = fastgunbullet;
            }
            if (reloadtime < 99)
            {
                reloading.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                reloading.color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
            }
            reloading.fillAmount = reloadtime / fastreloadtime;
            reloadtime -= reloadtimecounter;
            this.gunbulletText.GetComponent<Text>().text = gunbullet + "/" + fastgunbullet;
        }
    }
    public void Gameover()
    {
        gemestop = true;
    }
}
