using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a singleton script that manages the speed of the terrain and enemies
public class Conductor : MonoBehaviour
{
    public float levelSpeed = 10; //Level speed is the speed at which the level progresses (Flat value)
    public float gameSpeed = 1; //Game speed is a multiplier for how things move overall (including actions)
    public float boundary = -30; 
    public float startBoundary = 30;

    private float currentPosition = 0; 
    public GameObject keepUpWith;
    public float maxDistAway = 30;

    //Singleton stuff
    public static Conductor conductor;
    
    private void Awake()
    {
        if (conductor != null && conductor != this)
        {
            Debug.Log("Conductor Destroyed");
            Destroy(this);
        }
        else
        {
            conductor = this;
            currentPosition = boundary;
        }
    }

    private void Update() 
    {
        currentPosition += Time.deltaTime*gameSpeed;
        
        if (keepUpWith.transform.position.z - currentPosition > maxDistAway){
            float dist = keepUpWith.transform.position.z - currentPosition;
            dist -= maxDistAway;
            currentPosition += dist;
        }
    }

    public float getPosition(){
        return currentPosition;
    }

    public float getLevelSpeed()
    {
        return levelSpeed;
    }

    public float getGameSpeed()
    {
        return gameSpeed;
    }

    public float getBoundary()
    {
        return boundary;
    }

    private void setSpeeds(float lspeed, float gspeed)
    {
        levelSpeed = lspeed;
        gameSpeed = gspeed;
    }
}
