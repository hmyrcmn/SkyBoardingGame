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

    public GameObject playerObject;
    private PlayerSC playerSC;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MartiAnim =GetComponent<Animator>();

         playerObject = GameObject.Find("Player"); // Oyuncu karakterinin GameObject'ini bulun
         playerSC  = playerObject.GetComponent<PlayerSC>(); // Oyuncu karakterinin script bileşenini alın
      

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
             
            //martı simit buluşlması puan al 
            playerSC.GainPoint();
      
    
        }else if(other.gameObject.tag == "Player"){
            //martı player buluşması can azaltma
            playerSC.DecLives(); // inplayer
            
        }
    }

   
    
}
