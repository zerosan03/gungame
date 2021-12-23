using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunshoscript : MonoBehaviour
{
    //Imageクラス変数を宣言。SerializeFieldでInspector上から定義
    [SerializeField] Image sight;
    [SerializeField] Image hitaction;
    //rayの長さ
    public float rayLength = 20000;
    //rayが当たったオブジェクトの情報を取得する為の変数
    RaycastHit hit;
    //敵に与えるダメージの値
    public int damageScore = 1;
    public int gunbullet = 6;
    public int fastgunbullet;
    public GameObject gunbulletText;
    public int hittime = 0;
    public int reloadtime = 100;
    public int reloadtimecounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.gunbulletText = GameObject.Find("gunbullet");
        fastgunbullet = gunbullet;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gunbullet > 0)
        {
            gunbullet -= 1;
        }
        //カメラの原点からレイを飛ばすとプレイヤー自身のタグを取得してしまう為、Z方向にズラす
        Ray ray = new Ray(transform.position + transform.rotation * new Vector3(0, 0, 0.01f), transform.forward);
        //rayの可視化
        //ray上にコライダーが存在する場合、RayCastHit型変数に情報を格納する。out修飾子を付けて、戻り値を取得せずに関数内で変数の値を操作する
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            //hitに取得した情報の内、タグ名を取得
            string hitTagName = hit.transform.gameObject.tag;
            //タグ名がEnemyだった場合
            if (hitTagName == "Enemy")
            {
                //Enemyタグのオブジェクトにrayが当たった時の照準の色
                sight.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                //マウスの左ボタンがクリックされたら
                if (Input.GetMouseButtonDown(0) && gunbullet > 0)
                {
                    //Enemyオブジェクトに付いているEnemyHPのReceveDamage関数を呼び出す
                    hit.collider.GetComponent<EnemyHP>().ReceveDamage(damageScore);
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
        if (Input.GetButtonDown("Reload"))
        {
            reloadtimecounter = 1;
        }
        if (reloadtime == -1)
        {
            reloadtime = 100;
            reloadtimecounter = 0;
            gunbullet = fastgunbullet;
        }
        reloadtime -= reloadtimecounter;
        this.gunbulletText.GetComponent<Text>().text = fastgunbullet + "/" + gunbullet;
    }
}
