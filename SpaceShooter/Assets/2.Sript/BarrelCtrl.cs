using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //폭발 효과 프리팹을 저장할 변수
    public GameObject expEffect;
    //총알이 맞은 횟수
    private int hitCount = 0;
    //Rigidbody 컴포넌트를 저장할 변수
    private Rigidbody rb;

    //찌그러진 드럼동 메쉬를 저장할 배열
    public Mesh[] meshes;
    //MeshFilter 컴포넌트를 저장할 변수
    public MeshFilter meshFilter;

    //드럼통의 텍스처를 저장할 배열
    public Texture[] textures;
    //MeshRenderer 컴포넌트를 저장할 변수
    private MeshRenderer _renderer;

    //폭발 반경
    public float expRadius = 10.0f;
    //AudioSource 컴포넌트를 저장할 변수
    private AudioSource _audio;

    //폭발음 오디오 클립
    public AudioClip expSfx;
    // Start is called before the first frame update
    void Start()
    {
        //RigidBody 컴포넌트를 추출해 저장
        rb = GetComponent<Rigidbody>();

        //MeshFilter 컴포넌트를 추출해 저장
        meshFilter = GetComponent<MeshFilter>();

        //MeshRenderer 컴포넌트를 추출하여 저장
        _renderer = GetComponent<MeshRenderer>();

        //난수를 발새이켜 불규칙적인 텍스처를 적용
        _renderer.material.mainTexture = textures[Random.Range(0, textures.Length)];

        //AudioSource 컴포넌트를 추출해 저장
        _audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BULLET"))
        {
            if(++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        //폭발 효과 프리팹을 동적으로 생성
        GameObject effect = Instantiate(expEffect, transform.position, Quaternion.identity);
        //Rigidbody 컴포넌트의 mass를 1.0으로 수정해 무게를 가볍게 함.
        rb.mass = 1.0f;
        //위로 솟구치는 힘을 가함
        rb.AddForce(Vector3.up * 1000.0f);
        IndiredtDemage(transform.position);
        Destroy(effect, 2.0f);
        
        //난수를 발생
        int idx = Random.Range(0, meshes.Length);
        //찌그러진 메쉬를 적용
        meshFilter.sharedMesh = meshes[idx];
        GetComponent<MeshCollider>().sharedMesh = meshes[idx];

        //폭발음 생성
        _audio.PlayOneShot(expSfx, 1.0f);
    }

    void IndiredtDemage(Vector3 pos)
    {
        Collider[] colls = Physics.OverlapSphere(pos, expRadius, 1 << 8);

        foreach(var coll in colls)
        {
            //폭발 범위에 포함된 드럽통의 Rigidbody 컴포넌트 추출
            var _rb = coll.GetComponent<Rigidbody>();
            //드럼통의 무게를 가볍게 함
            _rb.mass = 1.0f;
            //폭발력을 전달
            _rb.AddExplosionForce(1200.0f, pos, expRadius, 1000.0f);
        }
    }
}
