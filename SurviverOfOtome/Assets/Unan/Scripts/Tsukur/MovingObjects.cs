using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    static public MovingObjects instance; // instance의 값을 공유

    public string currentMapName; // 이동할 맵 이름 저장

    // BoxCollider 컴포넌트를 가져오기 위해 선언
    private BoxCollider2D boxCollider;

    // 통과불가능한 레이어를 설정해주기 위해 선언
    public LayerMask layerMask;
    public LayerMask layerMaskObj; //오브젝트 인식까지

    //public LayerMask layerMaskEvent;
    //오디오
    public AudioClip walkSound_1;
    public AudioClip walkSound_2;
    private AudioSource audioSource;
    public int isound;

    public float speed; // 움직이는 속도 정의
    private Vector3 vector; // 움직이는 방향 정의

    public float runSpeed; // Shift키 입력시 증가하는 속도
    private float applyRunSpeed; // Shift키 입력시 연산되는 증가 속도
    private bool applyRunFlag = false; // Shift키 입력여부

    public int walkCount; // 방향키 입력시 이동값을 정하기 위한 값
    private int currentWalkCount; // 이동값 리셋을 위한 값

    private bool canMove = true; // 방향키 이동 반복실행 방지를 위한 값

    private Animator animator; //애니메이션
    public GameObject ScanObject; //오브젝트 스캔

    private GameManager theGame;
    // Start is called before the first frame update
    void Awake()
    {
        theGame = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); // 게임 오브젝트 파괴금지

            // 애니메이터 컴포넌트 가져오기
            boxCollider = GetComponent<BoxCollider2D>();
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }

            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
            {
                vector.y = 0;
            }

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;
            RaycastHit2D hitObj;
            //RaycastHit2D hitEvent;

            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            hitObj = Physics2D.Linecast(start, end, layerMaskObj);
            //hitEvent = Physics2D.Linecast(start, end, layerMaskEvent);

            boxCollider.enabled = true;

            if (hit.transform != null ) break;

            if (hitObj.transform != null)
            {
                Debug.Log("hit");
                ScanObject = hitObj.collider.gameObject;
                break;
            }

            //if(hitEvent.transform != null)
            //{
            //    Debug.Log("hit");
            //}
            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }

                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }

                if (applyRunFlag)
                {
                    currentWalkCount++;
                }

                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);

                if (currentWalkCount % 10 == 2)
                {
                    int temp = isound;
                    switch (temp)
                    {
                        case 1:
                            audioSource.clip = walkSound_1;
                            audioSource.Play();
                            break;

                        case 2:
                            audioSource.clip = walkSound_2;
                            audioSource.Play();
                            break;

                    }
                }
            }
            currentWalkCount = 0;
        }

        animator.SetBool("Walking", false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && ScanObject != null)
        {
            Debug.Log("대화창 OK");
            theGame.Action(ScanObject);
        }
    }


}
