using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    public Vector3 targetPos;   //目标位置  
    public Vector3 endTargetPos;
    public float smoothing = 0.5f;   //移动速度
    public Wheel[] WheelArrays;
    private bool isReaching = false;    // 表示是否达到目标位置
    public Damper damper;

    Animator manitor;
    GameObject player;
    public AudioClip carStartSound;
    public AudioClip carStopSound;

    float myTime = 0;
    void Start() {
       player = GameObject.Find("Player");
      
       manitor = player.GetComponent<Animator>();
       AudioSource.PlayClipAtPoint(carStartSound, new Vector3(Camera.main.transform.position.x, 0, 0));
       manitor.enabled = false;
       Invoke("PlaySound",0.3f);
    }

	void Update () {
        myTime+=Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position,targetPos,smoothing*Time.deltaTime);
        if (isReaching == false) {
            if (Vector3.Distance(transform.position, targetPos) < 0.3f) {
                isReaching = true;
                OnReach();
            }
           
        }
        if (myTime > 0.5f) {
            manitor.enabled = true;
//            print("0.5:" + manitor.enabled);
        }
        if (myTime > 2.0f) {
           manitor.enabled = false;

           player.transform.FindChild("PlayerGround").gameObject.transform.localScale = new Vector3(-1, 1, 1);
//            print("2.1:" + manitor.enabled);
        }

       
	}
    void PlaySound() {
        AudioSource.PlayClipAtPoint(carStopSound, new Vector3(Camera.main.transform.position.x, 0, 0));
    }
    void OnReach() {
        foreach (Wheel w in WheelArrays) {
            w.Stop();
        }
        damper.startRotate();   //挡板开始旋转
        //放下主角
        //把车子开走
        Invoke("DamperRotation", 0.8f);
    }
    //挡板回旋
    void DamperRotation()
    {

        damper.is2Rotate = true;
        Invoke("GoOut",0.8f);
        
    }
    void GoOut()
    {
        //改变车子移出的速度
        smoothing = 0.4f;
        //设置车子最后消失的位置
        targetPos = endTargetPos;
        foreach (Wheel w in WheelArrays)
        {
            w.isRotate = true;

        }
        Destroy(this.gameObject,1f);
    }
}
