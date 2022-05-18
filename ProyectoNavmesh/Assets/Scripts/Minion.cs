using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum EstadoMinion
{
    Idle,
    Perseguir
}
public class Minion : MonoBehaviour
{
    public GameObject Jugador;
    public Transform Casa;
    public LayerMask mascaraJugador;
    public float anguloLimite = 40;
    private Color colorActivo = Color.red;
    private Color colorDesactivo = Color.green;
    private EstadoMinion estadoActual;
    public Renderer renderizador;
    NavMeshAgent agenteNavegacion;

    Collider colisionador;
    // Start is called before the first frame update
    void Start()
    {
        this.agenteNavegacion = GetComponent<NavMeshAgent>();

        agenteNavegacion.destination = Jugador.transform.position;
        this.renderizador.material.SetColor("__Color", colorActivo);
        ComprobarEstado();
    }

    // Update is called once per frame
    void Update()
    {
        ComprobarEstado();
        if (estadoActual == EstadoMinion.Idle)
        {
            this.agenteNavegacion.destination = Casa.position;
        }
        else if (estadoActual == EstadoMinion.Perseguir)
        {
            this.agenteNavegacion.destination = Jugador.transform.position;
        }
    }

    private void ComprobarEstado()
    {
        RaycastHit[] JugadoresEnObjetivo = Physics.SphereCastAll(this.transform.position, 10, this.transform.forward, 10, mascaraJugador);
        if (JugadoresEnObjetivo.Length > 0)
        {
            if (EstaEnVision(JugadoresEnObjetivo[0].transform.gameObject))
            {
                estadoActual = EstadoMinion.Perseguir;
                return;
            }
        }
        else
        {
            estadoActual = EstadoMinion.Idle;
            return;
        }
        estadoActual = EstadoMinion.Idle;
    }
    //Este metodo comprueba si esta en vision
    private bool EstaEnVision(GameObject objetivoDetectado)
    {
        Vector3 direccion = this.transform.position - objetivoDetectado.transform.position;
        direccion = direccion.normalized;
        direccion = -direccion;
        //Subimos la posicion del raycast
        RaycastHit[] objetivos = Physics.RaycastAll(this.transform.position + Vector3.up, direccion, 10, -1);
        Debug.Log("Se hizo el racast se encontraron" + objetivos.Length);
        //En el caso de que el primer objeto que vea el raycast del minion sea el jugador
        //devolvemos verdadero si el raycast coje otro es falso;
        foreach (RaycastHit golpe in objetivos)
        {
            Debug.Log(golpe.transform.gameObject.tag);

            if (golpe.transform.gameObject.tag == "Player" && EstaEnRango(golpe.transform.gameObject))
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private bool EstaEnRango(GameObject objetivoDetectado)
    {
        Vector3 direccion = this.transform.position - objetivoDetectado.transform.position;
        direccion = direccion.normalized;
        direccion = -direccion;
        //Cogemos el angulo entre el vector frente del jugador y el objetivo
        float angulo = Vector3.Angle(this.transform.forward, direccion);
        Debug.Log(angulo);
        if (angulo <= anguloLimite)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
