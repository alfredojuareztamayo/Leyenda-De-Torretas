using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerPerceptionAndDecision : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> enemiesInside = new List<GameObject>();
    public float perceptionRadius = 5f; // Radio de percepci�n de la torre
    public Transform target; // El transform del objetivo seleccionado
    DisparoTower disparoTower;

    private void OnDrawGizmosSelected()
    {
        // Dibuja un Gizmos en el editor para visualizar el radio de percepci�n
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, perceptionRadius);
    }
    private void Start()
    {
        disparoTower = GetComponent<DisparoTower>();
    }

    private void Update()
    {
        PerceptionManager();
        DesicionManager();
    }
    private void PerceptionManager()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, perceptionRadius);

        // Crear una lista temporal para los enemigos que deben ser eliminados
        List<GameObject> enemiesToRemove = new List<GameObject>();
        foreach (var enemyInside in enemiesInside)
        {
            // Comprobar si el enemigo todav�a est� dentro del radio de percepci�n
            bool isStillInside = false;

            foreach (var hitCollider in hitColliders)
            {
                if (enemyInside == hitCollider.gameObject)
                {
                    isStillInside = true;
                    break;
                }
            }

            if (!isStillInside)
            {
                enemiesToRemove.Add(enemyInside);
                Debug.Log("Enemy exited the tower's perception range: " + enemyInside.name);

                // Si el enemigo que sali� era el objetivo, establecer el objetivo en null
                if (target == enemyInside.transform)
                {
                    target = null;
                }
            }
        }

        // Eliminar los enemigos que deben ser removidos de la lista
        foreach (var enemyToRemove in enemiesToRemove)
        {
            enemiesInside.Remove(enemyToRemove);
        }

        // Detectar nuevos enemigos que entran en el radio de percepci�n
        foreach (var hitCollider in hitColliders)
        {
            if (target == null)
            {
                if (hitCollider.CompareTag("EnemyAl") && !enemiesInside.Contains(hitCollider.gameObject))
                {
                    enemiesInside.Add(hitCollider.gameObject);
                    Debug.Log("Enemy entered the tower's perception range: " + hitCollider.gameObject.name);

                    // Cambiar el objetivo seleccionado al enemigo detectado
                    target = hitCollider.transform;
                    
                }
            }
            
        }
    }
    private void DesicionManager()
    {
        if(target == null)
        {
            return;
        }
        disparoTower.ShootArrow();
    }
}
