using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAgent : MonoBehaviour
{
    private float movimientoHorizontal;
    private float movimientoVertical;
    public float velocidad = 5.0f; // Velocidad de movimiento del personaje
    [SerializeField] private Animator animator;
    public BlowWeapon arrow;

    private void Start()
    {
       
    }
    void Update()
    {
        MovementCA();
        AnimationCA();
    }

    private void MovementCA()
    {
        // Obtener las entradas de teclado para las teclas WASD
         movimientoHorizontal = Input.GetAxis("Horizontal");
         movimientoVertical = Input.GetAxis("Vertical");
        // Calcula el vector de movimiento
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical) * velocidad * Time.deltaTime;
        // Aplica el movimiento al transform del personaje
        transform.Translate(movimiento);
    }
    private void AnimationCA()
    {
        if(movimientoHorizontal != 0 || movimientoVertical != 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isDeath", false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);
            animator.SetBool("isDeath", false);
            arrow.Fire();
            return;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
