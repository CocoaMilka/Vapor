using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gameObject.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("Player hit");

            manager.GameOver();
        }
    }
}
