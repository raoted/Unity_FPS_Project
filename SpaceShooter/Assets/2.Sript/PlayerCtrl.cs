using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerCtrl : MonoBehaviour
{
    private Transform playerPos;
    private float moveSpeed;

    float x, z;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GetComponent<Transform>();
        //playerPos = GetComponent("Transform") as Transform;
        //playerPos = (Transform)GetComponent(typeof(transform));
        //세가지 다 같은 의미이다.

        moveSpeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        x = moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        z = moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        playerPos.Translate(Vector3.forward * z, Space.Self);
        //Space.Self는 기준 좌표.
        //playerPos.position += new Vector3(z, 0, x);
    }
}
