using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public string name;
    public int maxHealth;
    public int currentHealth;
    public float invulnerableTimeCooldown;

    private float invulnerableTimer;

    public Health shield;
    public AudioSource hitSFX;

    public DeathEffect effect;
   
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        invulnerableTimer = 0;
        //hitSFX = GetComponent<AudioSource>();
        hitSFX.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerableTimer>0){
            invulnerableTimer -= Time.deltaTime;
        }else{
            invulnerableTimer = 0;
        }

        if (transform.position.y < -100 && gameObject.layer == 9) {
            takeDamage(1000);
            PlayerStats.getInst().addStat("fall");
        }
    }

    public int takeDamage(int dam){
        if(invulnerableTimer<=0)
        {
            if(hitSFX != null)
            {
                hitSFX.Play(0);
            }
            
            if(gameObject.layer == 9)
            {
                invulnerableTimer = invulnerableTimeCooldown;
            }
            if (shield != null) {
                dam = shield.takeDamage(dam);
            }

            currentHealth -= dam;
            Debug.Log("health " + currentHealth);
            if (currentHealth <= 0){
                if (effect != null)
                    effect.effect();
                Destroy(gameObject);
                PlayerStats.getInst().addStat("kill "+ name);
            }

            if (currentHealth > 0)
                return 0;
            
        } 
        return currentHealth;
    }

    public int getHealth(){
        return currentHealth;
    }

    public void Reset(){
        currentHealth = maxHealth;
    }

}
