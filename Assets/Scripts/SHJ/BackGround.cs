using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] float backGroundspeed; // ��� �������� �ӵ�
    [SerializeField] int backGroundstartIndex; // ���� �� ���� �ִ� ��������Ʈ �ε���
    [SerializeField] int backGroundendIndex;  // �� �Ʒ��� �ִ� ��������Ʈ �ε���


    public Transform[] backGroundSprites; //��� ��������Ʈ���� ��Ƶ� �迭
    private float backGround; //ȭ��
    // Start is called before the first frame update
    void Start()
    {
        backGround = Camera.main.orthographicSize * 2 * Camera.main.aspect;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * backGroundspeed * Time.deltaTime;

        //endIndex�� �ִ� ��������Ʈ�� ȭ�� ������ ������ �����
        if (backGroundSprites[backGroundstartIndex].position.x < -backGround)
        {
            backGroundSprites[backGroundendIndex].localPosition = backGroundSprites[backGroundstartIndex].localPosition + Vector3.right * backGround;


            int temp = backGroundstartIndex;
            backGroundstartIndex = backGroundendIndex;
            backGroundendIndex = temp;


            
        }
    }
}
