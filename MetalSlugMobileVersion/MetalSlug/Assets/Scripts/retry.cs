using UnityEngine;
using System.Collections;

public class retry : MonoBehaviour {
    public AudioClip wingamesound;
    GameObject mainCamera;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        AudioSource.PlayClipAtPoint(wingamesound, new Vector3(mainCamera.transform.position.x, 0, 0));

    }
	// Use this for initialization
    public void StartNextGame()
    {
        DigitDisplay.life = 20;
        DigitDisplay.grenade = 9;
        DigitDisplay.leftTime = 200;
        Application.LoadLevel("play2");

    }
	// Update is called once per frame
    public void exitGame()
    {

        Application.Quit();

    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        { Application.Quit(); }
    }
   
}
