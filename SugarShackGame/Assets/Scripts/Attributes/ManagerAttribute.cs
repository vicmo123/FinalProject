using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ManagerAttribute : Attribute
{
    public Type ManagerType;

    public ManagerAttribute(Type ManagerType)
    {
        this.ManagerType = ManagerType;
    }
}
