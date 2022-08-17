using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    public float escapeTimeMax;
    private float escapeTime;
    public float EscapeTime 
    {
        get 
        {
            return escapeTime;
        }
    }
    public bool inBounds;

    // Start is called before the first frame update
    void Start()
    {
        inBounds = false;
        escapeTime = escapeTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!inBounds && escapeTime > 0)
        {
            escapeTime -= Time.deltaTime;
        }

        if(inBounds && escapeTime < escapeTimeMax)
        {
            escapeTime += Time.deltaTime;
        }

        if(escapeTime > escapeTimeMax)
            escapeTime = escapeTimeMax;

        if (escapeTime <= 0)
        {
            //KILL PLAYER
            Destroy(gameObject);
            escapeTime = escapeTimeMax;
        }*/
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "PlayerBoundary")
        {
            inBounds = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "PlayerBoundary")
        {
            inBounds = false;
        }
    }
}
