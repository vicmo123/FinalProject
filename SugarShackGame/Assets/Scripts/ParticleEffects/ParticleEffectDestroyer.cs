using System.Collections;
using UnityEngine;

public class ParticleEffectDestroyer : MonoBehaviour
{
    public float duration ;
    
    private float m_MaxLifetime;
    private bool m_EarlyStop;
    private ParticleSystem[] systems;

    private void Start()
    {
        systems = GetComponentsInChildren<ParticleSystem>();
        StartCoroutine(StartEffect());
    }

    public IEnumerator StartEffect()
    {
        Debug.Log("Particle effect started");
        // find out the maximum lifetime of any particles in this effect 
        foreach (var system in systems)
        {
            m_MaxLifetime = Mathf.Max(system.main.startLifetime.constant, m_MaxLifetime);
        }

        // wait for random duration

        float stopTime = Time.time + duration;

        while (Time.time < stopTime && !m_EarlyStop)
        {
            yield return null;
        }
        Debug.Log("stopping " + name);

        // turn off emission
        foreach (var system in systems)
        {
            var emission = system.emission;
            emission.enabled = false;
        }
        BroadcastMessage("Extinguish", SendMessageOptions.DontRequireReceiver);

        // wait for any remaining particles to expire
        yield return new WaitForSeconds(m_MaxLifetime);

        Debug.Log("Particle effect is going to be destroyed");

        Destroy(this.gameObject);
    }

    public void Stop()
    {
        // stops the particle system early
        m_EarlyStop = true;
    }   
}