using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private GameObject player;
    public GameObject Jet;
    private Enemy1State state;
    public float xPos = 320;
    public float yPos = 220;
    public Texture gameOver;
    float time = 0;
    bool isCome = false;
    public GameObject jetPos;
    public GameObject enemy2;
    public GameObject enemy1;
    public GameObject enemy3;
    public GameObject tank;
    private int num1 = 0;
    private int num2 = 0;
    private int num3 = 0;
    private int num4 = 0;
    private int num5 = 0;

    private bool isEnemy = false;
    public GameObject[] objects;
    public float spawnTime = 2.5f;
    //出现位置
    private Vector3 spawnPosition;
	void Start () {
       // InvokeRepeating("Spawn", spawnTime, spawnTime);
        player = GameObject.FindGameObjectWithTag("Player");
	}
	void Update () {
      
        Vector3 position = player.transform.position;

        foreach (Collider co in Physics.OverlapSphere(position, 3))
        {
            if (co.gameObject.tag == "Enemy1" || co.gameObject.tag == "Enemy2" || co.gameObject.tag == "Enemy3")
            {
                isEnemy = true;
            }
        }
        if (isEnemy == false)
        {
            EnemySpawn();
        }
	}
    void FixedUpdate()
    {
      
    }
    void EnemySpawn() {

        if (player.transform.position.x >= -0.5f&& num1 < 1 && player.transform.position.x <= 7 )
            {
                num1++;
                StartCoroutine(enemy(0, enemy1, new Vector3(1f, player.transform.position.y, 0), 1));
                StartCoroutine(enemy(0f, enemy1, new Vector3(2.5f, player.transform.position.y, 0), 1));
                //StartCoroutine(enemy(1.5f,enemy2,new Vector3(3.5f,0,0),1));
                //StartCoroutine(enemy(2.5f, enemy2, new Vector3(4.0f, 1f, 0), 1));
            }
        else if (num2 < 1 && player.transform.position.x > 7 && player.transform.position.x <=14.5)
        {
               num2++;
               StartCoroutine(enemy(0.5f, enemy2, new Vector3(3.0f, 0, 0), 1));
               StartCoroutine(enemy(1,enemy3,new Vector3(3.5f,0,0),2));
        }
        else if (num3 < 1 && player.transform.position.x >= 14.5f && player.transform.position.x <=18)
         {
                num3++;
                Instantiate(enemy3, player.transform.position + new Vector3(2, player.transform.position.y, 0), enemy3.transform.rotation);
                //StartCoroutine(enemy(1,enemy1,new Vector3(3.5f,0,0),1));
                //StartCoroutine(enemy(1, enemy1, new Vector3(4.5f, 0, 0), 1));
         }else if (num4<1&&player.transform.position.x >= 18) {
                num4++;
                StartCoroutine(enemy(1, enemy2, new Vector3(0.5f, player.transform.position.y + 0.8f, 0), 1));
                StartCoroutine(enemy(1, enemy2, new Vector3(1.5f, player.transform.position.y + 1.5f, 0), 1));
                //StartCoroutine(enemy(1, enemy3, new Vector3(2.3f, player.transform.position.y + 1.3f, 0), 1));
                //StartCoroutine(enemy(1, enemy2, new Vector3(3.5f, player.transform.position.y + 1.1f, 0), 1));
            }
        if (num5<1&&player.transform.position.x >= 4.2f) {
            num5++;
            Instantiate(tank,new Vector3(player.transform.position.x+5f,-1.34f,0),transform.rotation);
        }
      
    }
    //time秒之后生成对象num个obj对象
    IEnumerator enemy(float time,GameObject obj,Vector3 vector3,int num) {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < num; ++i) {
            Instantiate(obj, player.transform.position +vector3+new Vector3(i*1.5f,0,0), obj.transform.rotation);
        }
    }
    
    void Spawn() { 
         spawnPosition.x = Random.Range (0, 22);
         spawnPosition.y = 0.5f;
         spawnPosition.z =0;
         Instantiate(objects[Random.Range(0,objects.Length-1)], spawnPosition, Quaternion.identity);
    
    }
     
}

