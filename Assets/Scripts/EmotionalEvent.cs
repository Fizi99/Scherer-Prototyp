using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionalEvent
{

    
    public string tag;
    public float congruence;

    public int count;
    public float lastFire;
    public float suddeness;
    public float intrinsicPleasentness;
    public float copeUrgency;

    public GameObject actor;

    public List<string> targets = new List<string>();

    public EmotionalEvent(string tag, float suddeness, float intrinsicPleasentness, float copeUrgency)
    {

        this.tag = tag;
        this.congruence = 0;
        this.count = 1;
        this.lastFire = 0f;
        this.suddeness = suddeness;
        this.intrinsicPleasentness = intrinsicPleasentness;
        this.copeUrgency = copeUrgency;
    }

    public EmotionalEvent(string tag, string target, float suddeness, float intrinsicPleasentness, float copeUrgency)
    {

        this.tag = tag;
        this.congruence = 0;
        this.targets.Add(target);
        this.count = 1;
        this.lastFire = 0f;
        this.suddeness = suddeness;
        this.intrinsicPleasentness = intrinsicPleasentness;
        this.copeUrgency = copeUrgency;
    }

    public void EventFired()
    {
        lastFire = 0f;
        count++;
    }
    
}
