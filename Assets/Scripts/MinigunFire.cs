using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunFire : MonoBehaviour
{
    public AudioClip fireSFX;
    public Bullet bulletPrefab;
    public float maxFireRate; //seconds
    public float minFireRate; //seconds
    public float maxFireRadius;
    public float minFireRadius;
    public float fireRadiusIncrement;
    public float fireRateIncrement;
    public int damage;

    private Camera cam;
    private Animator animator;
    private bool audioPlaying;
    private int volumeAdjuster;

    public GameObject spawnPoint;

    private float currentFireRate;
    private float currentFireRadius;
    private float fireTimer; //seconds
    LineRenderer line;
    


    void Start()
    {
        fireTimer = 0f;
        animator = GetComponent<Animator>();
        cam = (Camera)FindObjectOfType(typeof(Camera));
        audioPlaying = false;
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        currentFireRadius = minFireRadius;
        currentFireRate = minFireRate;
        volumeAdjuster = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && fireTimer <= 0 && !PauseMenu.gameIsPaused)
        {

                if(volumeAdjuster < 1)
                {
                    volumeAdjuster = 5;
                }
                AudioSource audio = AudioPool.GetAudioSource();
                audio.clip = fireSFX;
                audio.volume = 0.75f/volumeAdjuster;
                audio.Play(0);
                volumeAdjuster--;

            
            
            

            Vector3 hitPos;
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000)){
                hitPos = hit.point;
            } else {
                hitPos = cam.transform.position + cam.transform.forward*1000;
            }

            Vector3 hitDir = (hitPos - spawnPoint.transform.position).normalized; 
            Vector3 dir = hitDir 
                + new Vector3(
                    Random.Range(currentFireRadius,-currentFireRadius),
                    Random.Range(currentFireRadius,-currentFireRadius),
                    0);
            

            if(currentFireRadius < maxFireRadius)
            {
                currentFireRadius += fireRadiusIncrement*Time.deltaTime;
            }else{
                currentFireRadius = maxFireRadius;
            }
            if(currentFireRate <= maxFireRate)
            {
                currentFireRate = maxFireRate;
            }else{
                currentFireRate -= fireRateIncrement*Time.deltaTime;
            }
            fireTimer = currentFireRate;
//          Debug.Log(dir);
            Laser laser = LaserPool.GetLaser();
            laser.SetDamage(damage);
            laser.fire(
                spawnPoint.transform.position,
                dir,
                0.1f,
                gameObject.name
                );
            animator.SetBool("Shooting",true);
            
        }else if(!Input.GetButton("Fire1"))
        {
            currentFireRate = minFireRate;
            currentFireRadius = minFireRadius;
            audioPlaying = false;
            animator.SetBool("Shooting",false);
        } else {
            line.enabled = false;
        }

        fireTimer -= Time.deltaTime;
    }
}
