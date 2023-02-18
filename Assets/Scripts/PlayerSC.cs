using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSC : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rigidBody;
     public Animator PlayerTurnAnim,PlayerAnim;

      public GameObject SimitPF;
       public Transform spawnPoint;
       public GameObject SimitInstance;

    private int PlayerPoint;
    public int PlayerLives=3;

    public  GameObject[] simits;
    //PlayerSC player = FindObjectOfType<PlayerSC>();


    private void Start()
    {
        
         simits = GameObject.FindGameObjectsWithTag("Simit");
        rigidBody = GetComponent<Rigidbody>();

       PlayerTurnAnim = GetComponent<Animator>();
       PlayerAnim= GetComponent<Animator>();
        SimitInstance = Instantiate(SimitPF, spawnPoint.position, spawnPoint.rotation);

    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        rigidBody.velocity = direction * speed;

        //player moving kontrol 
        if (transform.position.z > 9.0f){
            transform.position =new Vector3(transform.position.x, transform.position.y, transform.position.z-9.0f); 
        }else if (transform.position.z <-10.0f){
            transform.position =new Vector3(transform.position.x, transform.position.y, 0); 
        }else if (transform.position.x < -10.0f){
            transform.position =new Vector3(transform.position.x+7.0f, transform.position.y, transform.position.z);   
        }else if (transform.position.x > 10.0f){
            transform.position =new Vector3(transform.position.x-7.0f, transform.position.y, transform.position.z); 
        }

        // player atributes : space turn 
       
         if (Input.GetKey(KeyCode.Space) && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            // player show
            //Debug.Log("Both keys are being pressed");
            PlayerTurnAnim.Play("PlayerTurnAnim");
           

        }
      
            // when mause clicked * dont forget do see the smiti
         if (Input.GetMouseButtonDown(0))
        {
            //player throw
             PlayerAnim.Play("PlayerAnim");
            
            GameObject simit = Instantiate(SimitPF, transform.position, transform.rotation); // Simiti oluşturun
        Rigidbody simitRigidbody = simit.GetComponent<Rigidbody>(); // Simitin rigidbody bileşenini alın
        
        // Simitin hedef noktasını fare imlecinin tıklanma konumu olarak ayarlayın
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Vector3 hedefNokta = hit.point;
            Vector3 hareketYonu = (hedefNokta - transform.position).normalized;
            simitRigidbody.AddForce(hareketYonu * speed, ForceMode.Impulse); // Simiti hareket ettirin
        }
        
        FixedUpdate();      
          
        }



    }

    public void setNonVisibleTheSmit()
    {
        GameObject myObject = GameObject.FindWithTag("Simit");
        if (myObject != null)
        {
            myObject.SetActive(false);
        }
    }
// Atış nesnesini hareket ettir
    public void Throw(){
   
  Vector3 mausePozisyonu = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Fare tıklama pozisyonunu dünya koordinatlarına çevir
        mausePozisyonu.z = 0; // Z ekseni sıfır
        GameObject simit = Instantiate(SimitPF, mausePozisyonu, Quaternion.identity); // Simit prefab'ını fare tıklama pozisyonuna göre oluştur
       Rigidbody2D simitRigidbody = simit.GetComponent<Rigidbody2D>(); // Rigidbody2D bileşenine eriş
        Vector2 MovingRot = (mausePozisyonu - transform.position).normalized; // Simitin hareket yönünü belirle
        simitRigidbody.AddForce(MovingRot * speed, ForceMode2D.Impulse); // Simit nesnesine hareket yönünde hız kazandır
    
    FixedUpdate();
        
    }

    void FixedUpdate()
    {
       // GameObject[] simits = GameObject.FindGameObjectsWithTag("Simit");
        foreach (GameObject simit in simits)
        {
            if (simit.transform.position.y < -5.0f || simit.transform.position.y >5.0f || simit.transform.position.x>10.0f || simit.transform.position.x<-10.0f)
            {
             
                //Destroy(simit);
               
            }
        }
    }
    public void GainPoint(){
        PlayerPoint+=5;
        print(PlayerPoint);
    }
    public void GameOver(){
        print(PlayerPoint);// in uı
        //Destroy(player,5.0f);// delete player

    }
    public void DecLives(){
       if( PlayerLives>0){
            PlayerLives--;
       }else{
            GameOver();
       }
    }
}
