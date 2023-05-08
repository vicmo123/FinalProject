using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectReusable : MonoBehaviour
{
    public float duration = 8;
    
    private float m_MaxLifetime;
    private bool m_EarlyStop;
    private ParticleSystem[] systems;


    private bool isPlaying;

    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

    private void Start()
    {
        systems = GetComponentsInChildren<ParticleSystem>();

    }

    public IEnumerator StartEffect()
    {
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

        isPlaying = false;
    }

    public void Stop()
    {
        // stops the particle system early
        m_EarlyStop = true;
    }

    public GameObject Pool(GameObject objToRecycle)
    {        
        return this.gameObject;
    }
}
