using UnityEngine;
using System.Collections;

public class Hostage2Move : MonoBehaviour {
    //获取四个状态对象
    public GameObject Idle;
    public GameObject Released;
    public GameObject Pang;
    public GameObject Gift;
    public GameObject hostage2;
    public HostageStates status = HostageStates.Idle;
    public float m_speed = 0.1f;
    public GameObject Player;
    //人质是否和子弹碰撞
    private bool isColliderProjectile = false;
    //人质是否和主角碰撞
    private bool isColliderPlayer = false;
    //记录被子弹击中的次数
    private int count = 0;
//    private bool  isColliding = false;
	// Use this for initialization
	void Start () {
        Idle.SetActive(true);
	}
    
	// Update is called once per frame
	void Update () {
        if (isColliderProjectile == true)
        {
            //当子弹第一次碰撞到人质时，人质进入released状态
                if (Pang != null&&count==1)
                {
                    Idle.SetActive(false);
                    Pang.SetActive(true);
                    Invoke("Gone", 0.4f);
                    Released.SetActive(true);
                }
        }
        isColliderProjectile = false;
        ////判断人质的状态是否为released,是的话则向主角跑去
        if (Released.activeSelf == true)
        {
            Invoke("ComeToPlayer", 0.1f);
            //如果子弹第二次碰到人质，人质变为gift
            if (count > 2) {
                Released.SetActive(false);
                Gift.SetActive(true);
            }
            isColliderProjectile = false;
        }
        //当主角碰到gif时，血量加1，gif消失
        if (isColliderPlayer == true)
        {
            Destroy(this.gameObject);
        }
        isColliderPlayer = false;
	}
    void OnTriggerEnter(Collider collidedObject)
    {
      
        //当人质碰到子弹，isColliderObject为真
        if (collidedObject.tag == "PlayerProjectile" && collidedObject.name == "projectile(Clone)")
        {
            count++;
            isColliderProjectile = true;
        }
        //当人质碰到主角时,isColliderPlayer位真
        if (collidedObject.tag == "Player")
        {
            isColliderPlayer = true;
        }
    }
    void OnTriggerExit(Collider colliderObject) {

        Debug.Log("exit");
    }
    //人质奔向主角
    public void ComeToPlayer()
    {
        transform.Translate(Vector3.right * Time.deltaTime * -m_speed);
    }
    //pang消失
    public void Gone()
    {
        Destroy(Pang);
    }
}
