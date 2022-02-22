using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameclearscript : MonoBehaviour
{
    public float clearflag;
    public float start;
    public GameObject gamecleartext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clearflag != 0)
        {
            start = 1;
        }
        if (clearflag == 0 && start == 1)
        {
            gamecleartext.GetComponent<Text>().text = "GAMECLEAR";
        }
    }
    public void enemycounter(float counter)
    {
        clearflag += counter;
    }
}
