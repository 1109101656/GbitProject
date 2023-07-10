using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private GameObject playerModel;
    private float horizontal;
    private float vertical;
    private int camResult=-1;
    

    /// <summary>
    /// ����ƶ���������
    /// </summary>
    private Vector3 moveDir;

    [Header("�����ģʽѡ��")]
    public bool HD2D;
    public bool THIRD;
    [Header("�����������")]
    public GameObject mainCamera;
    public Transform tracePoint;
    public float smooth;
    private Vector3 camSpeed;

    [Header("����ƶ��ٶ�")]
    public float speed;



    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerModel = GameObject.Find("PlayerModel");
        camResult= CameraCheck();
    }

    void Update()
    {
        PlayerMovement();
        MainCameraFollow(tracePoint,mainCamera);
    }

    /// <summary>
    /// ����ƶ�����
    /// </summary>
    void PlayerMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //�ƶ�����
        moveDir = (new Vector3(horizontal, 0, vertical)).normalized * speed;
        characterController.SimpleMove(moveDir);
        if (!HD2D)
        {
            //ģ�ͳ���
            Vector3 lookDir = transform.position + moveDir;
            playerModel.transform.LookAt(lookDir);
        }
    }

    /// <summary>
    /// ������ѡ���Ƿ���ȷ
    /// </summary>
    /// <returns></returns>
    int CameraCheck()
    {
        //HD2D
        if (!THIRD && HD2D)
        {
            Debug.Log("����HD2Dģʽ���");
            return 0;
        }
        //�����˳�
        else if (THIRD && !HD2D)
        {
            Debug.Log("����3Dģʽ���");
            return 1;
        }
        else if (!THIRD && !HD2D)
        {
            Debug.LogWarning("��ѡ�����ģʽ");
            return -1;
        }
        else
        {
            Debug.LogWarning("���ģʽ����ͬʱѡ��");
            return -1;
        }
    }

    /// <summary>
    /// ������ٷ���
    /// </summary>
    void MainCameraFollow(Transform target,GameObject mainCamera)
    {
        //HD2D
        if (camResult==0)
        {
            mainCamera.transform.position= Vector3.SmoothDamp(mainCamera.transform.position,target.transform.position,ref camSpeed ,smooth);
        }

        if (camResult == 1)
        {

        }
        
    }


    
}
