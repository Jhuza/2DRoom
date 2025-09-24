using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro; //libreria para usar textos

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; //variable para guardar la velocidad
    public int score = 0; //variable para guardar la puntuación
    public bool hasKey = false; //variable para guardar si tiene la llave
    public bool hasWater = false; //variable para guardar si tiene el agua
    public TextMeshProUGUI scoreText; //variable para el texto de la puntuación
    public TextMeshProUGUI notificationText; //variable para el texto de notificaciones


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTextScore();


        notificationText.text = "Collect 7 items to win!";
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
            UpdateTextScore();

            Destroy(other.gameObject); //destruir el objeto con el que colisionamos
            Debug.Log("Objeto recogido");
            Debug.Log("Score:  "+ score);
            if(score >= 7)
                Debug.Log("Recoje la llave!");
            notificationText.text = "Score: " + score + "/7";

        } 
        else
        {
            Debug.Log("Sigue intentando, necesitas 7 para ganar");

            notificationText.text = "Score: " + score + "/7";

        }
        if ( other.CompareTag("Key"))
        {
            hasKey = true; //el jugador tiene la llave
            Debug.Log("Has recogido la llave, ya puedes abrir la puerta");
            Destroy(other.gameObject); //destruir el objeto con el que colisionamos

            notificationText.text = "You have the key! Now go to the door!";

        }
        if (other.CompareTag("Water"))
        {
            hasWater = true; 
            Debug.Log("Has tocado el agua y perdiste");
            Destroy(gameObject); 

            notificationText.text = "You touched the water and lost!";

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

            notificationText.text = "You need 7 items and the key to win!";
        }

    }
    void UpdateTextScore()
    {
        scoreText.text = "Score: " + score;
    }
}
