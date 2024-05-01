using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIcon : MonoBehaviour
{
    public Transform player;

    void Start()
    {

    }

    void Update()
    {
        this.transform.position = new Vector2(player.position.x * 11f + 687.7f, player.position.y * 10.38f + 355f);
    }
}