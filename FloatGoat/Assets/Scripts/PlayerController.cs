using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float fuel;
    [SerializeField] float fuelMax;
    public float fuelLossRate;
    public float fuelRiseConsumption;

    [SerializeField] float accelX;
    [SerializeField] float accelY;

    [SerializeField] float speedX;
    public float speedXMax;
    public float speedXMin;

    [SerializeField] float speedY;
    public float speedYMax;
    public float speedYMin;

    public float horizontalAccelMult;
    public float horizontalDragMult;

    public float gravity;
    public float targetPassiveSpeedY;
    public float thrust;

    // Use this for initialization
    void Start()
    {
        fuel = 75;
        fuelMax = 100;
        accelX = 0;
        accelY = 0;
        speedX = 0;
        speedY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            accelX = Input.GetAxis("Horizontal") * horizontalAccelMult;
        }
        else
        {
            accelX = -speedX * horizontalDragMult;
        }

        if (fuel <= 0)
        {
            Debug.Log("Out of fuel");
            if (fuel < 0)
            {
                fuel = 0;
            }
            accelY = -gravity;
        }
        else
        {
            if(Input.GetAxis("Vertical") > 0)
            {
                fuel -= fuelRiseConsumption * Time.deltaTime;
                accelY = -gravity + thrust;
            }
            else if(Input.GetAxis("Vertical") < 0)
            {
                accelY = -gravity;
            }
            else
            {
                fuel -= fuelLossRate * Time.deltaTime;
                accelY = -speedY + targetPassiveSpeedY;
            }
        }
        if(fuel > fuelMax)
        {
            fuel = fuelMax;
        }

        speedX += accelX * Time.deltaTime;
        speedX = Mathf.Clamp(speedX, speedXMin, speedXMax);

        speedY += accelY * Time.deltaTime;
        speedY = Mathf.Clamp(speedY, speedYMin, speedYMax);

        transform.Translate(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fuel")
        {
            fuel += other.GetComponent<Fuel>().fuel;
        }
        if(other.tag == "InstaKill")
        {
            Debug.Log("Rip in kill");
            //Go to game over screen
        }
    }
}
