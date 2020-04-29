using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject PlayPanel;
    public GameObject StartTextPanel;

    public Image StartRender;
    public Sprite[] StartSpriteArr;

    public Text RemineText;
    public Text EatenText;
    public Text ScoreText;

    public int RemineNum;
    public int EatenNum;
    private int ScoreNum;

    public GameObject winPanel;
    public GameObject failPanel;


    //控制面板显示或隐藏
    public void CtrlBeanVisible(int idx)
    {
        //传1时显示该UI
        StartPanel.SetActive(idx == 1);

        //传2时显示该UI
        PlayPanel.SetActive(idx == 2);

        //传3时显示该UI
        StartTextPanel.SetActive(idx == 3);
    }

    //吃豆后数字的变化
    public void Eaten(bool isSuper)
    {
        RemineNum--;
        EatenNum++;

        //吃到的是超级豆
        if (isSuper)
        {
            ScoreNum += 110;
        }
        //吃到的不是超级豆
        else
        {
            ScoreNum += 10;
        }

        //更新
        UpdateUI();
    }

    //更新UI
    private void UpdateUI()
    {
        RemineText.text = RemineNum.ToString();
        EatenText.text = EatenNum.ToString();
        ScoreText.text = ScoreNum.ToString();
    }

    //点击开始后现实的UI
    public void OnStartClick()
    {
        CtrlBeanVisible(2);
        GameControl.Instance.GameStrat();
    }

    //播放开场动画
    IEnumerator PlayStartAnimation()
    {
        for (int i = 0; i < StartSpriteArr.Length; i++)
        {
            //切换图片
            StartRender.sprite = StartSpriteArr[i];

            //图片切换间隔时长
            yield return new WaitForSeconds(0.04f);
         }

         CtrlBeanVisible(3);
    }

    // Start is called before the first frame update
    void Start()
    {
        //刚开始时显示的UI（图片）
        CtrlBeanVisible(1);

        //启动播放
        StartCoroutine(PlayStartAnimation());

        //初始化
        RemineNum = GameObject.FindObjectsOfType<Eat>().Length;

        //初始化
        EatenNum = 0;

        //初始化
        ScoreNum = 0;

        //更新
        UpdateUI();
    }

    public void ShowGameOverPanel(bool isWin)
    {
        //成功过关
        winPanel.SetActive(isWin);

        //失败
        failPanel.SetActive(!isWin);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
