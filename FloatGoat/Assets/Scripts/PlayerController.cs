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

    public float elapsedTime;
    public float score;

    // Alexi's vars
    public float Fuel { get { return fuel; } }
    public float FuelMax { get { return fuelMax; } }
    GoatAudioMgr noise;

    // Use this for initialization
    void Start()
    {
        fuel = 75;
        fuelMax = 100;
        accelX = 0;
        accelY = 0;
        speedX = 0;
        speedY = 0;

        elapsedTime = 0f;
        score = 0f;
        noise = GetComponent<GoatAudioMgr>();
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

        elapsedTime += Time.deltaTime;
        score += Time.deltaTime;
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
            GameManager.Die(); // freeze frame w/ instant replay
        }

        WallObject isWallObj = other.GetComponent<WallObject>();
        if(null != isWallObj) { isWallObj.ApplyToPlayer(this); }
    }

    public void HitFuel(float damage)
    {
        // particle effect (?)
        // sound
        // update ui

        fuel += damage;
        if(fuel <= 0) { Debug.Log("RIP"); }
        fuel = Mathf.Clamp(fuel, 0, fuelMax);

        if(damage >= 0) { noise.PowerupSound(); }
        else { noise.DamageSound(); }
    }
}
