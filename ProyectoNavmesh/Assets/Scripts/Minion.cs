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
        StartCoroutine(CorrutinaSeguir());
    }

    // Update is called once per frame
    void Update()
    {
        if (agenteNavegacion.isOnOffMeshLink){
            OffMeshLinkData link = agenteNavegacion.currentOffMeshLinkData;
            transform.position = link.endPos;
        }
    }

    IEnumerator CorrutinaSeguir()
    {
        while (true)
        {
            this.agenteNavegacion.destination = this.Jugador.transform.position;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
