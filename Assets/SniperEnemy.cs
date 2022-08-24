using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour
{

    Transform target;
    Rigidbody rb;
    public float moveSpeed = 3;
    public float maxLookDist = 10;
    public string targetTag = "Player";


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {

        if (target == null || (target.position - transform.position).magnitude > maxLookDist || target.position.z > rb.position.z)
            return;

        Vector3 direction = target.position-transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        //rb.MovePosition( rb.position +(target.position - rb.position).normalized*moveSpeed*Time.fixedDeltaTime);
    }

    private void Shoot(){
        //RaycastHit hitInfo
    }
}
