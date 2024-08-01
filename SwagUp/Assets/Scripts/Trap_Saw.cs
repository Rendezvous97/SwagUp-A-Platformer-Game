using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private Transform[] wayPoint;
    [SerializeField] private float cooldown = 1;
    
    private Vector3[] wayPointPositions;
    public int wayPointIndex = 1;
    public int moveDirection = 1;
    private bool canMove = true;

    private void Awake() {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        UpdateWayPointInfo();
        transform.position = wayPointPositions[0];;
    }

    private void UpdateWayPointInfo()
    {
        wayPointPositions = new Vector3[wayPoint.Length];

        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPointPositions[i] = wayPoint[i].position;
        }
    }

    private void Update() {

        anim.SetBool("active", canMove);

        if(canMove == false)
            return;


        transform.position = Vector2.MoveTowards(transform.position, wayPointPositions[wayPointIndex], moveSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, wayPointPositions[wayPointIndex]) < .1f)
        {
            if ((wayPointIndex == wayPointPositions.Length - 1) || wayPointIndex ==0)
            {
                moveDirection *= -1;
                StartCoroutine(StopMovement(cooldown));
            }
            
            wayPointIndex = wayPointIndex + moveDirection;
        }
    }

    private IEnumerator StopMovement(float delay)
    {
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;
        sr.flipX = !sr.flipX;
    }

}
