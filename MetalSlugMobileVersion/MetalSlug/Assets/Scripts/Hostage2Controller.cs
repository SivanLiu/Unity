using UnityEngine;
using System.Collections;
public enum HostageStates
{
    Idle,
    Released,
    Gift,
    Pang
}
public class Hostage2Controller : FSM
{
    //获取四个状态对象
    public GameObject Idle;
    public GameObject Released;
    public GameObject Pang;
    public GameObject Gift;
    public GameObject hostage2;
    public HostageStates status = HostageStates.Idle;
    public float m_speed = 0.8f;
    public GameObject Player;
    //人质是否和子弹碰撞
    private bool isColliderProjectile = false;
    //人质是否和主角碰撞
    private bool isColliderPlayer = false;
    //记录被子弹击中的次数
    private int count = 0;
    Transform m_transform;
    //    private bool  isColliding = false;
    // Use this for initialization
    void Start()
    {
        Idle.SetActive(true);
        m_transform = this.transform;
    }
    void Update()
    {
        if (isColliderProjectile == true)
        {
            //当子弹碰撞到人质时，人质进入released状态
            if (Pang != null )
            {
                Idle.SetActive(false);
                Pang.SetActive(true);
                Invoke("Gone", 0.4f);
                Released.SetActive(true);
            }
            isColliderProjectile = false;
        }
      
        isColliderPlayer = false;
        ////判断人质的状态是否为released,是的话则向主角跑去
        if (Released.activeSelf == true)
        {
            Invoke("ComeToPlayer", 0.1f);
            StartCoroutine(WaitForTime(0.1f));
            if (Gift != null) { 
                if(Gift.activeSelf==true)
                    Released.SetActive(false);
            }
             
        }
    }
    void OnTriggerEnter(Collider collidedObject)
    {

        //当人质碰到子弹，isColliderObject为真
        if (collidedObject.tag == "PlayerProjectile" )
        {
            isColliderProjectile = true;
            Destroy(collidedObject.gameObject);
        }
        //当人质碰到主角时,isColliderPlayer位真
        if (collidedObject.tag == "Player")
        {

            isColliderPlayer = true;
        }
        //print(Time.time +":"+ this.gameObject.name + ":" + collidedObject.name);
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
    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        if(Gift!=null)
            Gift.SetActive(true);
    }
}
