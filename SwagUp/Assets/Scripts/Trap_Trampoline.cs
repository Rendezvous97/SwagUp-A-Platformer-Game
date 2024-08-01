using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Trampoline : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float pushPower = 25;
    [SerializeField] private float pushDuration =.5f;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();

        if(player != null)
        {
            player.Push(transform.up*pushPower, pushDuration);
            anim.SetTrigger("activate");

        }
            
    }
}
