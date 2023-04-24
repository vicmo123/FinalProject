using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect 
{
     void ActivateDefault();
     void ActivatePlayerHit(Player player);
     void ActivateAnimalHit(Animal animal);
     void ActivateEnvironmentHit(Vector3 position);
     void SubscribeToManager();
     void UnsubscribeToManager();    
}
