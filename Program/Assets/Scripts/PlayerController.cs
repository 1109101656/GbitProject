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
    /// 玩家移动方向向量
    /// </summary>
    private Vector3 moveDir;

    [Header("摄像机模式选择")]
    public bool HD2D;
    public bool THIRD;
    [Header("主摄像机设置")]
    public GameObject mainCamera;
    public Transform tracePoint;
    public float smooth;
    private Vector3 camSpeed;

    [Header("玩家移动速度")]
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
    /// 玩家移动方法
    /// </summary>
    void PlayerMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        //移动方向
        moveDir = (new Vector3(horizontal, 0, vertical)).normalized * speed;
        characterController.SimpleMove(moveDir);
        if (!HD2D)
        {
            //模型朝向
            Vector3 lookDir = transform.position + moveDir;
            playerModel.transform.LookAt(lookDir);
        }
    }

    /// <summary>
    /// 检查相机选择是否正确
    /// </summary>
    /// <returns></returns>
    int CameraCheck()
    {
        //HD2D
        if (!THIRD && HD2D)
        {
            Debug.Log("启用HD2D模式相机");
            return 0;
        }
        //第三人称
        else if (THIRD && !HD2D)
        {
            Debug.Log("启用3D模式相机");
            return 1;
        }
        else if (!THIRD && !HD2D)
        {
            Debug.LogWarning("请选择相机模式");
            return -1;
        }
        else
        {
            Debug.LogWarning("相机模式不可同时选中");
            return -1;
        }
    }

    /// <summary>
    /// 相机跟踪方法
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
