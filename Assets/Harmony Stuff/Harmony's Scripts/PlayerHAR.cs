using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHAR : MonoBehaviour
{
    public GameObject agent1;
    public GameObject p1spawnPoint;
    public GameObject p2spawnPoint;
    float cd = 1;
    float currentP1TimeRight;
    float currentP1TimeLeft;
    float currentP2TimeRight;
    float currentP2TimeLeft;
    float horizontalMovement;
    float verticalMovement;
    bool player1;
    bool player2;
    float vel = 2;
    float hp = 100;
    int money = 1000;
    int agent1Cost = 50;
    int agent2Cost = 100;
    int agent3Cost = 200;
    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "Player1")
        {
            player1 = true;
            player2 = false;
        }
        if (this.tag == "Player2")
        {
            player2 = true;
            player1 = false;
        }
    }
    void Update()
    {
        MovementCA();
        Die();
        if(player1 == true)
        {
            Player1SpawnAgents();
        }
        if(player2 == true)
        {
            Player2SpawnAgents();
        }
    }

    private void MovementCA()
    {
        if (player1 == true)
        {
            // Obtener las entradas de teclado para las teclas WASD
            horizontalMovement = Input.GetAxis("Horizontal");
            verticalMovement = Input.GetAxis("Vertical");
            // Calcula el vector de movimiento
            Vector3 movimiento = new Vector3(horizontalMovement, 0.0f, verticalMovement) * vel * Time.deltaTime;
            // Aplica el movimiento al transform del personaje
            transform.Translate(movimiento);
        }
        if (player2 == true)
        {
            // Obtener las entradas de teclado para las teclas WASD
            horizontalMovement = Input.GetAxis("Horizontal2");
            verticalMovement = Input.GetAxis("Vertical2");
            // Calcula el vector de movimiento
            Vector3 movimiento = new Vector3(horizontalMovement, 0.0f, verticalMovement) * vel * Time.deltaTime;
            // Aplica el movimiento al transform del personaje
            transform.Translate(movimiento);
        }
    }

    private void Player1SpawnAgents()
    {
        //PLAYER 1 LOGIC
        currentP1TimeLeft += Time.deltaTime;
        currentP1TimeRight += Time.deltaTime;

        //SPAWNING AGENT 1

        //Spawn on P1's left side.
        if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.Z))
        {
            if (currentP1TimeLeft > cd && money > agent1Cost)
            {
                GameObject agent = Instantiate(agent1, p1spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.SpawnLeftSideP1();
                currentP1TimeLeft = 0;
                money -= agent1Cost;
            }
        }

        //SPAWN on P1's right side.
        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.Z))
        {
            if (currentP1TimeRight > cd && money > agent1Cost)
            {
                GameObject agent = Instantiate(agent1, p1spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.SpawnRightSideP1();
                currentP1TimeRight = 0;
                money -= agent1Cost;
            }
        }
    }

    private void Player2SpawnAgents()
    {
        //PLAYER 2 LOGIC

        currentP2TimeLeft += Time.deltaTime;
        currentP2TimeRight += Time.deltaTime;

        //SPAWNING AGENT 1.

        //Spawn on P2's right side.
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (currentP2TimeRight > cd && money > agent1Cost)
            {
                GameObject agent = Instantiate(agent1, p2spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.SpawnRightSideP2();
                currentP2TimeRight = 0;
                money -= agent1Cost;
            }
        }

        //Spawn on P1's left side.
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (currentP2TimeLeft > cd && money > agent1Cost)
            {
                GameObject agent = Instantiate(agent1, p2spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.SpawnLeftSideP2();
                currentP2TimeLeft = 0;
                money -= agent1Cost;
            }
        }
    }

    void TakeDamage(int dmg)
    {
        hp -= dmg;
    }

    void Die()
    {
        if (hp <= 0)
        {
            Destroy(gameObject, 1.5f);
        }
    }

    public int GetPlayerMoney()
    {
        return money;
    }
}
