using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSheet : MonoBehaviour
{

    public List<EmotionalEvent> eventList = new List<EmotionalEvent>();


    private void Awake()
    {
        InitializeEvents();
    }

    private void Update()
    {
        UpdateRecency();
    }

    public void InitializeEvents()
    {
        eventList.Add(new EmotionalEvent("praise", 1f, 0.6f, 0f));
        eventList.Add(new EmotionalEvent("insult", 1f, 0.4f, 0.5f));
        eventList.Add(new EmotionalEvent("give gift", 1f, 0.9f, 0f));
        eventList.Add(new EmotionalEvent("steal item", 1f, 0.1f, 0.8f));
    }

    public EmotionalEvent SearchEvent(string input)
    {

        foreach (EmotionalEvent e in eventList)
        {

            if (e.tag == input)
            {

                return e;
            }
        }

        return null;

    }

    public void UpdateRecency()
    {

        foreach (EmotionalEvent ev in eventList)
        {
            ev.lastFire += Time.deltaTime;
        }
    }

    public List<EmotionalEvent> SortedByRecency()
    {

        List<EmotionalEvent> sortedList = eventList;
        sortedList.Sort(delegate (EmotionalEvent x, EmotionalEvent y)
        {
            return x.lastFire.CompareTo(y.lastFire);
        });

        return sortedList;
    }

    public float CalcLikelihoodOfEvent(EmotionalEvent e)
    {

        float allEvents = 0;
        foreach (EmotionalEvent ev in eventList)
        {

            allEvents += ev.count;
        }
        
        return ((e.count / allEvents) + (1 / (1 + e.lastFire))) * 0.5f;
    }

    public float CalcLikelihoodForTarget(EmotionalEvent e, string target)
    {
        float count = 0;
        float countAll = 0;
        foreach (string s in e.targets)
        {
            countAll++;
            if (target == s)
            {
                count++;
            }
        }

        return count / countAll;
    }
    
}
