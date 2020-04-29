using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPicture : MonoBehaviour
{
    public enum AnimDir//封装方向
    {
        None = -1,//方向默认值
        Right = 0,
        Down = 1,
        Left = 2,
        Up = 3 
    }

    public float AnimSpeed = 0.15f;//播放速度

    public Sprite[] spriteArr;//建立数组
    private SpriteRenderer spriteRender;//渲染距离

    private int currFrame = 0;//记录当前播放的图片的下标
    private int startFrame;//记录从第几帧开始播放
    private int endFrame;//记录结束帧
    public int totalFrame = 4;//一个动画有几帧

    private AnimDir currDir = AnimDir.None;//记录当前方向

    // Start is called before the first frame update
    void Start()
    {
        //获得
        spriteRender = GetComponent<SpriteRenderer>();

        //默认方向为右
        ChangeDir(AnimDir.Right);

        //启动
        StartCoroutine(PlayerAnim());
    }

    IEnumerator PlayerAnim()
    {
        //死循环。一直切换动画
        while (true)
        {
            //切换动画的时间间隔
            yield return new WaitForSeconds(AnimSpeed);

            //切换图片
            spriteRender.sprite = spriteArr[currFrame];
            currFrame++;
            
            //判断播放是否结束
            if (currFrame >= endFrame)
            {
                currFrame = startFrame;
            }       
        }
    }

    //方向改变时对应播放的开始帧与结束帧也改变
    public void ChangeDir(AnimDir dir)
    {
        //当前方向和记录方向一致时
        if (currDir == dir)
        {
            return;
        }

        //不一致时
        else
        {
            currDir = dir;
            startFrame = currFrame = (int)dir * totalFrame;
            endFrame = startFrame + totalFrame;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
