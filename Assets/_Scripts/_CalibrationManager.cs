 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CalibrationManager : MonoBehaviour
{
    
    public GameObject headGO;
    public GameObject handLeftGO;
    public GameObject handRightGO;
    public GameObject elbowLeftGO;
    public GameObject elbowRightGO;
    public GameObject shoulderLeftGO;
    public GameObject shoulderRightGO;
    public GameObject spineShoulderGO;
    public GameObject[] segmentEfectorGOLeft = new GameObject[2];
    public GameObject[] segmentEfectorGORight = new GameObject[2];
    public GameObject startHandLeftT;
    public GameObject startHandRightT;
    public GameObject startElbowLeftT;
    public GameObject startElbowRightT;
    public GameObject startSpineShoulderT;
    [SerializeField] float long1Left;// arm left
    [SerializeField] float long1Right;//arm right
    [SerializeField] float long2Left;//forearma left
    [SerializeField] float long2Right;//forearm right
    [SerializeField] private GameObject pointHandLeftGO;
    [SerializeField] private GameObject pointTouchHandRight3GO;
    [SerializeField] public GameObject prefGoLeft;
    [SerializeField] private GameObject prefGoRight;
    [SerializeField] public GameObject areaLeft;
    [SerializeField] public GameObject areaRight;
    public Sprite handSprite;
    private int flag = 0;
    private Vector3 lookSegmentLeft;
    private Vector3 lookSegmentRight;
    public int numSegmentLeft ;
    public int numSegmentRight;
    [SerializeField] public static int stateULLeft;
    [SerializeField] public static int stateULRight;
    void Start()
    {
        /*saveConfigManagerGO = GameObject.FindGameObjectWithTag("dataConfig");
        saveConfigManagerScript = saveConfigManagerGO.GetComponent<SaveConfigManager>();
        for(int i = 0 ; i <  saveConfigManagerScript.savePrefLeft.Length ; ++i)
        {
            GameObject auxGoLeft =  Instantiate(prefGoLeft,new Vector2(0,0),Quaternion.identity);
            auxGoLeft.transform.SetParent(areaLeft.transform);
            auxGoLeft.transform.localPosition = saveConfigManagerScript.positionPrefLeft[i];
            auxGoLeft.transform.localScale = saveConfigManagerScript.scalePrefLeft[i];
            //active the script to drag and drop
            auxGoLeft.gameObject.GetComponent<DaDPoint>().enabled = true;
        } 
        for(int i = 0 ; i <  saveConfigManagerScript.savePrefRight.Length ; ++i)
        {
            GameObject auxGoRight =  Instantiate(prefGoRight,new Vector2(0,0),Quaternion.identity);
            auxGoRight.transform.SetParent(areaRight.transform);
            auxGoRight.transform.localPosition = saveConfigManagerScript.positionPrefRight[i];
            auxGoRight.transform.localScale = saveConfigManagerScript.scalePrefRight[i];
            //active the script to drag and drop
            auxGoRight.gameObject.GetComponent<DaDPoint>().enabled = true;
        }  */
    }
    void Update()
    {
        headGO = GameObject.Find("Neck");
        handLeftGO = GameObject.Find("HandLeft");
        handRightGO = GameObject.Find("HandRight");
        elbowLeftGO = GameObject.Find("ElbowLeft");
        elbowRightGO = GameObject.Find("ElbowRight");
        shoulderLeftGO = GameObject.Find("ShoulderLeft");
        shoulderRightGO = GameObject.Find("ShoulderRight");
        spineShoulderGO = GameObject.Find("SpineShoulder");

        segmentEfectorGOLeft[0] = handLeftGO = GameObject.Find("HandLeft");
        segmentEfectorGOLeft[1] = elbowLeftGO = GameObject.Find("ElbowLeft");

        segmentEfectorGORight[0] = handRightGO = GameObject.Find("HandRight");
        segmentEfectorGORight[1] = elbowRightGO = GameObject.Find("ElbowRight");

        if(handLeftGO != null && handRightGO != null)
        {
            handLeftGO.transform.localPosition = new Vector3(handLeftGO.transform.localPosition.x,handLeftGO.transform.localPosition.y,0f); 
            handRightGO.transform.localPosition = new Vector3(handRightGO.transform.localPosition.x,handRightGO.transform.localPosition.y,0f); 
        }    


        if(shoulderLeftGO != null && shoulderRightGO != null)
        {
            SpriteRenderer shoulderLeftRenderer = shoulderLeftGO.GetComponent<SpriteRenderer>();
            shoulderLeftRenderer.enabled = true;

            SpriteRenderer shoulderRightRenderer = shoulderRightGO.GetComponent<SpriteRenderer>();
            shoulderRightRenderer.enabled = true;

        }    
        if(flag == 0 & handLeftGO != null) // tomo solo un punto para tener la referencia de que esta o no en la escena
        {
            handLeftGO.transform.localScale = new Vector3(-1.7f,1.7f,1.7f);
            handLeftGO.tag = "hand";
            SpriteRenderer handLeftRend = handLeftGO.GetComponent<SpriteRenderer>();
            handLeftRend.color = Color.red;
            handLeftRend.sprite = handSprite;
            Rigidbody2D handLeftRig = handLeftGO.AddComponent<Rigidbody2D>();
            handLeftRig.bodyType = RigidbodyType2D.Kinematic;
            CircleCollider2D handLeftColl = handLeftGO.AddComponent<CircleCollider2D>();
            handLeftColl.isTrigger = false;
            //creo colider en hijo del GO principal para que funcione como trigger ya que no lo puedo colocar en el principal por que con fisicas no puede ser trigger.
            CircleCollider2D handLeftCollChild = handLeftGO.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
            handLeftCollChild.isTrigger = true;
            numSegmentLeft = 0;

            handRightGO.transform.localScale = new Vector3(1.7f,1.7f,1.7f);
            handRightGO.tag = "hand";
            SpriteRenderer handRightRend = handRightGO.GetComponent<SpriteRenderer>();
            handRightRend.color = Color.magenta;
            handRightRend.sprite = handSprite;
            Rigidbody2D handRightRig = handRightGO.AddComponent<Rigidbody2D>();
            handRightRig.bodyType = RigidbodyType2D.Kinematic;
            CircleCollider2D handRightColl = handRightGO.AddComponent<CircleCollider2D>();
            handRightColl.isTrigger = false;
            //creo colider en hijo del GO principal para que funcione como trigger ya que no lo puedo colocar en el principal por que con fisicas no puede ser trigger.
            CircleCollider2D handRightCollChild = handRightGO.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
            handRightCollChild.isTrigger = true;
            numSegmentRight = 0;

            elbowLeftGO.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            /*elbowLeftGO.tag = "elbow";
            SpriteRenderer elbowLeftRend = elbowLeftGO.GetComponent<SpriteRenderer>();
            elbowLeftRend.color = Color.red;
            elbowLeftGO.layer = LayerMask.NameToLayer("noLook");*/
            elbowLeftGO.transform.localScale = new Vector3(-1.7f,1.7f,1.7f);
            SpriteRenderer elbowLeftRend = elbowLeftGO.GetComponent<SpriteRenderer>();
            elbowLeftRend.color = Color.yellow;
            elbowLeftRend.sprite = handSprite;
            //Rigidbody2D elbowLeftRig = elbowLeftGO.AddComponent<Rigidbody2D>();
            //elbowLeftRig.bodyType = RigidbodyType2D.Kinematic;
            //CircleCollider2D elbowLeftColl = elbowLeftGO.AddComponent<CircleCollider2D>();
            //elbowLeftColl.isTrigger = false;
            //creo colider en hijo del GO principal para que funcione como trigger ya que no lo puedo colocar en el principal por que con fisicas no puede ser trigger.
            //CircleCollider2D elbowLeftCollChild = elbowLeftGO.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
            //elbowLeftCollChild.isTrigger = true;
            /////seteo inicial/////
            //elbowLeftGO.GetComponent<Collider2D>().enabled = false;
            elbowLeftGO.GetComponent<SpriteRenderer>().enabled = false;

            elbowRightGO.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            /*elbowRightGO.tag = "elbow";
            SpriteRenderer elbowRightRend = elbowRightGO.GetComponent<SpriteRenderer>();
            elbowRightRend.color = Color.red;
            elbowRightGO.layer = LayerMask.NameToLayer("noLook");*/
            elbowRightGO.transform.localScale = new Vector3(1.7f,1.7f,1.7f);
            SpriteRenderer elbowRightRend = elbowRightGO.GetComponent<SpriteRenderer>();
            elbowRightRend.color = Color.yellow;
            elbowRightRend.sprite = handSprite;
            //Rigidbody2D elbowRightRig = elbowRightGO.AddComponent<Rigidbody2D>();
            //elbowRightRig.bodyType = RigidbodyType2D.Kinematic;
            //CircleCollider2D elbowRightColl = elbowRightGO.AddComponent<CircleCollider2D>();
            //elbowRightColl.isTrigger = false;
            //creo colider en hijo del GO principal para que funcione como trigger ya que no lo puedo colocar en el principal por que con fisicas no puede ser trigger.
            //CircleCollider2D elbowRightCollChild = elbowRightGO.transform.GetChild(0).gameObject.AddComponent<CircleCollider2D>();
            //elbowLeftCollChild.isTrigger = true;
            /////seteo inicial/////
            //elbowRightGO.GetComponent<Collider2D>().enabled = false;
            elbowRightGO.GetComponent<SpriteRenderer>().enabled = false;

            /*/////seteo inicial left/////
            if(stateULLeft == 0)
            {
            elbowLeftGO.GetComponent<Collider2D>().enabled = false;
            elbowLeftGO.GetComponent<SpriteRenderer>().enabled = false;

            handLeftGO.GetComponent<Collider2D>().enabled = true;
            handLeftGO.GetComponent<SpriteRenderer>().enabled = true;

            numSegmentLeft = 0;
            }
            if(stateULLeft == 1)
            {
            //flag = 0; // para volver a iterar el update 
            //segEfLeft = 1;
            elbowLeftGO.GetComponent<Collider2D>().enabled = true;
            elbowLeftGO.GetComponent<SpriteRenderer>().enabled = true;

            handLeftGO.GetComponent<Collider2D>().enabled = false;
            handLeftGO.GetComponent<SpriteRenderer>().enabled = false;

            numSegmentLeft = 1;
            }
            ///// seteo incial right/////
            if(stateULRight == 0)
            {
                //flag = 0; // para volver a iterar el update 
                //segEfLeft = 0;
                elbowRightGO.GetComponent<Collider2D>().enabled = false;
                elbowRightGO.GetComponent<SpriteRenderer>().enabled = false;

                handRightGO.GetComponent<Collider2D>().enabled = true;
                handRightGO.GetComponent<SpriteRenderer>().enabled = true;

                numSegmentRight = 0;
            }
            if(stateULRight == 1)
            {
                //flag = 0; // para volver a iterar el update 
                //segEfLeft = 1;
                elbowRightGO.GetComponent<Collider2D>().enabled = true;
                elbowRightGO.GetComponent<SpriteRenderer>().enabled = true;

                handRightGO.GetComponent<Collider2D>().enabled = false;
                handRightGO.GetComponent<SpriteRenderer>().enabled = false;

                numSegmentRight = 1;
            }*/
            spineShoulderGO.tag = "tronco";
            Rigidbody2D spineShoulderRig = spineShoulderGO.AddComponent<Rigidbody2D>();
            spineShoulderRig.bodyType = RigidbodyType2D.Kinematic;
            CircleCollider2D spineShoulderColl = spineShoulderGO.AddComponent<CircleCollider2D>();
            spineShoulderColl.isTrigger = false;
            spineShoulderGO.layer = LayerMask.NameToLayer("noLook");

            flag = 1;
        }
        if(flag == 1 & handLeftGO == null)
        {
            flag = 0;
        }
        /*if(Input.GetButtonDown("Fire1") & handLeftGO & handRightGO & elbowLeftGO & elbowRightGO != null)
        {
            StartPointBody();
            ReffPointsStart();
        }*/
    FrotationSegmentLeft(numSegmentLeft);
    FrotationSegmentRight(numSegmentRight);
    }
    public void selecteEfectLeft(int i)
    {
        if(i == 0)
        {
            stateULLeft = 0;
            //flag = 0; // para volver a iterar el update 
            //segEfLeft = 0;
            elbowLeftGO.GetComponent<Collider2D>().enabled = false;
            elbowLeftGO.GetComponent<SpriteRenderer>().enabled = false;

            handLeftGO.GetComponent<Collider2D>().enabled = true;
            handLeftGO.GetComponent<SpriteRenderer>().enabled = true;

            numSegmentLeft = 0;
        }
        if(i == 1)
        {
            stateULLeft = 1;
            //flag = 0; // para volver a iterar el update 
            //segEfLeft = 1;
            elbowLeftGO.GetComponent<Collider2D>().enabled = true;
            elbowLeftGO.GetComponent<SpriteRenderer>().enabled = true;

            handLeftGO.GetComponent<Collider2D>().enabled = false;
            handLeftGO.GetComponent<SpriteRenderer>().enabled = false;

            numSegmentLeft = 1;
        }
    }

    public void selecteEfectRight(int i)
    {
        if(handLeftGO != null)
        {
            if(i == 0)
            {
                stateULRight = 0;
                //flag = 0; // para volver a iterar el update 
                //segEfLeft = 0;
                elbowRightGO.GetComponent<Collider2D>().enabled = false;
                elbowRightGO.GetComponent<SpriteRenderer>().enabled = false;

                handRightGO.GetComponent<Collider2D>().enabled = true;
                handRightGO.GetComponent<SpriteRenderer>().enabled = true;

                numSegmentRight = 0;
            }
            if(i == 1)
            {
                stateULRight = 1;
                //flag = 0; // para volver a iterar el update 
                //segEfLeft = 1;
                elbowRightGO.GetComponent<Collider2D>().enabled = true;
                elbowRightGO.GetComponent<SpriteRenderer>().enabled = true;

                handRightGO.GetComponent<Collider2D>().enabled = false;
                handRightGO.GetComponent<SpriteRenderer>().enabled = false;

                numSegmentRight = 1;
            }
        }
    }
    void FrotationSegmentLeft(int numSegment)
    {

        if(handLeftGO!=null & handRightGO!=null)
        {
        if(numSegment == 0)
        {
        ///GO rotation of Hand
            lookSegmentLeft = elbowLeftGO.transform.position - handLeftGO.transform.position;    
        }
        if(numSegment == 1)
        {
        ///GO rotation of Elbow
            lookSegmentLeft = shoulderLeftGO.transform.position - elbowLeftGO.transform.position; 
        }
   
        float angleL = Mathf.Atan2(lookSegmentLeft.x,lookSegmentLeft.y) * Mathf.Rad2Deg;
        segmentEfectorGOLeft[numSegmentLeft].transform.rotation = Quaternion.Euler(0,0,180 - angleL);

        /*Vector3 lookHandRight = elbowRightGO.transform.position - handRightGO.transform.position;
        float angleR = Mathf.Atan2(lookHandRight.y,lookHandRight.x) * Mathf.Rad2Deg;
        handRightGO.transform.rotation = Quaternion.Euler(0,0,angleR+90);*/
        ///
        }
    }
    void FrotationSegmentRight(int numSegmentRight)
    {
        if(handLeftGO!=null & handRightGO!=null)
        {
        if(numSegmentRight == 0)
        {
        ///GO rotation of Hand
            lookSegmentRight = elbowRightGO.transform.position - handRightGO.transform.position;    
        }
        if(numSegmentRight == 1)
        {
        ///GO rotation of Elbow
            lookSegmentRight = shoulderRightGO.transform.position - elbowRightGO.transform.position; 
        }
   
        float angleR = Mathf.Atan2(lookSegmentRight.y,lookSegmentRight.x) * Mathf.Rad2Deg;
        segmentEfectorGORight[numSegmentRight].transform.rotation = Quaternion.Euler(0,0,angleR + 90);

        /*Vector3 lookHandRight = elbowRightGO.transform.position - handRightGO.transform.position;
        float angleR = Mathf.Atan2(lookHandRight.y,lookHandRight.x) * Mathf.Rad2Deg;
        handRightGO.transform.rotation = Quaternion.Euler(0,0,angleR+90);*/
        ///
        }
    }
    public void StartPointBody()
    {
        /*startHandLeftT.transform.position = handLeftGO.transform.position;
        startHandRightT.transform.position = handRightGO.transform.position;
        startElbowLeftT.transform.position = elbowLeftGO.transform.position;
        startElbowRightT.transform.position = elbowRightGO.transform.position;
        startSpineShoulderT.transform.position = spineShoulderGO.transform.position;

        long1Left = Vector3.Distance(startSpineShoulderT.transform.position,startElbowLeftT.transform.position);
        long2Left = Vector3.Distance(startElbowLeftT.transform.position,startHandLeftT.transform.position);
        long1Right = Vector3.Distance(startSpineShoulderT.transform.position,startElbowRightT.transform.position);
        long2Right = Vector3.Distance(startElbowRightT.transform.position,startHandRightT.transform.position);*/
    }
    public void ReffPointsStart()
    {
        /*pointHandLeftGO.transform.position = startHandLeftT.transform.position;
        pointTouchHandRight3GO.transform.position = startHandRightT.transform.position;*/
    }
}

