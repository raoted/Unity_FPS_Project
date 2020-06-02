using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip runF;
    public AnimationClip runB;
    public AnimationClip runL;
    public AnimationClip runR;
}
public class PlayerCtrl : MonoBehaviour
{
    private Transform playerPos;
    private float moveSpeed;
    private float rotateSpeed;

    float v, h, r;

    public PlayerAnim playerAnim;

    public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        v = h = r = 0.0f;
        playerPos = GetComponent<Transform>();
        //playerPos = GetComponent("Transform") as Transform;
        //playerPos = (Transform)GetComponent(typeof(transform));
        //세가지 다 같은 의미이다.

        moveSpeed = 10.0f;
        rotateSpeed = 80.0f;

        anim = GetComponent<Animation>();
        anim.clip = playerAnim.idle;
        anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        r = Input.GetAxis("Mouse X");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //Space.Self는 기준 좌표.
        playerPos.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        playerPos.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * r);

        if( v >= 0.1f) { anim.CrossFade(playerAnim.runF.name, 0.3f); }
        else if( v <= -0.1f) { anim.CrossFade(playerAnim.runB.name, 0.3f); }
        else if( h >= 0.1f) { anim.CrossFade(playerAnim.runR.name, 0.3f); }
        else if(h <= -0.1f) { anim.CrossFade(playerAnim.runL.name, 0.3f); }
        else { anim.CrossFade(playerAnim.idle.name, 0.3f); }
    }
}
