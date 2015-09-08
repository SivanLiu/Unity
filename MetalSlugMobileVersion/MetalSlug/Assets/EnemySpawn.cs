using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    private int yOffset = 0;
    private int xOffset = 50;
    private GameObject player;
    public GameObject enemy2;
    public GameObject enemy1;
    public GameObject enemy3;
    private int num1 = 0;
    private int num2 = 0;
    private int num3 = 0;
    private int num4 = 0;
    private int num5 = 0;
    public float spawnTime = 2.5f;
    //出现位置
    private Vector3 spawnPosition;
    public GameObject[] objects;
    private PlayerController playerController;
    bool isEnemy=false;
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

	}
    private void OnEnable()
    {
       // InvokeRepeating("Spawnenemy",1,1);
    }
    private void OnDisable() {
       // this.CancelInvoke("Spawnenemy");
    }
  
	// Update is called once per frame
	void Update () {
        spawnTime += Time.deltaTime;
        if (DigitDisplay.score <= 20000) {
            if (spawnTime > 3)
            {
                EnmeyMake();
                spawnTime = 0;
            }
         }
       
        
       
    }
    void FixedUpdate() {
    }
    void Enemy_Spawn()
    {

        if (player.transform.position.x >= -0.5f && num1 < 1 && player.transform.position.x <= 7)
        {
            num1++;
            StartCoroutine(enemy(0, enemy1, new Vector3(1f, player.transform.position.y, 0), 1));
            StartCoroutine(enemy(0f, enemy1, new Vector3(2.5f, player.transform.position.y, 0), 1));
            StartCoroutine(enemy(1.5f, enemy2, new Vector3(3.5f, 0, 0), 1));
            StartCoroutine(enemy(2.5f, enemy2, new Vector3(4.0f, 1f, 0), 1));
        }
        else if (num2 < 1 && player.transform.position.x > 7 && player.transform.position.x <= 14.5)
        {
            num2++;
            StartCoroutine(enemy(0.5f, enemy2, new Vector3(3.0f, 0, 0), 1));
            StartCoroutine(enemy(1, enemy3, new Vector3(3.5f, 0, 0), 2));
        }
        else if (num3 < 1 && player.transform.position.x >= 14.5f && player.transform.position.x <= 18)
        {
            num3++;
            Instantiate(enemy3, player.transform.position + new Vector3(2, player.transform.position.y, 0), enemy3.transform.rotation);
            StartCoroutine(enemy(1, enemy1, new Vector3(3.5f, 0, 0), 1));
            StartCoroutine(enemy(1, enemy1, new Vector3(4.5f, 0, 0), 1));
        }
        else if (num4 < 1 && player.transform.position.x >= 18)
        {
            num4++;
            StartCoroutine(enemy(1, enemy2, new Vector3(0.5f, player.transform.position.y + 0.8f, 0), 1));
            StartCoroutine(enemy(1, enemy2, new Vector3(1.5f, player.transform.position.y + 1.5f, 0), 1));
            StartCoroutine(enemy(1, enemy3, new Vector3(2.3f, player.transform.position.y + 1.3f, 0), 1));
            StartCoroutine(enemy(1, enemy2, new Vector3(3.5f, player.transform.position.y + 1.1f, 0), 1));
        }
     
    }
    //time秒之后生成对象num个obj对象
    IEnumerator enemy(float time, GameObject obj, Vector3 vector3, int num)
    {
       
       
        spawnPosition.y = player.transform.position.y;
        spawnPosition.z = 0;
        yield return new WaitForSeconds(time);
        for (int i = 0; i < num; ++i)
        {
            Instantiate(obj, player.transform.position + vector3 + new Vector3(i * 1.5f, 0, 0), obj.transform.rotation);
        }
        GameObject enemy = Instantiate(objects[Random.Range(0, objects.Length - 1)], spawnPosition, Quaternion.identity) as GameObject;
        enemy.SetActive(true);
        Destroy(enemy, 4);
    }

    void Spawn()
    {
        if (player.transform.position.x > 11&&player.transform.position.x<45)
        {
            spawnPosition.x = Random.Range(player.transform.position.x - 7, player.transform.position.x + 7);
            spawnPosition.y = player.transform.position.y+0.5f;
            spawnPosition.z = 0;
            GameObject enemy = Instantiate(objects[Random.Range(0, objects.Length - 1)], spawnPosition, Quaternion.identity) as GameObject;
            Destroy(enemy,10f);
        }
    }
    private void EnmeyMake() {
        isEnemy = true;
        foreach (Collider co in Physics.OverlapSphere(player.transform.position, 5))
        {
            if (co.gameObject.tag == "Enemy1" || co.gameObject.tag == "Enemy2" ||
                co.gameObject.tag == "Enemy3" )
            {
                isEnemy = false;
            }
            if (isEnemy) {
                Spawn();
                break;
            }
        }
    }
    private void Spawnenemy() {
        float x = UnityEngine.Random.Range(xOffset,Screen.width-xOffset);
        Vector3 startPos = new Vector3(x + xOffset, Screen.height + yOffset, 2);
        Vector3 pos = Camera.main.ScreenToWorldPoint(startPos);
        GameObject.Instantiate(enemy1,pos,Quaternion.identity);
    }
}
