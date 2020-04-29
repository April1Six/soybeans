using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathChoose : MonoBehaviour
{
    public static PathChoose Instance;
    private List<int> CanUsePath;//列表，表示能够用的路径

    void Awake()
    {
        Instance = this;
        CanUsePath = new List<int>();

        //把所有可用路径的索引存入列表里面
        for (int i = 0; i < transform.childCount; i++)
        {
            CanUsePath.Add(i);
        }
    }

    //随机获取一个路径
    public Vector3[] GetPath(out int childPathdx)
    {
        //随机选取一个列表的下标
        int randomIdx = Random.Range(0, CanUsePath.Count);

        //用索引把值取出来
        childPathdx = CanUsePath[randomIdx];

        //用一个路径删一个路径，避免重复
        CanUsePath.Remove(childPathdx);

        Transform child = transform.GetChild(childPathdx);
        Vector3[] result = new Vector3[child.childCount];
        
        //获取路径
        for (int i = 0; i < child.childCount; i++)
        {
            result[i] = child.GetChild(i).position;
        }

        return result;
    }

    //路径用完之后返回
    public void BackPath(int childPathdx)
    {
        CanUsePath.Add(childPathdx);
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
