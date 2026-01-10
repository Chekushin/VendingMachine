using System;

public abstract class Item
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Description { get; set; }
    public int Level { get; set; } = 1;

    protected Item(string name, string desc)
    {
        Name = name;
        Description = desc;
    }
}