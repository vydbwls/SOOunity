using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    Dictionary<int, string[]> EventData;
    public GameObject[] EventImage;

    public GameObject scanEventObject;

    private void Awake()
    {
        EventData = new Dictionary<int, string[]>();
    }

    private void EObjDestroy()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
