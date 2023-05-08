using UnityEngine;

public class ParticleEffectTest : MonoBehaviour
{
    public Transform attatchPoint;
    public ParticleEffectManager partEffectManager;

    private void Start()
    {
        GameObject particleEffect = partEffectManager.Create(ParticleEffectType.Fireworks);
        particleEffect.transform.position = attatchPoint.position;
    }
}
