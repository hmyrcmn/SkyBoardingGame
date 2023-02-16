using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMartı : MonoBehaviour
{
  public Transform target;
    public float speed = 3f;
    public float attackThreshold = 5f;

    private Rigidbody rb;
    private float elapsedTime = 0f;
    public Animator MartiAnim;
    private int  lives;

     // Player objesine eriş
        GameObject playerObj = GameObject.Find("Player");

// PlayerSC component'ine eriş
        //PlayerSC playerScript = playerObj.GetComponent<PlayerSC>();


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MartiAnim =GetComponent<Animator>();

       
        

    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > attackThreshold)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
             MartiAnim.Play("MartiAnim");

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Simit")
        {
            elapsedTime = 0f;
           print("kazannnn");
             
            
            // GainPoint fonksiyonunu çağır
          //  playerScript.GainPoint();
    
        }else if(other.gameObject.tag == "Martı"){
             //lives =playerScript.PlayerLives--;
            if(lives<0 ){
                //playerScript.GameOver();
            }
        }
    }

   
    
}
