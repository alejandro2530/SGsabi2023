using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private Transform _handLeft;
    [SerializeField] private Transform _handRight;
    [SerializeField] private Transform _refHandLeft;
    [SerializeField] private Transform _refHandRight;
    private GameObject _handLKnectGO;
    private GameObject _handRKnectGO;
    private bool _isActive;
    [SerializeField] private float _scaleMoveHand;
    [SerializeField] private float _smoothness;
    private void Update() {
            if(GameObject.Find("HandLeft") != null && _isActive == true)
            {
                _handLKnectGO = GameObject.Find("HandLeft");
                _handRKnectGO = GameObject.Find("HandRight");
                _isActive = false;
            } else if(GameObject.Find("HandLeft") == null) {
                _isActive = true;
            }
            if(_handLKnectGO != null)
            {
                //_handLeft.position = new Vector3(_handLKnectGO.transform.position.x*_scaleMoveHand,_handLKnectGO.transform.position.y*_scaleMoveHand,_handLKnectGO.transform.position.z -25);
                //_handRight.position = new Vector3(_handRKnectGO.transform.position.x*_scaleMoveHand,_handRKnectGO.transform.position.y*_scaleMoveHand,_handRKnectGO.transform.position.z -25);
                
                _handLeft.position = Vector3.Lerp(_handLeft.position,new Vector3(_handLKnectGO.transform.position.x*_scaleMoveHand,_handLKnectGO.transform.position.y*_scaleMoveHand,_handLKnectGO.transform.position.z -25),_smoothness*Time.deltaTime);
                _handRight.position = Vector3.Lerp(_handRight.position,new Vector3(_handRKnectGO.transform.position.x*_scaleMoveHand,_handRKnectGO.transform.position.y*_scaleMoveHand,_handRKnectGO.transform.position.z -25),_smoothness*Time.deltaTime);
            }
    }
}
