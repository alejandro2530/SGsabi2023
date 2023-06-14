using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int PositivePoint, NegativePoint;
    public static float TiempoJuego, AuxTiempoJuego;
    public List<float> TiempoRespList;
    public List<float> TiempoRespList2;
    [SerializeField] private GameObject _panelResp;
    [SerializeField] private int _cantidadCartas;
    [SerializeField] private int _contador;
    public TextMeshProUGUI PpointTxt, NpointTxt, TimeTotalTxt, ContadorCartasTxt;
    public TextMeshProUGUI ResTimeTotalTxt, ResPnTxt, ResPpTxt, ResTimeMeanRespTxt;
    public GameObject[] cartas;
    public GameObject contenedorCartasGO;
    public GameObject imagenEquipo;
    public GameObject cartaActual;
    public GameObject cartaPosterior;
    public Text imagenEquipoText;
    public Animator imagenEquipoAnim;
    public Transform RefCartas;
    public static bool onGame;
    private int j;
    private int auxEq;
    private float _suma, _media;
    void Awake()
    {
        auxEq = 0;
    }
    void Start()
    {
        _panelResp.SetActive(false);
        onGame = true;
        EncuentroCartas();
    }
    void Update()
    {
        if(onGame == true)
        {
            TiempoJuego += Time.deltaTime;
            AuxTiempoJuego += Time.deltaTime;
            TimeTotalTxt.text = TiempoJuego.ToString("f0") + " seg";
            ApareceCarta();
        }
    }
    public void EncuentroCartas()
    {
        cartas = GameObject.FindGameObjectsWithTag("cartas");
        _cantidadCartas = cartas.Length;
        Debug.Log(_cantidadCartas);
        // crear el array de cartas con un orden aleatorio
        for(int i=0 ; i<cartas.Length ; ++i)
        {
            int pos = Random.Range(i,cartas.Length);
            GameObject temp = cartas[pos];
            cartas[pos]=cartas[i]; // linea para que no se repitan las cartas
            cartas[i]=temp;
            cartas[i].SetActive(false);
        }
        ApareceCarta();
        _contador = 1;
    }
    public void ApareceCarta()
    {
        cartaActual = cartas[j];
        cartaActual.SetActive(true);
        cartaActual.transform.position = RefCartas.transform.position;
    }
    public void CambioCartaAcertada()
    {
        if(_contador <= _cantidadCartas-1){
            _contador++; // lleva el control del número de cartas jugadas
            PositivePoint++;
            TiempoRespList.Add(AuxTiempoJuego);
            AuxTiempoJuego = 0;
            PpointTxt.text = PositivePoint.ToString();
            cartaActual.SetActive(false);
            cartaPosterior = cartas[j+1];
            cartaPosterior.SetActive(true);
            cartaPosterior = cartaActual;
            ++j;
        } else{
            PositivePoint++;
            PpointTxt.text = PositivePoint.ToString();
            CartasAgotadas();
        }
    }
    public void CambioCartaErrada()
    {
        if(_contador <= _cantidadCartas-1){
            _contador++; // lleva el control del número de cartas jugadas
            NegativePoint++;
            TiempoRespList.Add(AuxTiempoJuego);
            AuxTiempoJuego = 0;
            NpointTxt.text = NegativePoint.ToString();
            cartaActual.SetActive(false);
            cartaPosterior = cartas[j+1];
            cartaPosterior.SetActive(true);
            cartaPosterior = cartaActual;
            ++j;
        } else {
            NegativePoint++;
            NpointTxt.text = NegativePoint.ToString();
            CartasAgotadas();
        }
    }
    public void CartasAgotadas(){
        onGame  = false;
        _panelResp.SetActive(true);
        MeantimeResp();
        TextResult();
    }
    private void MeantimeResp(){
        
        foreach (int valor in TiempoRespList)
        {
            _suma += valor;
        }
        if (TiempoRespList.Count > 0)
        {
            _media = (float)_suma / TiempoRespList.Count;
        }
    }
    private void TextResult(){
        ResTimeMeanRespTxt.text = _media.ToString("f2")+" seg";
        ResTimeTotalTxt.text = TiempoJuego.ToString("f0")+" seg";
        ResPpTxt.text = PositivePoint.ToString();
        ResPnTxt.text = NegativePoint.ToString();
        ContadorCartasTxt.text = "Cartas totales: "+_contador.ToString();
    }
}
