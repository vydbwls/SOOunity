using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : MonoBehaviour
{
    public float speed;
    private Vector3 vector;

    public float runSpeed;
    public float applyRunSpeed;
    private bool applyRun = false;

    public int walkCount;
    private int currentWalkCount;

    private bool CanMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator MoveCoroutine()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            applyRunSpeed = runSpeed;
            applyRun = true;
        }

        else
        {
            applyRunSpeed = 0;
            applyRun = false;
        }

        vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

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

            if(applyRun)
            {
                currentWalkCount++;
            }

            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }
        currentWalkCount = 0;

        CanMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(CanMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                CanMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

    }
}
