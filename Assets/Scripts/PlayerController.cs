using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; //variable para guardar la velocidad
    public int score = 0; //variable para guardar la puntuación
    public bool hasKey = false; //variable para guardar si tiene la llave
    public bool hasWater = false; //variable para guardar si tiene el agua


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //leer las teclas WASD o las flechas
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //creamos un vector para direccion del movimiento
        Vector3 direction = new Vector3(moveHorizontal, moveVertical, 0);

        transform.Translate(direction * speed * Time.deltaTime);

    }

    //Función especial que se ejecuta cuando se toca a otro objeto que tiene un collider en modo trigger
    private void OnTriggerEnter2D(Collider2D other)  
    {
        if(other.CompareTag("Collectable"))
        {
            score = score + 1; //aumentar la puntuación
            

            Destroy(other.gameObject); //destruir el objeto con el que colisionamos
            Debug.Log("Objeto recogido");
            Debug.Log("Score:  "+ score);
            if(score >= 7)
                Debug.Log("Recoje la llave!");

        } 
        else
        {
            Debug.Log("Sigue intentando, necesitas 7 para ganar");

        }
        if ( other.CompareTag("Key"))
        {
            hasKey = true; //el jugador tiene la llave
            Debug.Log("Has recogido la llave, ya puedes abrir la puerta");
            Destroy(other.gameObject); //destruir el objeto con el que colisionamos

        }
        if (other.CompareTag("Water"))
        {
            hasWater = true; 
            Debug.Log("Has tocado el agua y no puedes ganar");
            Destroy(gameObject); 

        }


        //condición para abrir la puerta
        if (score >= 7 && hasKey == true && !hasWater) //doble barrita || significa "o"
            {
            Debug.Log("Has ganado!");
            }
        // v||v = verdadero, v||f = verdadero, f||v = verdadero, f||f = falso
        else
        {
            Debug.Log("No puedes abrir la puerta, te faltan objetos por recoger");
        }

    }
}
