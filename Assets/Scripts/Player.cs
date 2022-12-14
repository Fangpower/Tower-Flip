using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform cam;
    [SerializeField] GameObject startUI;
    [SerializeField] GameObject doneUI;
    [SerializeField] GameObject gameUI;

    [SerializeField] AudioClip swoosh;
    [SerializeField] AudioClip coin;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem coinPart;
    [SerializeField] ParticleSystem deathPart;

    AudioSource ad;

    public enum State{
        Wait,
        Alive,
        NoRevive,
        Dead
    }
    public State state;

    private float targX = -1;
    private float deathRot = 90;
    private float deathHeight;
    private float targRot;
    private float deathYPos;

    InputManager im;

    void Awake()
    {
        im = new InputManager();

        im.Player.Switch.performed += _ => Switch();
        ad = GetComponent<AudioSource>();
    }

    void OnEnable(){
        im.Enable();
    }

    void OnDisable(){
        im.Disable();
    }

    void Update()
    {
        if(state == State.Alive){
            if(transform.position.y >= -2.5f){
                cam.position = Vector3.MoveTowards(cam.transform.position, new Vector3(0, transform.position.y+2.5f, -10), speed * Time.deltaTime);
            }

            transform.position = Vector3.MoveTowards(transform.position, 
                new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, 0), speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, 
                new Vector3(targX, transform.position.y, 0), 2 * speed * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, targRot, 0), 100 * speed * Time.deltaTime);

            speed += Time.deltaTime * 0.05f;
        } else if(state == State.Dead){
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, deathHeight, 0), 5 * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, targRot, -90), 50 * speed * Time.deltaTime);
        }

        if(im.Player.Reset.ReadValue<float>() == 1){
            PlayerPrefs.DeleteAll();
        }
    }

    void Switch(){
        if(state == State.Wait){
            
        }
        else if(state == State.NoRevive){
            //doneText.SetActive(false);
            FindObjectOfType<LoadingScreen>().Close();
        } else if(state != State.Dead && Vector2.Distance(transform.position, new Vector2(targX, transform.position.y)) < 0.25f) {
            PlaySound(swoosh);
            targX *= -1;
            deathRot += 180;
            targRot += 180;
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if(state == State.Alive){
            if(col.CompareTag("Spike")){
                deathYPos = transform.position.y;
                Instantiate(deathPart, col.transform.position, Quaternion.identity);
                
                PlaySound(death);
                state = State.Dead;
                deathHeight = transform.position.y - 10;
                doneUI.SetActive(true);
            } else if(col.CompareTag("Coin")){
                FindObjectOfType<Score>().AddCoin();
                //PlaySound(coin);
                Instantiate(coinPart, col.transform.position, Quaternion.identity);
                GameObject.Destroy(col.gameObject);
            }
        }
    }

    private void PlaySound(AudioClip clip){
        ad.clip = clip;
        ad.pitch = 1 + Random.Range(-0.1f, 0.1f);
        ad.Play();
    }

    public void Continue(){
        state = State.Alive;
        transform.position = new Vector2(targX, deathYPos);
        transform.rotation = Quaternion.Euler(0, targRot, 0);
        doneUI.SetActive(false);

        FindObjectOfType<Score>().RemoveCoin();

        foreach(Collider2D spike in Physics2D.OverlapCircleAll(transform.position, 6)){
            if(spike.CompareTag("Spike")) GameObject.Destroy(spike.gameObject);
        }
    }

    public void StartGame(){
        startUI.SetActive(false);
        gameUI.SetActive(true);
        state = State.Alive;
    }
}
