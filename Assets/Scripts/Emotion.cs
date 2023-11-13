using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotion
{

    public string tag;
    public bool positiveEmotion;
    public float percentage;
    public Color color;
    public float bias;

    public List<float> emotionVector = new List<float>();
    public float distance;

    public Emotion(string tag)
    {

        this.tag = tag;
        this.positiveEmotion = false;
        this.color = Color.gray;
    }

    public Emotion(string tag, bool positiveEmotion, Color color)
    {

        this.tag = tag;
        this.positiveEmotion = positiveEmotion;
        this.color = color;
    }

    public void SetEmotionVector(float nov, float time, float plea, float rele, float expec, float condu, float urgen, float egoC, float othC, float chaC, float cont, float power, float adj, float ext, float inter)
    {
        emotionVector = new List<float>();
        emotionVector.Add(nov);
        emotionVector.Add(time);
        emotionVector.Add(plea);
        emotionVector.Add(rele);
        emotionVector.Add(expec);
        emotionVector.Add(condu);
        emotionVector.Add(urgen);
        emotionVector.Add(egoC);
        emotionVector.Add(othC);
        //emotionVector.Add(chaC);
        //emotionVector.Add(cont);
        emotionVector.Add(power);
        //emotionVector.Add(adj);
        emotionVector.Add(ext);
        //emotionVector.Add(inter);
    }

    

}
