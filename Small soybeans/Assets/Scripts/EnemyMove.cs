using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float MoveSpeed = 70;//移动速度
    public float AddSpeed = 0;

    private Vector3[] AllPoints;
    private int CurrPoint;

    private PlayPicture animator;

    private int ChildPathIdx;
    private SpriteRenderer model;

    private int BuffSpeed = 1;//控制吃到超级豆后敌人速度
    private Vector3 BornPosition;//记录出生点

    public UI UI;

    // Start is called before the first frame update
    void Start()
    {
        //记录出生点
        BornPosition = transform.localPosition;
        animator = GetComponent<PlayPicture>();
        model = GetComponent<SpriteRenderer>();
        InitOnePath();
    }

    //随机选择一条路径
    private void InitOnePath()
    {
        AllPoints = PathChoose.Instance.GetPath(out ChildPathIdx);
        AllPoints[0].x = transform.position.x;
        CurrPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.Instance.isGameStrat)
        {
            //吃豆越多，速度越快
            switch (UI.EatenNum/100)
            {
                case 0:
                    AddSpeed = 0;
                    break;

                case 1:
                    AddSpeed = 10;
                    break;

                case 2:
                    AddSpeed = 15;
                    break;

                default:
                    AddSpeed = 20;
                    break;
            }

            transform.position = Vector3.MoveTowards(transform.position, AllPoints[CurrPoint], 
                                                     Time.deltaTime * (MoveSpeed + AddSpeed) * BuffSpeed);
            
            //是否到达目标点
            if (transform.position == AllPoints[CurrPoint])
            {
                Vector3 pre = AllPoints[CurrPoint];
                CurrPoint++;
                
                //是否越界
                if (CurrPoint >= AllPoints.Length)
                {
                    PathChoose.Instance.BackPath(ChildPathIdx);
                    InitOnePath();
                }

                Vector3 next = AllPoints[CurrPoint];

                //通过始末位置判断方向
                CalculateDir(pre, next);
            }
        }
    }

    //增加辅助
    public void DebuffAdded()
    {
        Color color = model.color;

        //透明度
        color.a = 0.4f;
        model.color = color;
        BuffSpeed = 0;
    }

    //剪掉辅助
    public void DebuffRemoved()
    {
        Color color = model.color;

        //透明度
        color.a = 1f;
        model.color = color;
        BuffSpeed = 1;
    }

    //计算距离
    private void CalculateDir(Vector3 pre, Vector3 next)
    {
        float x = next.x - pre.x;
        float y = next.y - pre.y;

        //y方向移动
        if (Mathf.Abs(y) > Mathf.Abs(x))
        {
            animator.ChangeDir(y > 0 ? PlayPicture.AnimDir.Up : PlayPicture.AnimDir.Down);
        }

        //x方向移动
        else
        {
            animator.ChangeDir(x > 0 ? PlayPicture.AnimDir.Right : PlayPicture.AnimDir.Left);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //主角与敌人碰到
        if (collision.gameObject.tag == "Player")
        {
            //没有吃到超级豆
            if (BuffSpeed == 1)
            {
                GameControl.Instance.GameOver(false);
            }

            //吃到超级豆，返回出生点
            else
            {
                //返回出生点
                transform.localPosition = BornPosition;
                PathChoose.Instance.BackPath(ChildPathIdx);
                InitOnePath();
            }
        }
    }
}
