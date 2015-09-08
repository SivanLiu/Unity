using UnityEngine;
using System.Collections;

public class JetController : MonoBehaviour {
    //Player的位置
    private GameObject target;
    public GameObject jetBomb;
    public  GameObject projectileExplosdion;
    public GameObject jetExplosion;
    public GameObject JetPos;
    float newPositionX;
    float velocity = 0.0f;
    float smoothTime = 1.0f;
    //飞机飞行速度
    public float m_speed = 1.5f;
    private float timer = 0;
    //记录飞机杯子弹击中的次数
    private int count = 0;
    private GameObject jetBombc;
    private GameObject obj;
	// Use this for initialization
	void Start () {
        //开始取消刚体重力属性
        this.gameObject.rigidbody.useGravity = false;
        target = GameObject.FindGameObjectWithTag("Player");

	}
    float PingPong(float aValue, float aMin, float aMax)
    {
        return Mathf.PingPong(aValue, aMax - aMin) + aMin;
    }
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //飞机往返移动
        transform.position = new Vector3(Mathf.PingPong(Time.time, 3f)+50f,transform.position.y, transform.position.z);
        newPositionX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity, smoothTime);
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        if (timer > 0.5f && Mathf.Abs(transform.position.x - target.transform.position.x) < 0.2f)
        {
            jetBombc = Instantiate(jetBomb, transform.position, transform.rotation) as GameObject;
            Destroy(jetBombc, 5);
            timer = 0;
        }
     
	}
  
    //public void SetTime(float time) {
    //    timer = time;
    //}
    void OnTriggerEnter(Collider other) {
        //子弹击中飞机
        if (other.gameObject.tag == "PlayerProjectile") {
            Instantiate(projectileExplosdion,other.transform.position,Quaternion.Euler(0,0,90));
            Destroy(other.gameObject);
            count++;
            if (count > 10) {
                this.gameObject.rigidbody.useGravity = true;
                DigitDisplay.score += 1000;
            }
           
        }
        //飞机与地面碰撞
        if (other.gameObject.tag == "Ground1") {
            obj=Instantiate(jetExplosion,transform.position,transform.rotation) as GameObject;
            Destroy(gameObject,1);
            Destroy(obj,1.5f);
        }

    }
}
