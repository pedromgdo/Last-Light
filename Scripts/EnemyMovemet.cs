using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovemet : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody2D body;
    private GameObject target;
    // Start is called before the first frame update
    private void Start() {
        target = GameObject.FindGameObjectWithTag("Goal");
        if (!target) Debug.LogError("Error: Could not find an object with the goal tag.");
    }
    // Update is called once per frame
    void Update()
    {
        body.MovePosition(Vector3.MoveTowards(body.position,target.transform.position,moveSpeed * Time.fixedDeltaTime));
    }
}
