using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    private bool isSuper = false;

    public Sprite[] spriteArr;//建立数组
    private SpriteRenderer spriteRender;

    private static int currFrame;//当前帧
    private int startFrame = 0;//初始帧
    private int endFrame = 4;//末尾帧

    // Start is called before the first frame update
    void Start()
    {
        //获得
        spriteRender = GetComponent<SpriteRenderer>();

        //初始化
        currFrame = startFrame;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //判断是不是和角色重合
        if (collision.gameObject.tag == "Player") 
        {
            GameControl.Instance.EatBeans(isSuper);
            Destroy(gameObject);
        }
    }

    public void MakeSuperBeans()
    {
        isSuper = true;
       
        //改变超级豆形状
        spriteRender.sprite = spriteArr[currFrame];
        currFrame++;

        //变大
        transform.localScale = new Vector3(3f, 3f, 1f);

        //是否到最后一帧
        if (currFrame >= endFrame)
        {
            currFrame = startFrame;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
