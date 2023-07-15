//using System.Collections;
//using System.Collections.Generic;
//using System;
//using UnityEngine;
//using UnityEngine.InputSystem;
//public class Controller_Test : MonoBehaviour
//{
//    private CharacterController characterController;
//    private GameObject playerModel;
//    private float horizontal;
//    private float vertical;
//    private int camResult = -1;

//    /// <summary>
//    /// ����ƶ���������
//    /// </summary>
//    private Vector3 moveDir;
//    private Vector2 InputMove;
//    PlayerInput playerInput;

//    [Header("ChooseCam")]
//    public bool HD2D;
//    public bool THIRD;
//    [Header("MainCam")]
//    public GameObject mainCamera;
//    public Transform tracePoint;
//    public float smooth;
//    private Vector3 camSpeed;

//    [Header("PlayerSpeed")]
//    public float speed;

//    [Header("PlayerAnimator")]
//    public Animator animator;


//    //���⿪ʼ
//    //private bool isHuman = true;//����or�����Կ��Ǽ���CharacterController
//    //private bool hasBell = false; // ����ֵ��Ĭ��Ϊfalse
//    //private bool isAttacked = false; // �Ƿ��ѱ������������Ƿ�����
//    //private bool isBoosted = false;// �Ƿ��ڰ������״̬
//    //private bool isStunned = false;// �Ƿ�����ѣ״̬
//    //private bool isHolding = false;// �Ƿ��ڳ���״̬
//    public float boostDuration = 3f; // ���ٳ���ʱ��
//    public float stunDuration = 10f; // ��ѣ����ʱ��
//    public float bellDuration = 10f; // �����ӷּ��ʱ��

//    //velocity
//    //score


//    private void OnTriggerEnter(Collider other)
//    {
//        Rigidbody playerRigidbody = GetComponent<Rigidbody>();

//        if (other.CompareTag("Bell") && playerRigidbody.isHuman)//������Ҫ������
//        {
//            playerRigidbody.hasBell = true;
//            moveDir = (new Vector3(0.8f * InputMove.x, 0, 0.8f * InputMove.y)).normalized;
//            playerRigidbody.velocity = moveDir;
//            //****************����ģ��Ϊ����������Ч****************

//            Destroy(other.gameObject);
//        }
//        else if (other.CompareTag("Coin") && playerRigidbody.isHuman)//��ң���Ҫ������
//        {
//            playerRigidbody.score++;
//            BellScoreManager.Instance.SetTotalScore(BellScoreManager.Instance.GetTotalScore() + 1);
//            //****************��Ч����Ч****************

//            Destroy(other.gameObject);
//        }
//        else if (other.CompareTag("") && !playerRigidbody.hasBell && playerRigidbody.isHuman)//�������ߣ��������ˣ���û�д�������
//        {

//        }
//        else if (other.CompareTag("Player") && playerRigidbody.isHuman && !other.GetComponent<Rigidbody>().isHuman)//****************��������,��д��ײ������ĳɹ���****************
//        {
//            //****************���˶�������Ч****************

//            //��δ����
//            if (!playerRigidbody.isAttacked)
//            {
//                //�������
//                if (playerRigidbody.hasBell)
//                {
//                    int addScore = 6 + (int)Math.Floor(BellScoreManager.Instance.GetTotalScore() * 0.1);
//                    other.GetComponent<Rigidbody>().score += addScore;
//                    BellScoreManager.Instance.SetTotalScore(BellScoreManager.Instance.GetTotalScore() + addScore);

//                    //****************���ת������****************

//                }
//                else
//                {
//                    int addScore = 4 + (int)Math.Floor(BellScoreManager.Instance.GetTotalScore() * 0.05);
//                    other.GetComponent<Rigidbody>().score += addScore;
//                    BellScoreManager.Instance.SetTotalScore(BellScoreManager.Instance.GetTotalScore() + addScore);
//                }
//                //����״̬
//                playerRigidbody.isAttacked = true;
//                boostDuration = 3f;
//                playerRigidbody.isBoosted = true;

//            }
//            else
//            {
//                //�������
//                if (playerRigidbody.hasBell)
//                {
//                    int addScore = 10 + (int)Math.Floor(BellScoreManager.Instance.GetTotalScore() * 0.2);
//                    other.GetComponent<Rigidbody>().score += addScore;
//                    BellScoreManager.Instance.SetTotalScore(BellScoreManager.Instance.GetTotalScore() + addScore);

//                    //****************���ת������****************

//                }
//                else
//                {
//                    int addScore = 6 + (int)Math.Floor(BellScoreManager.Instance.GetTotalScore() * 0.1);
//                    other.GetComponent<Rigidbody>().score += addScore;
//                    BellScoreManager.Instance.SetTotalScore(BellScoreManager.Instance.GetTotalScore() + addScore);
//                }
//                //�����˵�״̬
//                playerRigidbody.isAttacked = false;
//                stunDuration = 10f;
//                playerRigidbody.isStunned = true;
//                playerRigidbody.isHuman = false;
//                //****************����ģ��Ϊ����ѣ�Ĺ���Ч****************

//                //****************���ϸ��������****************
//                GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();
//                foreach (GameObject go in allGameObjects)
//                {
//                    if (go.CompareTag("Player") && go.GetComponent<Rigidbody>().isHuman && go.GetComponent<Rigidbody>().isAttacked)
//                    {
//                        go.GetComponent<Rigidbody>().isAttacked = false;
//                    }
//                }

