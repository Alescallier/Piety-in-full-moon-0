using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPersonaje : MonoBehaviour
{

    // correr 
    public int velCorrer = 8;

    public float velocidadMovimiento = 5.0f;
    public float velocidadInicial = 5.0f;
    public float velocidadRotacion = 200.0f;
    private Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float fuerzaSalto = 8f;
    public bool puedoSaltar;


    // Start is called before the first frame update
    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();

    }

    void FixedUpdate()
    {

        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);

    }

    // Update is called once per frame

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);


        if (Input.GetKey(KeyCode.LeftShift) && puedoSaltar)
        {
            velocidadMovimiento = velCorrer;
            if (y > 0)
            {
                anim.SetBool("correr", true);
            }
            else
            {
                anim.SetBool("correr", false);
            }
        }
        else
        {
            anim.SetBool("correr", false);
            if (puedoSaltar)
            {
                velocidadMovimiento = velocidadInicial;
            }
        }

        if (puedoSaltar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("Salte", true);
                //rb.AddForce(transform.up * fuerzaSalto);
                rb.AddForce(new Vector3(0, fuerzaSalto, 0), ForceMode.Impulse);
            }
            else // este agregue
            {
                anim.SetBool("Salte", false); // este agregue

            }
            anim.SetBool("TocaSuelo", true);

        }
        else
        {
            EstoyCayendo();

        }

    }

    public void EstoyCayendo()
    {

        anim.SetBool("TocaSuelo", false);
        anim.SetBool("Salte", false);
    }
}
