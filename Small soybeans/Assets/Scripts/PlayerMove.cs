using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 3;

    private float PlayerPauseSpeed;
    private float EnemyPauseSpeed;

    private Rigidbody2D Player;
    private PlayPicture animator;

    public EnemyMove enemy1;
    public EnemyMove enemy2;
    public EnemyMove enemy3;
    public EnemyMove enemy4;

    private bool isGamePause;

    // Start is called before the first frame update
    void Start()
    {
        //初始化
        Player = GetComponent<Rigidbody2D>();
        animator = GetComponent<PlayPicture>();

        //保留原始速度
        PlayerPauseSpeed = MoveSpeed;
        EnemyPauseSpeed = enemy1.MoveSpeed + enemy1.AddSpeed;

        isGamePause = false;
    }

    // Update is called once per frame
    void Update()
    {
        //控制主角移动以及暂停
        if (GameControl.Instance.isGameStrat)
        {
            //右
            if (Input.GetKey(KeyCode.D))
            {
                Vector2 dest = Player.position + Vector2.right * MoveSpeed;
                Player.MovePosition(dest);
                animator.ChangeDir(PlayPicture.AnimDir.Right);
            }

            //下
            else if (Input.GetKey(KeyCode.S))
            {
                Vector2 dest = Player.position + Vector2.down * MoveSpeed;
                Player.MovePosition(dest);
                animator.ChangeDir(PlayPicture.AnimDir.Down);
            }

            //左
            else if (Input.GetKey(KeyCode.A))
            {
                Vector2 dest = Player.position + Vector2.left * MoveSpeed;
                Player.MovePosition(dest);
                animator.ChangeDir(PlayPicture.AnimDir.Left);
            }

            //上
            else if (Input.GetKey(KeyCode.W))
            {
                Vector2 dest = Player.position + Vector2.up * MoveSpeed;
                Player.MovePosition(dest);
                animator.ChangeDir(PlayPicture.AnimDir.Up);
            }

            //暂停
            else if (Input.GetKey(KeyCode.P))
            {
                //按下P之前是暂停状态
                if (isGamePause)
                {
                    isGamePause = false;

                    //恢复为原始速度
                    MoveSpeed = PlayerPauseSpeed;
                    enemy1.MoveSpeed = EnemyPauseSpeed + enemy1.AddSpeed;
                    enemy2.MoveSpeed = EnemyPauseSpeed + enemy2.AddSpeed;
                    enemy3.MoveSpeed = EnemyPauseSpeed + enemy3.AddSpeed;
                    enemy4.MoveSpeed = EnemyPauseSpeed + enemy4.AddSpeed;
                }

                //按下P之前是非暂停状态
                else
                {
                    isGamePause = true;

                    //暂停时速度为0
                    MoveSpeed = 0;
                    enemy1.MoveSpeed = 0;
                    enemy2.MoveSpeed = 0;
                    enemy3.MoveSpeed = 0;
                    enemy4.MoveSpeed = 0;
                }
            }
        }
    }
}
