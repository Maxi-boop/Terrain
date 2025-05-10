using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFences : MonoBehaviour
{
    public GameObject fence;
    public void SpawnFence(int quantityFence)
    {
        //int xPosition = fence.GetComponentInParent<Position>

        Instantiate(fence);

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
