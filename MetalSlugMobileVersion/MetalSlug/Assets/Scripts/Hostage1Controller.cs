using UnityEngine;
using System.Collections;

public class Hostage1Controller : MonoBehaviour {

    public GameObject Idle;
    public GameObject Released;
    public GameObject Pang;
    public GameObject Gift;
    public float m_speed = 0.1f;

    //记录子弹击中人质的次数
    private int count = 0;
    //人质是否和子弹碰撞
    private bool isColliderProjectile = false;
    //人质是否和主角碰撞
    private bool isColliderPlayer = false;
    // Use this for initialization
    void Start()
    {
        Idle.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    { if (Released!=null) {
            if (Released.activeSelf == true)
            {
                transform.Translate(Vector3.right * Time.deltaTime * 1.9f * m_speed);
                StartCoroutine(ComeToPlayer());
            }
            }
    }
    void OnTriggerEnter(Collider collidedObject)
    {
        //当人质碰到子弹，isColliderObject为真
        if (collidedObject.gameObject.tag == "PlayerProjectile" )
        {
            count++;
            isColliderProjectile = true;
            //Idle.SetActive(false);
            Destroy(Idle);
            if( Released!=null)
                Released.SetActive(true);
            Destroy(collidedObject.gameObject);
        }
    }
    //人质奔向主角
    IEnumerator  ComeToPlayer()
    {
    yield return new WaitForSeconds(1.3f);
        Destroy(Released);
        GetComponent<Rigidbody>().isKinematic = false;
        if (Gift != null)
        {
            Gift.SetActive(true);
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
           // this.GetComponent<Rigidbody>().useGravity = false;
            Destroy(this.gameObject, 3f);
        }

       
    }
    //pang消失
    public void Gone()
    {
        Destroy(Pang);
    }
}
