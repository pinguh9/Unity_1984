using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedDrop<T>
{
    List<DropElement> DropList = new List<DropElement>();

    public class DropElement
    {
        public T target;
        public float probability;

        public  DropElement(T target, float probability)
        {
            this.target = target;
            this.probability = probability;
        }
    }

    public void Add(T target, float probability)
    {
        DropList.Add(new DropElement(target, probability));
    }

    public T Get()
    {
        float totalProbability = 0;
        foreach (DropElement item in DropList)
        {
            totalProbability += item.probability;
        }

        float pick = Random.value * totalProbability;
        foreach(DropElement item in DropList)
        {
            if (pick < item.probability)
                return item.target;
            else 
                pick -= item.probability;
        }

        return default(T);
    }
}
