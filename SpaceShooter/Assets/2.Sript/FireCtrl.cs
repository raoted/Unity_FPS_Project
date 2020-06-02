using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//총알 발사와 재장전 오디오 클립을 저장할 구조체
[System.Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;
}
public class FireCtrl : MonoBehaviour
{
    //무기 타입
    public enum WeaponType
    {
        RIFLE = 0,
        SHOTGUN
    }

    //주인공이 현재 들고 있는 무기를 저장할 변수
    public WeaponType currWeapon = WeaponType.RIFLE;
    //총알 프리팹
    public GameObject bullet;
    //총알 발사 좌표
    public Transform firePos;

    //총구 화염 파티클
    private ParticleSystem muzzleFlash;
    //AudioSourcce 컴포넌트를 저장할 변수
    private AudioSource _audio;
    //오디오 클립을 저장할 변수
    public PlayerSfx playerSfx;

    // Start is called before the first frame update
    void Start()
    {
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        //AudioSource 컴포넌트 추출
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        //총구 화염 파티클 생성
        muzzleFlash.Play();
        //사운드 발생
        FireSfx();
    }

    void FireSfx()
    {
        //현재 들고 있는 무기의 오디오 클립을 가져옴
        var _sfx = playerSfx.fire[(int)currWeapon];
        //사운드 발생
        _audio.PlayOneShot(_sfx, 1.0f);
    }
}
