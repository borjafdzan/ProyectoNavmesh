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
        
        agenteNavegacion.destination = Jugador.transform.position;
        this.renderizador.material.SetColor("__Color", colorActivo);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] JugadoresEnObjetivo = Physics.SphereCastAll(this.transform.position, 10, this.transform.forward, 10, mascaraJugador);
            if (JugadoresEnObjetivo.Length > 0)
            {
                this.agenteNavegacion.destination = Jugador.transform.position;
            }
            else
            {
                this.agenteNavegacion.destination = Casa.position;
                this.renderizador.material.SetColor("__Color", colorDesactivo);
            }
    }

    
}
