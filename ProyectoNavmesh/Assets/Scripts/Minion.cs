using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public GameObject Jugador;
    NavMeshAgent agenteNavegacion;
    // Start is called before the first frame update
    void Start()
    {
        this.agenteNavegacion = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        this.agenteNavegacion.destination = this.Jugador.transform.position;
    }
}
