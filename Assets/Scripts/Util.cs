using System;
using System.Collections.Generic;

public class EmotionAttribution
{
    public EmotionAttribution(int v, string n)
    {
        Value = v;
        Name = n;
    }

    public int Value { get; set; }
    public string Name { get; }
}

[Serializable]
public class Egg
{
    public Egg(string n, int l)
    {
        Name = n;
        Level = l;
    }

    public string Name { get; }
    public int Level { get; }

    public List<string> GetKeyList()
    {
        return new List<string> {Name, Level.ToString()};
    }
}