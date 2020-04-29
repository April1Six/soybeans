using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float xMargin = 1f;      // 在摄像机跟随之前，玩家可以移动的X轴距离。
    public float yMargin = 1f;      // 在摄像机跟随之前，玩家可以移动的Y轴距离。

    public float xSmooth = 8f;      // 相机在X轴上捕捉目标移动的平滑程度。
    public float ySmooth = 8f;      // 相机在Y轴上捕捉目标移动的平滑程度。

    public Vector2 maxXAndY;        // 相机可以拥有的最大X和Y坐标。
    public Vector2 minXAndY;        // 相机可以拥有的最小X和Y坐标。

    private Transform Player;       // 玩家位置。

    void Awake()
    {
        // 设置玩家位置。
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    bool CheckXMargin()
    {
        // 如果X轴中相机和播放机之间的距离大于X边缘，则返回“真”。
        return Mathf.Abs(transform.position.x - Player.position.x) > xMargin;
    }

    bool CheckYMargin()
    {
        // 如果Y轴中相机和播放机之间的距离大于Y边缘，则返回“真”。
        return Mathf.Abs(transform.position.y - Player.position.y) > yMargin;
    }

    void FixedUpdate()
    {
        TrackPlayer();
    }

    void TrackPlayer()
    {
        // 默认情况下，相机的目标X和Y坐标是当前的X和Y坐标。
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        // 如果玩家移动到了X边以外
        if (CheckXMargin())
        {
            // 目标X坐标应该是相机当前X位置和玩家当前X位置。
            targetX = Mathf.Lerp(transform.position.x, Player.position.x, xSmooth * Time.deltaTime);
        }

        // 如果玩家移动到了Y边以外
        if (CheckYMargin())
        {
            // 目标X坐标应该是相机当前Y位置和玩家当前Y位置。
            targetY = Mathf.Lerp(transform.position.y, Player.position.y, ySmooth * Time.deltaTime);
        }

        // 目标X和Y坐标不应大于最大值或小于最小值。
        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        // 使用相同的Z组件将相机位置设置为目标位置。(+15是把计分界面除开，使主角始终处于游戏界面中心，-5是为了让界面处于垂直中心)
        transform.position = new Vector3(targetX + 15, targetY - 5, transform.position.z);
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
