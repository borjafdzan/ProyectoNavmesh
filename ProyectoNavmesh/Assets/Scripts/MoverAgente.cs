using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MoverAgente : MonoBehaviour
{
    public Transform[] posicionesMoverse;
    public bool espera = true;
    public float tiempoEspera = 0.5f;
    private NavMeshAgent agente;
    private int indicePatrulla;
    // Start is called before the first frame update
    void Start()
    {
        this.agente = GetComponent<NavMeshAgent>();
        if (posicionesMoverse.Length == 0)
        {
            Debug.Log("No se han metido las posiciones correctamente");
        }
        else
        {
            this.agente.destination = posicionesMoverse[0].position;
            this.indicePatrulla = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (AlcanzoDestino())
        {
            SiguientePosicion();
        }
    }
    private bool AlcanzoDestino()
    {
        if (Vector3.Distance(this.transform.position, posicionesMoverse[indicePatrulla].position) < 0.2f)
        {
            Debug.Log("Llego al destino");
            return true;
        }
        return false;
    }
    private void SiguientePosicion()
    {
        this.indicePatrulla++;
        if (indicePatrulla >= this.posicionesMoverse.Length)
        {
            this.indicePatrulla = 0;
        }
        if (espera)
        {
            Invoke("DejarEsperar", tiempoEspera);
        }
        else
        {
            this.agente.destination = posicionesMoverse[indicePatrulla].position;
        }
    }

    private void DejarEsperar()
    {
        this.agente.destination = posicionesMoverse[indicePatrulla].position;
    }
}
