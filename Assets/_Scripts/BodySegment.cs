using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySegment : MonoBehaviour
{
    [SerializeField] private GameManager _gameManagerScript;
    [SerializeField] private bool onCambio;
    private void Start() {
        onCambio = true;
    }
    void OnTriggerEnter(Collider other)
    {  
        if(other.gameObject.CompareTag("rCorrecta") && this.CompareTag("hLeft") && onCambio == true && GameManager.onGame == true)
        {
            Debug.Log("Correcta");
            StartCoroutine("PositiveIE");
        }
        else if(other.gameObject.CompareTag("rIncorrecta") && this.CompareTag("hLeft") && onCambio == true && GameManager.onGame == true)
        {
            Debug.Log("Incorrecta");
            StartCoroutine("NegativeIE");
        }
        if(other.gameObject.CompareTag("rCorrecta") && this.CompareTag("hRight") && onCambio == true && GameManager.onGame == true)
        {
            Debug.Log("Correcta");
            StartCoroutine("PositiveIE");
        }
        else if(other.gameObject.CompareTag("rIncorrecta") && this.CompareTag("hRight") && onCambio == true && GameManager.onGame == true)
        {
            Debug.Log("Incorrecta");
            StartCoroutine("NegativeIE");
        }
    }
    public IEnumerator PositiveIE(){
        Debug.Log("entró Corrutina");
        Correcta();
        onCambio = false;
        yield return new WaitForSeconds(1f);
        onCambio = true;
    }
    public IEnumerator NegativeIE(){
        Debug.Log("entró Corrutina");
        Incorrecta();
        onCambio = false;
        yield return new WaitForSeconds(1f);
        onCambio = true;
    }
    private void Correcta(){
        _gameManagerScript.CambioCartaAcertada();
    }
    private void Incorrecta(){
        _gameManagerScript.CambioCartaErrada();
    }
}
