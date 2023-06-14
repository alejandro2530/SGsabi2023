using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    public GameObject imag1GO;
    public GameObject imag2GO;
    private float t;
    private Vector2 s1;
    private Vector2 s2;
    private float dist;
    private Vector2 dist2;
    private float dist3;
    private float dist4;
    void Start()
    {
        s1 = new Vector2(1,1);
        s2 = new Vector2(5,5);  
        dist = (imag1GO.transform.position - imag2GO.transform.position).sqrMagnitude;
        dist2 = imag2GO.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        dist = (imag1GO.transform.position - imag2GO.transform.position).sqrMagnitude;
        dist3 = 1/dist;
        Debug.Log(dist3);

        imag1GO.transform.localScale = new Vector2(Mathf.Lerp(s1.x,s2.x,dist3),Mathf.Lerp(s1.y,s2.y,dist3));
    }
}
