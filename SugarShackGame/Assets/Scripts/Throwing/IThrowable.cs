using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable
{
    public void AttachToThrower(Thrower thrower);
    public void Throw(Vector3 velocity, bool addRotation = false, bool isGhost = false);
}
