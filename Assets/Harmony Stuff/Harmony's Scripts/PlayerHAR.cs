using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHAR : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    public GameObject agent1;
    public GameObject agent2;
    public GameObject agent3;
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
    float vel = 8;
    //[SerializeField] Transform cam;
    float hp = 100;
    [SerializeField] int money = 1000;
    int agent1Cost = 50;
    int agent2Cost = 100;
    int agent3Cost = 200;
    float time;
    float timeBeforeMoney = 1;
    public TextMeshProUGUI moneyDisplay;

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
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        moneyDisplay.text = money.ToString();
        time += Time.deltaTime;
        EarnMoney();
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
            Vector3 movimiento = new Vector3(verticalMovement*-1, 0.0f, horizontalMovement);
            
            // Aplica el movimiento al transform del personaje
            transform.Translate(movimiento* vel * Time.deltaTime, Space.World);

            if(movimiento != Vector3.zero)
            {
                transform.forward = movimiento;
            }
            /*
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;

            camForward.y = 0;
            camRight.y = 0;

            Vector3 forwardRelative = verticalMovement * camForward;
            Vector3 rightRelative = horizontalMovement * camRight;

            Vector3 moveDir = forwardRelative + rightRelative;

            rb.velocity = new Vector3(moveDir.x, 0, moveDir.z);
            // Aplica el movimiento al transform del personaje


            if(Input.GetKey(KeyCode.F))
            {
                cam.transform.Translate(new Vector3(-50*Time.deltaTime, 0, 0));
            }
            if(Input.GetKey(KeyCode.G))
            {
                cam.transform.Translate(new Vector3(50*Time.deltaTime, 0, 0));
            }
            cam.transform.LookAt(this.transform);
            */
        }
        if (player2 == true)
        {
            // Obtener las entradas de teclado para las flechas.
            horizontalMovement = Input.GetAxis("Horizontal2");
            verticalMovement = Input.GetAxis("Vertical2");
            // Calcula el vector de movimiento
            Vector3 movimiento = new Vector3(verticalMovement, 0.0f, horizontalMovement*-1);
            
            // Aplica el movimiento al transform del personaje
            transform.Translate(movimiento* vel * Time.deltaTime, Space.World);

            if(movimiento != Vector3.zero)
            {
                transform.forward = movimiento;
            }
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
                agentSc.player1 = true;
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
                agentSc.player1 = true;
                agentSc.SpawnRightSideP1();
                currentP1TimeRight = 0;
                money -= agent1Cost;
            }
        }

        //SPAWNING AGENT 3

        //Spawn on P1's left side.
        if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.C))
        {
            if (currentP1TimeLeft > cd && money > agent1Cost)
            {
                GameObject agent = Instantiate(agent3, p1spawnPoint.transform.position, Quaternion.identity);
                SiegeAgentHAR agentSc = agent.GetComponent<SiegeAgentHAR>();
                agentSc.player1 = true;
                agentSc.SpawnLeftSideP1();
                currentP1TimeLeft = 0;
                money -= agent3Cost;
            }
        }

        //SPAWN on P1's right side.
        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.C))
        {
            if (currentP1TimeRight > cd && money > agent1Cost)
            {
                GameObject agent = Instantiate(agent3, p1spawnPoint.transform.position, Quaternion.identity);
                SiegeAgentHAR agentSc = agent.GetComponent<SiegeAgentHAR>();
                agentSc.player1 = true;
                agentSc.SpawnRightSideP1();
                currentP1TimeRight = 0;
                money -= agent3Cost;
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
        if (Input.GetKeyDown(KeyCode.P) && Input.GetKey(KeyCode.B))
        {
            if (currentP2TimeRight > cd && money > agent1Cost)
            {
                GameObject agent = Instantiate(agent1, p2spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.player2 = true;
                agentSc.SpawnRightSideP2();
                currentP2TimeRight = 0;
                money -= agent1Cost;
            }
        }

        //Spawn on P2's left side.
        if (Input.GetKeyDown(KeyCode.I) && Input.GetKey(KeyCode.B))
        {
            if (currentP2TimeLeft > cd && money > agent1Cost)
            {
                GameObject agent = Instantiate(agent1, p2spawnPoint.transform.position, Quaternion.identity);
                PathFollowerHAR agentSc = agent.GetComponent<PathFollowerHAR>();
                agentSc.player2 = true;
                agentSc.SpawnLeftSideP2();
                currentP2TimeLeft = 0;
                money -= agent1Cost;
            }
        }

        //SPAWNING AGENT 3

        //Spawn on P1's left side.
        if (Input.GetKeyDown(KeyCode.I) && Input.GetKey(KeyCode.M))
        {
            if (currentP2TimeLeft > cd && money > agent1Cost)
            {
                GameObject agent = Instantiate(agent3, p2spawnPoint.transform.position, Quaternion.identity);
                SiegeAgentHAR agentSc = agent.GetComponent<SiegeAgentHAR>();
                agentSc.player2 = true;
                agentSc.SpawnLeftSideP2();
                currentP2TimeLeft = 0;
                money -= agent3Cost;
            }
        }

        //SPAWN on P1's right side.
        if (Input.GetKeyDown(KeyCode.P) && Input.GetKey(KeyCode.M))
        {
            if (currentP2TimeRight > cd && money > agent1Cost)
            {
                GameObject agent = Instantiate(agent3, p2spawnPoint.transform.position, Quaternion.identity);
                SiegeAgentHAR agentSc = agent.GetComponent<SiegeAgentHAR>();
                agentSc.player2 = true;
                agentSc.SpawnRightSideP2();
                currentP2TimeRight = 0;
                money -= agent3Cost;
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

    public void EarnMoney()
    {
        if(time > timeBeforeMoney)
        {
            money += 4;
            time = 0;
        }

    }
}
