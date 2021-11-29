using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] private Transform preyPrefab;
    [SerializeField] private Transform hunterPrefab;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float size;

    System.Random random = new System.Random();

    void Start()
    {
        prefabRender();
    }

    void Update()
    {
        
    }

    public void prefabRender()
    {
        Transform prey = Instantiate(preyPrefab, transform);
        prey.position = new Vector3(random.Next(-width/2, width/2), random.Next(-height/2, height/2));
        Transform hunter = Instantiate(hunterPrefab, transform);
        hunter.position = new Vector3(random.Next(-width/2, width/2), random.Next(-height/2, height/2));
    }
}
