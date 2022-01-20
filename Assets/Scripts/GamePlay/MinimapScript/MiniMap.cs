using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    Transform player;
    [SerializeField] bool miniMapRotate = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = this.transform.position.y;
        this.transform.position = newPosition;


        //Minimap หมุนตาม Player
        if (miniMapRotate)
        {
            this.transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }
}
