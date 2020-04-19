using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float rotationOffset = 0f;
    [Header("Player Components")]
    public PlayerController player;
    public Rigidbody2D body;
    public Camera cam;
    public GameObject toRotate;
    public Sprite stoppedSprite;

    private Vector2 movement;
    private Vector2 mousePos;
    private Animator anim;
    private SpriteRenderer rend;
    private void Awake() {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
    }

    private void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate() {
        var rota = transform.rotation;
        if (movement.x < 0) rota.y = -180; else rota.y = 0;
        transform.rotation = rota;
        body.MovePosition(body.position + movement * moveSpeed * player.speedMult * Time.fixedDeltaTime);

        if(movement.x==0 && movement.y == 0) {
            anim.enabled = false;
            rend.sprite = stoppedSprite;
        }
        else {
            anim.enabled = true;
        }

        GetComponent<Animator>().enabled = !(movement.x == 0f && movement.y == 0f);
        Vector2 lookDir = mousePos - body.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - rotationOffset;
        var rot = toRotate.transform.rotation;
        rot.eulerAngles = new Vector3(0, 0, angle);
        toRotate.transform.rotation = rot;
    }
}