//                //������״̬
//                other.GetComponent<Rigidbody>().isHuman = true;
//                moveDir = (new Vector3(InputMove.x, 0, InputMove.y)).normalized;
//                other.GetComponent<Rigidbody>().velocity = moveDir;
//                //****************����ģ��Ϊ�ˡ���Ч**************** ��α�ʶ��ͬ���ˣ���id��

//            }
//        }
//    }

//    void Start()
//    {
//        playerInput = GetComponent<PlayerInput>();
//        characterController = GetComponent<CharacterController>();
//        playerModel = GameObject.Find("PlayerModel");
//        camResult = CameraCheck();
//    }

//    void Update()
//    {
//        PlayerMovement();
//        MainCameraFollow(tracePoint, mainCamera);

//        Rigidbody playerRigidbody = GetComponent<Rigidbody>();

//        //���ٺ���ѣ״̬
//        if (playerRigidbody.isBoosted)
//        {
//            moveDir = (new Vector3(1.3f * InputMove.x, 0, 1.3f * InputMove.y)).normalized;
//            playerRigidbody.velocity = moveDir;
//            boostDuration -= Time.deltaTime;
//            if (boostDuration <= 0f)
//            {
//                // ����ʱ��������ָ��˵������ٶ�
//                playerRigidbody.isBoosted = false;
//                moveDir = (new Vector3(InputMove.x, 0, InputMove.y)).normalized;
//                playerRigidbody.velocity = moveDir;
//            }
//        }
//        else if (playerRigidbody.isStunned)
//        {
//            moveDir = (new Vector3(0, 0, 0)).normalized;
//            playerRigidbody.velocity = moveDir;
//            stunDuration -= Time.deltaTime;
//            if (stunDuration <= 0f)
//            {
//                // ��ѣʱ��������ָ� ��� �����ٶ�
//                playerRigidbody.isStunned = false;
//                moveDir = (new Vector3(1.2f * InputMove.x, 0, 1.2f * InputMove.y)).normalized;
//                playerRigidbody.velocity = moveDir;

//                //****************����ģ��Ϊ�����Ĺ���Ч****************
//            }
//        }

//        //����ӷ�
//        if (playerRigidbody.hasBell)
//        {
//            bellDuration -= Time.deltaTime;
//            if (bellDuration <= 0f)
//            {
//                // �����ӷ�
//                int addScore = 5 + (int)Math.Floor(BellScoreManager.Instance.GetTotalScore() * 0.01);
//                playerRigidbody.score += addScore;
//                BellScoreManager.Instance.SetTotalScore(BellScoreManager.Instance.GetTotalScore() + addScore);
//                bellDuration = 10f;
//            }
//        }
//    }

//    /// <summary>
//    /// ���ڽ���InputAction���ص������������
//    /// </summary>
//    /// <param name="value0"></param>
//    public void OnMovement(InputAction.CallbackContext value0)
//    {
//        InputMove = value0.ReadValue<Vector2>();
//    }
//    /// <summary>
//    /// ����ƶ�����
//    /// </summary>
//    void PlayerMovement()
//    {
//        //�������������ú�Sprite��ת
//        if (characterController.velocity.x > 0)
//        {
//            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
//            animator.SetBool("isRunSide", true);
//            animator.SetBool("isRunFount", false);

//        }
//        else if (characterController.velocity.x < 0)
//        {
//            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
//            animator.SetBool("isRunSide", true);
//            animator.SetBool("isRunFount", false);
//        }
//        else if (characterController.velocity.z < 0 && characterController.velocity.x == 0)
//        {
//            animator.SetBool("isRunFount", true);
//            animator.SetBool("isRunSide", false);
//        }
//        else if (characterController.velocity.z > 0 && characterController.velocity.x == 0)
//        {
//            animator.SetBool("isRunFount", false);
//            animator.SetBool("isRunSide", false);
//        }
//        else
//        {
//            animator.SetBool("isRunFount", false);
//            animator.SetBool("isRunSide", false);
//        }

//        //�ƶ�����
//        moveDir = (new Vector3(InputMove.x, 0, InputMove.y)).normalized * speed;
//        characterController.SimpleMove(moveDir);

//        //ģ�ͳ���
//        //Vector3 lookDir = transform.position + moveDir;
//        //playerModel.transform.LookAt(lookDir);

//    }

//    /// <summary>
//    /// ������ѡ���Ƿ���ȷ
//    /// </summary>
//    /// <returns></returns>
//    int CameraCheck()
//    {
//        //HD2D
//        if (!THIRD && HD2D)
//        {
//            Debug.Log("����HD2Dģʽ���");
//            return 0;
//        }
//        //�����˳�
//        else if (THIRD && !HD2D)
//        {
//            Debug.Log("����3Dģʽ���");
//            return 1;
//        }
//        else if (!THIRD && !HD2D)
//        {
//            Debug.LogWarning("��ѡ�����ģʽ");
//            return -1;
//        }
//        else
//        {
//            Debug.LogWarning("���ģʽ����ͬʱѡ��");
//            return -1;
//        }
//    }

//    /// <summary>
//    /// ������ٷ���
//    /// </summary>
//    void MainCameraFollow(Transform target, GameObject mainCamera)
//    {
//        //HD2D
//        if (camResult == 0)
//        {
//            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, target.transform.position, ref camSpeed, smooth);
//        }
//    }

//}
