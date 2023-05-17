using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    public void ProtectPlayer(Player player, float duration)
    {
        transform.position = player.transform.position;
        transform.SetParent(player.transform);

        StartCoroutine(ApplyProtection(player, duration));
    }

    private IEnumerator ApplyProtection(Player player, float duration)
    {
        player.gameObject.GetComponent<Ragdoll>();
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
