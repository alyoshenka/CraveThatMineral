using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float fuel;
    [SerializeField] float fuelMax;
    public float fuelLossRate;
    public float fuelRiseConsumption;

    public float accelX;
    public float accelY;

    [SerializeField] float speedX;
    public float speedXMax;
    public float speedXMin;

    [SerializeField] float speedY;
    public float speedYMax;
    public float speedYMin;

    // Use this for initialization
    void Start()
    {
        fuel = 75;
        fuelMax = 100;
        speedX = 0;
        speedY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //accelY = -3;

        if (Input.GetAxis("Horizontal") != 0)
        {
            accelX = Input.GetAxis("Horizontal") * 7;
        }
        else
        {
            accelX = -speedX * 1.5f;
        }

        if(fuel <= 0)
        {
            Debug.Log("Out of fuel");
            fuel = 0;
        }
        else
        {
            fuel -= fuelLossRate * Time.deltaTime;
            if(Input.GetAxis("Vertical") > 0)
            {
                fuel -= fuelRiseConsumption * Time.deltaTime;
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
    }
}
