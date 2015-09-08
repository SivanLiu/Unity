using UnityEngine;
using System.Collections;

public class playerWin : MonoBehaviour {
    GameObject player;
    private GameObject jet;
    bool wingame = false;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        jet = GameObject.FindGameObjectWithTag("Jet");
    }
   
    void Update(){

        //foreach (Collider co in Physics.OverlapSphere(player.transform.position, 100))
        //{
        //    if (co.gameObject.tag == "Jet" || co.gameObject.tag == "Tank")
        //    {
        //        wingame = true;
        //    }
        //}
        if (player.transform.position.x >= 52.5f&& DigitDisplay.score >= 25000) {
            Application.LoadLevel("wingame4");
        }
       
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
 
   
    }
    
}
