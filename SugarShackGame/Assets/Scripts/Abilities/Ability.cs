using UnityEngine;

[System.Serializable]
public class Ability : ScriptableObject
{
    public string name;
    public bool isActive;
    public float activeTime;

    public Ability()
    {
        Activate();
    }
    public Ability(string name, bool isActive, float activeTime)
    {
        this.name = name;
        this.isActive = isActive;
        this.activeTime = activeTime;
    }

    private void Activate()
    {
        this.isActive = true;
    }
}