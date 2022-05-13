using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public GameObject Jugador;
    public Transform Casa;
    public LayerMask mascaraJugador;
    private Color colorActivo = Color.red;
    private Color colorDesactivo = Color.green;
    public Renderer renderizador;
    NavMeshAgent agenteNavegacion;

    Collider colisionador;
    // Start is called before the first frame update
    void Start()
    {
        this.agenteNavegacion = GetComponent<NavMeshAgent>();
        StartCoroutine(DetectarJugador());
        agenteNavegacion.destination = Jugador.transform.position;
        this.renderizador.material.SetColor("__Color", colorActivo);
    }

    // Update is called once per frame
    void Update()
    {
        //agenteNavegacion.destination = Jugador.transform.position;
    }

    IEnumerator DetectarJugador()
    {
        while (true)
        {
            float distanciaJugador = 0;
            RaycastHit golpeRayo;
            RaycastHit[] JugadoresEnObjetivo = Physics.SphereCastAll(this.transform.position, 10, this.transform.forward, 10, mascaraJugador);
            if (JugadoresEnObjetivo.Length > 0)
            {
                Debug.Log("Se detecta el jugador");
                
                float angulo = ComprobarRango(JugadoresEnObjetivo[0].transform.gameObject);
                if (angulo < 40){
                    this.agenteNavegacion.destination = Jugador.transform.position;
                }
                this.renderizador.material.SetColor("__Color", colorActivo);
            }
            else
            {
                this.agenteNavegacion.destination = Casa.position;
                this.renderizador.material.SetColor("__Color", colorDesactivo);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    //Este metodo deuelve el angulo de deteccion
    private float  ComprobarRango(GameObject objetivoDetectado){
        Vector3 direccion = this.transform.position - objetivoDetectado.transform.position;
        direccion = direccion.normalized;
        direccion = - direccion;
        Debug.DrawLine(this.transform.position, objetivoDetectado.transform.position, Color.cyan, 1);
        float angulo = Vector3.Angle(this.transform.forward, direccion);
        Debug.Log("El angulo actual es de " + angulo);
        return angulo;
        //RaycastHit rayo = Physics.Raycast(this.transform.position, direccion, 10, mascaraJugador):
    }
}
