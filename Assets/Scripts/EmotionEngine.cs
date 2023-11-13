using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EmotionEngine : MonoBehaviour
{

    private GameManager gm;

    public List<Goal> goals;
    public List<Relationship> relationships;
    public EventSheet eventSheet;
    [HideInInspector]
    public string npcName;

    public List<Emotion> emotions = new List<Emotion>();

    private Emotion neutral;
    private GameObject player;
    

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        eventSheet = gm.GetComponent<EventSheet>();

        player = GameObject.Find("Player");

        InitializeEmotions();


    }


    public void UpdateGoals(List<Goal> goals)
    {
        this.goals = goals;
        ResetEmotions();
    }

    private void ResetEmotions()
    {
        foreach(Emotion e in emotions)
        {
            e.distance = 101;
        }
    }

    public void EvaluateEvent(Goal g, EmotionalEvent e)
    {
        if(CalcCongruence(g,e) > 0)
        {
            List<float> eventVector = new List<float>();

            eventVector.Add(CalcSuddeness(e));
            eventVector.Add(CalcFamiliarity(g, e));
            eventVector.Add(CalcIntrinsicPleasentness(e));
            eventVector.Add(CalcRelevance(g, e));
            eventVector.Add(CalcExpectation(g, e));
            eventVector.Add(CalcConduciveness(g, e));
            eventVector.Add(CalcUrgency(e));
            eventVector.Add(CalcSelfCausation());
            eventVector.Add(CalcOtherCausation());
            //eventVector.Add(CalcMotive());
            //eventVector.Add(CalcControl());
            eventVector.Add(CalcPower(e));
            //eventVector.Add(CalcAdjustment());
            eventVector.Add(CalcNormCompatibility(e));
            //eventVector.Add(CalcSelfCompatibility());

            foreach (Emotion emotion in emotions)
            {
                float sum = 0f;
                List<float> connectedVec = new List<float>();
                for (int i = 0; i < eventVector.Count; i++)
                {
                    connectedVec.Add(eventVector[i] - emotion.emotionVector[i]);
                    sum += (connectedVec[i] * connectedVec[i]);
                }
                if ((float)Math.Sqrt(sum) + emotion.bias < emotion.distance)
                {
                    emotion.distance = (float)Math.Sqrt(sum) + emotion.bias;
                }
            }
        }
    }

    private void InitializeEmotions()
    {
        neutral = new Emotion("neutral", true, Color.grey);
        neutral.SetEmotionVector(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
        neutral.bias = 0f;
        neutral.distance = 0f;
        emotions.Add(neutral);

        Emotion contempt = new Emotion("contempt", true, new Color32(42, 170, 214, 255));
        contempt.SetEmotionVector(2f, 2f, 2f, 2f, 2f, 3f, 2f, 1f, 5f, 1f, 3f, 4f, 4f, 2f, 0f);
        contempt.bias = 2f;
        emotions.Add(contempt);

        Emotion enjoyment = new Emotion("enjoyment", true, new Color32(237, 233, 57, 255));
        enjoyment.SetEmotionVector(2f, 2f, 4f, 3f, 4f, 4f, 1f, 2f, 3f, 2f, 0f, 0f, 5f, 0f, 0f);
        enjoyment.bias = 0f;
        emotions.Add(enjoyment);

        Emotion joy = new Emotion("joy", true, new Color32(237, 233, 57, 255));
        joy.SetEmotionVector(4f, 3f, 3f, 4f, 1f, 5f, 2f, 3f, 3f, 3f, 0f, 0f, 4f, 0f, 0f);
        joy.bias = 0f;
        emotions.Add(joy);

        Emotion disgust = new Emotion("disgust", false, new Color32(204, 205, 35, 255));
        disgust.SetEmotionVector(3f, 3f, 1f, 2f, 2f, 3f, 3f, 1f, 3f, 3f, 3f, 2f, 4f, 3f, 0f);
        disgust.bias = 2f;
        emotions.Add(disgust);

        Emotion sad = new Emotion("sad", false, new Color32(138, 138, 130, 255));
        sad.SetEmotionVector(2f, 2f, 2f, 4f, 2f, 2f, 2f, 2f, 3f, 3f, 3f, 0f, 2f, 0f, 0f);
        sad.bias = 0f;
        emotions.Add(sad);

        Emotion desperate = new Emotion("desperate", false, new Color32(57, 192, 180, 255));
        desperate.SetEmotionVector(4f, 3f, 2f, 5f, 1f, 1f, 3f, 1f, 2f, 3f, 3f, 0f, 1f, 0f, 0f);
        desperate.bias = 2f;
        emotions.Add(desperate);

        Emotion anxious = new Emotion("anxious", false, new Color32(0, 0, 0, 255));
        anxious.SetEmotionVector(2f, 5f, 2f, 3f, 2f, 2f, 3f, 1f, 4f, 3f, 3f, 2f, 3f, 0f, 0f);
        anxious.bias = 0f;
        emotions.Add(anxious);

        Emotion fear = new Emotion("fear", false, new Color32(0, 0, 0, 255));
        fear.SetEmotionVector(5f, 4f, 2f, 5f, 1f, 1f, 5f, 1f, 4f, 4f, 2f, 1f, 2f, 0f, 0f);
        fear.bias = 0f;
        emotions.Add(fear);

        Emotion irritated = new Emotion("irritated", false, new Color32(204, 205, 35, 255));
        irritated.SetEmotionVector(3f, 2f, 3f, 3f, 2f, 2f, 3f, 2f, 4f, 3f, 4f, 3f, 4f, 2f, 2f);
        irritated.bias = 1f;
        emotions.Add(irritated);

        Emotion angry = new Emotion("angry", false, new Color32(212, 65, 76, 255));
        angry.SetEmotionVector(4f, 3f, 2f, 4f, 1f, 1f, 4f, 1f, 4f, 2f, 4f, 4f, 3f, 1f, 0f);
        angry.bias = 0f;
        emotions.Add(angry);

        Emotion indifferent = new Emotion("indifferent", false, new Color32(42, 170, 214, 255));
        indifferent.SetEmotionVector(1f, 3f, 3f, 2f, 5f, 3f, 1f, 0f, 0f, 0f, 3f, 3f, 4f, 0f, 0f);
        indifferent.bias = 3f;
        emotions.Add(indifferent);

        Emotion shame = new Emotion("shame", false, new Color32(204, 205, 35, 255));
        shame.SetEmotionVector(2f, 3f, 2f, 4f, 2f, 2f, 2f, 4f, 2f, 1f, 2f, 2f, 3f, 2f, 2f);
        shame.bias = 3f;
        emotions.Add(shame);

        Emotion guilt = new Emotion("guilt", false, new Color32(204, 205, 35, 255));
        guilt.SetEmotionVector(2f, 2f, 3f, 4f, 2f, 3f, 3f, 4f, 1f, 1f, 2f, 2f, 3f, 2f, 1f);
        guilt.bias = 3f;
        emotions.Add(guilt);

        Emotion pride = new Emotion("pride", true, new Color32(145, 114, 176, 255));
        pride.SetEmotionVector(2f, 3f, 4f, 4f, 4f, 4f, 2f, 5f, 2f, 2f, 4f, 4f, 5f, 4f, 5f);
        pride.bias = 3f;
        emotions.Add(pride);
    }


    public Emotion CurrentEmotion()
    {
        float lowest = 100f;
        Emotion output = neutral;
        neutral.distance = 100;
        foreach (Emotion e in emotions)
        {
            //Debug.Log(e.tag + ": " + e.distance);
            if (e.distance < lowest)
            {
                lowest = e.distance;
                output = e;
            }
        }

        //Debug.Log("lowest: " + output.tag);


        return output;
    }

    /// <summary>
    /// Calc if event happend aprubtliy. TODO: make function.
    /// </summary>
    /// <param name="g"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    private float CalcSuddeness(EmotionalEvent e)
    {
        if (e.suddeness < 0.2f){
            return 1f;
        }else if (e.suddeness < 0.4f){
            return 2f;
        }else if (e.suddeness < 0.6f){
            return 3f;
        }else if (e.suddeness < 0.8f){
            return 4f;
        }else if (e.suddeness <= 1f){
            return 5f;
        }else{
            return 0f;
        }
    }

    /// <summary>
    /// calc if event happaned recently or if it is expected to happen in the future.
    /// </summary>
    /// <param name="g"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    private float CalcFamiliarity(Goal g, EmotionalEvent e)
    {
        float likelihood = CalcLikelihood(g, e);
        if (e.lastFire > 20f){
            return 1f;
        }else if(e.lastFire > 5f){
            return 2f;
        }else if(e.lastFire > 0f){
            return 3f;
        }else if(likelihood > 0.6f){
            return 4f;
        }else if(likelihood > 0.4f){
            return 5f;
        } else{
            return 0f;
        }
        
    }

    /// <summary>
    /// is the event objectivly positive or negativ.
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    private float CalcIntrinsicPleasentness(EmotionalEvent e)
    {
        if(e.intrinsicPleasentness < 0.20f){
            return 1f;
        }else if(e.intrinsicPleasentness < 0.40f){
            return 2f;
        }else if (e.intrinsicPleasentness < 0.60f){
            return 3f;
        } else if (e.intrinsicPleasentness < 0.80f){
            return 4f;
        }else if (e.intrinsicPleasentness <= 1f){
            return 5f;
        }else{
            return 0f;
        }
    }

    /// <summary>
    /// Calc relevance for overall goals. Disregards if positive or negative for goals. 
    /// </summary>
    /// <param name="g"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    private float CalcRelevance(Goal g, EmotionalEvent e)
    {
        float relevance = Math.Abs(CalcDesirability(g, CalcCongruence(g, e)));

        if (relevance < 0.2f){
            return 1f;
        }else if (relevance < 0.4f){
            return 2f;
        }else if (relevance < 0.6f){
            return 3f;
        }else if (relevance < 0.8f){
            return 4f;
        }else if (relevance <= 1f){
            return 5f;
        }else{
            return 0f;
        }
    }

    /// <summary>
    /// calc if event is expected. uses likelihood function for occ.
    /// </summary>
    /// <param name="g"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    private float CalcExpectation(Goal g, EmotionalEvent e)
    {
        float likelihood = CalcLikelihood(g, e);

        if (likelihood < 0.2f){
            return 1f;
        }else if(likelihood < 0.4f){
            return 2f;
        }else if (likelihood < 0.6f){
            return 3f;
        }else if (likelihood < 0.8f){
            return 4f;
        }else if (likelihood <= 1f){
            return 5f;
        }else{
            return 0f;
        }
    }

    /// <summary>
    /// Calc if event is hindering or helping existing goals. Uses desirability function for occ.
    /// </summary>
    /// <param name="g"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    private float CalcConduciveness(Goal g, EmotionalEvent e)
    {
        float desirability = CalcDesirability(g, CalcCongruence(g, e));

        if(desirability < -0.5f){
            return 1f;
        }else if(desirability < -0.05f){
            return 2f;
        }else if (desirability < 0.05f){
            return 3f;
        }else if (desirability < 0.5f){
            return 4f;
        }else if (desirability <= 1f){
            return 5f;
        }else{
            return 0f;
        }
    }

    /// <summary>
    /// Calc if event needed urgent care of self to cope.
    /// </summary>
    /// <returns></returns>
    private float CalcUrgency(EmotionalEvent e)
    {
        if(e.copeUrgency < 0.2f){
            return 1f;
        }else if(e.copeUrgency < 0.4f){
            return 2f;
        }else if (e.copeUrgency < 0.6f){
            return 3f;
        }else if (e.copeUrgency < 0.8f){
            return 4f;
        }else if (e.copeUrgency <= 1f){
            return 5f;
        }else{
            return 0f;
        }
    }
    //IRRELEVANT
    /// <summary>
    /// Calc if event happaned by cause of own. Since it is done by player by chioce, it is always 0.
    /// </summary>
    /// <returns></returns>
    private float CalcSelfCausation()
    {
        return 0f;
    }
    //IRRELEVANT
    /// <summary>
    /// Calc if event happaned by choice of other person. Since it is done by player by chioce, it is always 4.
    /// </summary>
    /// <returns></returns>
    private float CalcOtherCausation()
    {
        return 5f;
    }
    //IRRELEVANT
    /// <summary>
    /// Calc if event happened due to chance. Since it is done by player by chioce, it is always 1.
    /// </summary>
    /// <returns></returns>
    private float CalcMotive()
    {
        return 1f;
    }
    //IRRELEVANT
    /// <summary>
    /// Is the event controlled by human action. Always returns 4 since it is controlled by player.
    /// </summary>
    /// <returns></returns>
    private float CalcControl()
    {
        return 5f;
    }

    /// <summary>
    /// Calc if npc has power to cope with the event. power is influenced by distance to player.
    /// </summary>
    /// <returns></returns>
    private float CalcPower(EmotionalEvent e)
    {

        float distance = (player.transform.position - transform.position).magnitude;

        if(distance < 1f){
            return 1f;
        }else if(distance < 5f){
            return 2f;
        }else if (distance < 20f){
            return 3f;
        }else if (distance < 40f){
            return 4f;
        }else{
            return 5f;
        }

    }
    //IRRELEVANT
    /// <summary>
    /// Calc if after the event you could adjust to the situation. Returns 3 for neutrality
    /// </summary>
    /// <returns></returns>
    private float CalcAdjustment()
    {
        return 3f;
    }


    private float CalcNormCompatibility(EmotionalEvent e)
    {
        if (e.intrinsicPleasentness < 0.25f){
            return 1f;
        }else if (e.intrinsicPleasentness < 0.45f){
            return 2f;
        }else if (e.intrinsicPleasentness < 0.55f){
            return 3f;
        }else if (e.intrinsicPleasentness < 0.75f){
            return 4f;
        }else if (e.intrinsicPleasentness <= 1f){
            return 5f;
        }else{
            return 0f;
        }
    }

    //IRRELEVANT
    private float CalcSelfCompatibility()
    {
        return 0f;
    }

    public float CalcCongruence(Goal g, EmotionalEvent e)
    {

        float output = 0;
        if (g.tag == e.tag && g.actors.Contains(e.targets[0]))
        {
            output = 1;
        }
        else
        {
            output = 0;
        }
        return output;
    }

    public float CalcDesirability(Goal g, float congruence)
    {

        return congruence * g.utility;
    }

    private float CalcLikelihood(Goal g, EmotionalEvent e)
    {

        float likelihoodEvent = eventSheet.CalcLikelihoodOfEvent(e);

        float likelihoodTarget = 0;
        float count = 0;
        foreach (string s in g.actors)
        {
            count++;
            likelihoodTarget += eventSheet.CalcLikelihoodForTarget(e, s);
        }
        likelihoodTarget = likelihoodTarget / count;

        return (likelihoodTarget + likelihoodEvent) * 0.5f;
    }

}
