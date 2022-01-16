using System;
using System.Collections.Generic;

public class EmotionAttribution
{
    public int Value { get; set; }
    public string Name { get; }

    public EmotionAttribution(int v, string n)
    {
        Value = v;
        Name = n;
    }
}

[Serializable]
public class Egg
{
    private string Name { get; }
    private int Level { get; }

    public Egg(string n, int l)
    {
        Name = n;
        Level = l;
    }

    public List<string> GetKeyList()
    {
        return new List<string> {Name, Level.ToString()};
    }
}
