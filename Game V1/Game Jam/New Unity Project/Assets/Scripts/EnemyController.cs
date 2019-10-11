using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Rigidbody myRB;
    public float moveSpeed;

    public PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<PlayerController>();
    }
    void FixedUpdate()
    {
        myRB.velocity = (transform.forward * moveSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(thePlayer.transform.position);

    }
}
