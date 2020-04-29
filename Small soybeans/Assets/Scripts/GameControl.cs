using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private const float WaitReload = 4f;//4秒后重加加载
    private const float SuperBeansTime = 5f;//生成超级豆间隔时间

    public static GameControl Instance;

    public UI UI;
    public bool isGameStrat;

    private void Awake()
    {
        Instance = this;

        //游戏未开始
        isGameStrat = false;
    }

    //吃到豆子
    public void EatBeans(bool isSuper)
    {
        UI.Eaten(isSuper);

        if (UI.RemineNum <= 0)
        {
            GameOver(true);
            return;
        }
        
        //如果吃掉的是超级豆
        if (isSuper)
        {
            //调用函数，继续生成超级豆
            StartCoroutine(BornSuperBeans());
            AddBuff();
        }
    }

    //增加BUFF
    private void AddBuff()
    {
        //获得所有敌人
        EnemyMove[] AllEnemy = GameObject.FindObjectsOfType<EnemyMove>();

        foreach (var item in AllEnemy)
        {
            item.DebuffAdded();
        }

        StartCoroutine(RemoveBuff());
    }

    //减去BUFF
    IEnumerator RemoveBuff()
    {
        //3秒后失去BUFF
        yield return new WaitForSeconds(3f);
        EnemyMove[] AllEnemy = GameObject.FindObjectsOfType<EnemyMove>();

        foreach (var item in AllEnemy)
        {
            item.DebuffRemoved();
        }
    }

    //游戏开始
    public void GameStrat()
    {
        isGameStrat = true;
        StartCoroutine(BornSuperBeans());
    }

    public void GameOver(bool isWin)
    {
        isGameStrat = false;
        UI.ShowGameOverPanel(isWin);

        //4秒后重新加载
        Invoke("ReloadScene", WaitReload);
    }

    void ReloadScene()
    {
        //重新加载场景，重新玩
        SceneManager.LoadScene(0);
    }

    //生成超级豆
    private IEnumerator BornSuperBeans()
    {
        //等待生成的时间
        yield return new WaitForSeconds(SuperBeansTime);
        Eat[] AllBeans = GameObject.FindObjectsOfType<Eat>();
        
        //豆子数目少于50则不再产生超级豆；
        if (AllBeans.Length < 50)
        {
            yield break;
        }

        //随机一个数
        Eat SuperBean = AllBeans[Random.Range(0, AllBeans.Length)];

        //生成超级豆
        SuperBean.MakeSuperBeans();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
