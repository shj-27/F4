using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] float backGroundspeed; // 배경 지나가는 속도
    [SerializeField] int backGroundstartIndex; // 현재 맨 위에 있는 스프라이트 인덱스
    [SerializeField] int backGroundendIndex;  // 맨 아래에 있는 스프라이트 인덱스


    public Transform[] backGroundSprites; //배경 스프라이트들을 담아둘 배열
    private float backGround; //화면
    // Start is called before the first frame update
    void Start()
    {
        backGround = Camera.main.orthographicSize * 2 * Camera.main.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * backGroundspeed * Time.deltaTime;

        //endIndex에 있는 스프라이트가 화면 밑으로 완전히 벗어나면
        if (backGroundSprites[backGroundstartIndex].position.x < -backGround)
        {
            backGroundSprites[backGroundendIndex].localPosition = backGroundSprites[backGroundstartIndex].localPosition + Vector3.right * backGround;


            int temp = backGroundstartIndex;
            backGroundstartIndex = backGroundendIndex;
            backGroundendIndex = temp;


            
        }
    }
}
