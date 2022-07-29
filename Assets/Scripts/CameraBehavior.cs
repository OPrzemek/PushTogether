using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform cart;
    private float speed = 2.0f;
    private void Awake()
    {
        cart = GameObject.FindGameObjectWithTag("Cart").transform;
    }
    private void FixedUpdate()
    {
        Vector3 cartPosition = new Vector3(cart.position.x, cart.position.y, -10);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, cartPosition, step);
    }
}
