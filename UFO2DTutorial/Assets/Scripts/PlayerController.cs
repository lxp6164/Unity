using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //引入UI

public class PlayerController : MonoBehaviour {

    [Header("水平方向")]
    public float moveX;

    [Header("垂直方向")]
    public float moveY;

    [Header("推力")]
    public float push;

    public Vector3 fristPos;//接触时的position
    public Vector3 twoPos;//移动后的position
    
    //刚体，以便可以被施力
    Rigidbody2D rb2D;

    [Header("分数文字UI")]
    public Text countText;    //注: Text类型不是String字符串
    [Header("过关位子UI")]
    public Text winText;

    //分数
    int score;

	// Use this for initialization
	void Start () {

        rb2D = GetComponent<Rigidbody2D>();



        //设置分数初始值
        setScoreText();  //默认为0

        winText.text = "";  //一开始不“You Win！”不显示
	}




    /*
 * 
 *
   #if UNITY_EDITOR || UNITY_STANDALONE_WIN //PC
   //代码
   #elseif UNITY_IPHONE || UNITY_ANDROID//iPhone和Android端
   //代码
   #endif
 * */

    void FixedUpdate()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN //PC
        FixedUpdate_PC();
#endif

#if UNITY_IPHONE || UNITY_ANDROID//iPhone和Android端
        FixedUpdate_Android();
#endif
    }



    // Update is called once per frame
    void FixedUpdate_PC () {
        //水平移动 = 输入.取得轴向（"水平"）；
        moveX = Input.GetAxis("Horizontal");

        //垂直移动 = 输入.取得轴向（"垂直"）；
        moveY = Input.GetAxis("Vertical");

        //二维向量 移动方向 = 新 二维向量(水平移动，垂直移动);
        Vector2 movement = new Vector2(moveX,moveY);

        //rb2D.施加力量(推力 * 移动方向);
        rb2D.AddForce(push * movement);
    }




    void FixedUpdate_Android()
    {

        float speet = (1.0f) * push;        //移动速度

        float moveY = 0;// 上下移动的速度        
        float moveX = 0;//左右移动的速度
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //获取接触屏幕的坐标
            fristPos = Input.GetTouch(0).position;
        }
        //判断移动                
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            //获取在屏幕上移动后的坐标
            twoPos = Input.GetTouch(0).position;
            //判断向上移动,并且不出上方屏幕
            if (fristPos.y < twoPos.y && Camera.main.WorldToScreenPoint(transform.position).y < Screen.height)
            {
                moveY += speet * Time.deltaTime;
            }
            //判断向下移动,并且不出下边屏幕
            if (fristPos.y > twoPos.y && Camera.main.WorldToScreenPoint(transform.position).y > 0)
            {
                moveY -= speet * Time.deltaTime;
            }
            //判断向左移动,并且不出左边屏幕
            if (fristPos.x > twoPos.x && Camera.main.WorldToScreenPoint(transform.position).x > 0)
            {
                moveX -= speet * Time.deltaTime;
            }
            //判断向右移动,并且不出右边屏幕
            if (fristPos.x < twoPos.x && Camera.main.WorldToScreenPoint(transform.position).x < Screen.width)
            {
                moveX += speet * 2.0f * Time.deltaTime;
            }
            //改变物体坐标
            transform.Translate(moveX, moveY, 0);
        }
    }

            //当player进入到其他触发器的范围(碰撞)后会有什么反应
            void OnTriggerEnter2D(Collider2D other)
    {
                  //玩家物件名字    //其他物件名字
        Debug.Log(name + "触发了" + other.name);

        //如果其他物件的标签为PickUp
        if (other.CompareTag("PickUp")) {
                           //把该物件设为false，即黄金被吃掉
            other.gameObject.SetActive(false);

            //吃到了黄金，要更新分数
            score += 1;
            setScoreText();

        }else if (other.CompareTag("Exception") && score >= 12) {
            winText.text = "You can try, but never win!";
        }

    }

    void setScoreText() {
        countText.text = "目前分数: " + score.ToString();

        /*
        if(score >= 12){
            winText.text = "You Win!";
        }
        */

    }

}
