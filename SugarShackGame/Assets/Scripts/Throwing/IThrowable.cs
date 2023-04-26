using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable
{
    void AttachToThrower(Thrower thrower);
    void Throw(Vector3 velocity, bool addRotation = false, bool isGhost = false);
}
