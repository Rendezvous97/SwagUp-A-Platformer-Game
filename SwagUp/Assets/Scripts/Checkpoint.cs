using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private bool active;
    [SerializeField] private bool canBeReactivated;

    private void OnTriggerEnter2D(Collider2D other) {

        if(active && canBeReactivated == false)
            return;

        Player player = other.GetComponent<Player>();

        if(player != null)
            ActivateCheckpoint();
    }

    private void ActivateCheckpoint()
    {
        active = true;
        anim.SetTrigger("activate");
        GameManager.instance.UpdateRespawnPosition(transform);
    }

}
