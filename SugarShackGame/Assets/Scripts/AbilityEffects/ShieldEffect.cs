using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    public void MakeShieldEffect(float duration, Player player)
    {
        transform.position = player.transform.position;
        transform.SetParent(player.transform);

        StartCoroutine(DoShield(duration, player));
    }

    private IEnumerator DoShield(float duration, Player player)
    {
        var ragdoll = player.GetComponent<Ragdoll>();

        //ragdoll.Lock();
        yield return new WaitForSeconds(duration);
        //ragdoll.UnLock();

        Destroy(this.gameObject);
    }
}
