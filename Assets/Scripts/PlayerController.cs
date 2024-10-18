using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerController : MonoBehaviour
{    
    [SerializeField] float speed;
    [SerializeField] Animator playerAnim;
    [SerializeField] Slider slider;
    private int health;    
    [SerializeField] AudioClip kura;   
    private float oldMousePositionX;
    private float eulerY;   
    public bool gameOver = false;
    private AudioSource playerAudio;
    [SerializeField] GameObject redOverlay;
    [SerializeField] TextMeshProUGUI CounterText;
    [SerializeField] TextMeshProUGUI RecordText;
    private int Count;
    private int record;
    [SerializeField] GameObject restart;

    void Start()
    {
        Load();
        slider.value = health;
        playerAudio = GetComponent<AudioSource>();        
    }
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            oldMousePositionX = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0) && !gameOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);            
            float deltaX = Input.mousePosition.x - oldMousePositionX;
            oldMousePositionX = Input.mousePosition.x;
            eulerY += deltaX;
            transform.eulerAngles = new Vector3(0, eulerY, 0);
            playerAnim.SetFloat("Speed_f", 1);
            RangeMove();
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerAnim.SetFloat("Speed_f", 0);
        }
    }
    void RangeMove()
    {
        if (transform.position.x < -50)
        {
            transform.position = new Vector3(-50, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 50)
        {
            transform.position = new Vector3(50, transform.position.y, transform.position.z);
        }
        if (transform.position.z < -50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -50);
        }
        if (transform.position.z > 50)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 50);
        }
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
    void TakeDamage(int damage)
    {
        health -= damage;
        slider.value = health;
        SaveHealth();        
        if (health <= 0)
        {           
            gameOver = true;
            Count = 0;
            SaveCount();            
            StartCoroutine(Restart());
        }       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chicken"))
        {
            playerAudio.PlayOneShot(kura, 2);
            if (health < 15)
            {
                TakeDamage(-1);
            }                       
            Count += 1;
            SaveCount();
            AddNewRecord();
        }
        if (other.CompareTag("Dog"))
        {
            TakeDamage(5);            
            StartCoroutine(RedOverlay());
        }
    }    
    IEnumerator RedOverlay()
    {
        redOverlay.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        redOverlay.SetActive(false) ;
    }
    IEnumerator Restart()
    {        
        yield return new WaitForSeconds(1);
        restart.SetActive(true);        
    }    
    void SaveCount() 
    {
        CounterText.text = " " + Count;
        PlayerPrefs.SetInt("Count",Count);
    }   
    void SaveHealth() 
    { 
        PlayerPrefs.SetInt("Health", health);
    }   
    void AddNewRecord()
    {
        if (Count > record)
        {
            record = Count;
            PlayerPrefs.SetInt("Record",record);
            YandexGame.NewLeaderboardScores("SaveRecord", record);
            RecordText.text = " " + record;
        }
    }
    void Load()
    {
        health = PlayerPrefs.GetInt("Health");
        if (health <= 0)
        {
            health = 15;
        }
        Count = PlayerPrefs.GetInt("Count");
        CounterText.text = " " + Count;
        record = PlayerPrefs.GetInt("Record");
        RecordText.text = " " + record;
    }
}
