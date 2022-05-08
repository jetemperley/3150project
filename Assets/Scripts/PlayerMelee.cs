using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public float startup;
    public float activeTime;
    public float recovery;
    public int damage;

    public GameObject sword;    
    public GameObject prefabHitbox;
    public Camera playerCamera;

    public bool attacking;
    public bool canAttack;
    public bool recovering;

    private float timer;
    private Animator animator;
    private GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        timer = startup;
        attacking = false;
        canAttack = true;
        recovering = false;
        weapon = GetComponent<GunHolder>().gunRoot;
        animator = sword.GetComponent<Animator>();
        sword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && canAttack)
        {
            weapon.SetActive(false);
            sword.SetActive(true);
            animator.SetTrigger("Attack");
            timer = startup;
            attacking = true;
            canAttack = false;
            recovering = false;
        }

        if ((attacking | recovering) && timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (attacking && timer <= 0)
        {
            GameObject hitbox = Instantiate(prefabHitbox);
            MeleeHitbox meleeHitbox = hitbox.AddComponent<MeleeHitbox>();
            meleeHitbox.activeTime = activeTime;
            meleeHitbox.damage = damage;
            hitbox.transform.position = gameObject.transform.position;
            hitbox.transform.rotation = playerCamera.transform.rotation;
            attacking = false;
            recovering = true;
            timer = recovery;
        }

        if (recovering && timer <= 0)
        {
            weapon.SetActive(true);
            sword.SetActive(false);
            recovering = false;
            canAttack = true;
        }
    }
}
