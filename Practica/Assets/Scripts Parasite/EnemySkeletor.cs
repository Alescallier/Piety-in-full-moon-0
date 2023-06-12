using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletor : MonoBehaviour
{
    public Transform[] waypoints; // Array con los waypoints para la patrulla
    public float speed = 5.0f; // Velocidad de movimiento del enemigo
    public float rotSpeed = 1.0f; // Velocidad de rotación del enemigo
    public float waypointDistance = 1.0f; // Distancia mínima al waypoint para considerarlo alcanzado
    public float detectionDistance = 5.0f; // Distancia máxima de detección del jugador
    public float attackDistance = 1.0f; // Distancia mínima de ataque al jugador
    public int attackDamage = 10; // Daño que inflige el enemigo al atacar

    private int currentWaypointIndex = 0; // Índice del waypoint actual
    private bool alert = false; // Indica si el enemigo está en estado de alerta
    private Transform player; // Transform del jugador

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Buscamos al jugador por etiqueta
    }

    private void Update()
    {
        if (alert)
        { // Si estamos en estado de alerta, rotamos hacia el jugador
            RotateTowards(player.position);
        }
        else if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < waypointDistance)
        { // Si llegamos al waypoint, buscamos el siguiente
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        { // Si no estamos en alerta ni llegamos al waypoint, avanzamos hacia él
            MoveTowards(waypoints[currentWaypointIndex].position);
        }

        if (Vector3.Distance(transform.position, player.position) < detectionDistance)
        { // Si detectamos al jugador
            alert = true; // Pasamos al estado de alerta
            RotateTowards(player.position); // Rotamos hacia el jugador
            if (Vector3.Distance(transform.position, player.position) < attackDistance)
            { // Si estamos suficientemente cerca para atacar
                Attack(); // Atacamos al jugador
            }
        }
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        // Aquí implementarías la lógica de ataque al jugador
        // Puedes usar la función SendMessage para comunicarte con el script del jugador y aplicarle el daño
        player.SendMessage("RecibirDanio", attackDamage);
    }

}
