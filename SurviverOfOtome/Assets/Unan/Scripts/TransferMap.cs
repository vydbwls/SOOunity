using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Scene 매니저 라이브러리 추가

public class TransferMap : MonoBehaviour
{
    public string transferMapName;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene(transferMapName);
        }
    }


}