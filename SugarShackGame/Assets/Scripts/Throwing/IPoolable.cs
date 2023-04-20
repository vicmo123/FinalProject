using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThrowableFactoryPool
{
    public interface IPoolable
    {
        bool IsActive { get; set; }
        void Activate();
        void Deactivate();
    }
}


