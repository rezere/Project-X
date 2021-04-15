using UnityEngine;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    public GameObject paus; // Меню паузы
    private Animator anim;
    Vector2 velocity;
    public bool kiss = false;
    public static bool pause = false;
    public  static float battery;
    public GameObject ligh;
    public Light lantern;
    private bool ligt_on;
    public Image img_battery;
    public Sprite[] battery_pr;
 
    private void Start()
    {
        battery = 100f;
        ligt_on = false;
        anim = GetComponent<Animator>();
        anim.SetBool("walk", false);
        lantern.range = 45f;
        lantern.spotAngle = 60f;
    }

    public void Update()
    {


        if (Input.GetKeyUp(KeyCode.Escape) && !pause) // открытие меню паузы
        {
            pause = true;
            anim.SetBool("walk", false);
            Cursor.lockState = CursorLockMode.Confined;
            //Exit(pause);
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && pause) // Закрытие паузы
        {
            pause = false;
            Cursor.lockState = CursorLockMode.Locked;
            FirstPersonLook.TimerStop();
            //Exit(pause);
        }
        Exit(pause);

        if (Input.GetKeyUp(KeyCode.F) && !ligt_on && battery>0)
        {
            ligh.SetActive(true);
            ligt_on = true;
        }
        else if (Input.GetKeyUp(KeyCode.F) && ligt_on)
        {
            ligh.SetActive(false);
            ligt_on = false;
        }
    }
    public void Exit(bool p)
    {
        paus.SetActive(p);
    }
    void FixedUpdate()
    {
 
        if (!pause)
        {
            velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(velocity.x, 0, velocity.y);
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("walk", true);
            }
            else
            {
                anim.SetBool("walk", false);
            }
            if (Input.GetKey(KeyCode.L)) // пасхалка
            {
                kiss = true;
                anim.SetBool("kiss", kiss);
            }
            if (!kiss)
            {
                anim.SetBool("kiss", kiss);
            }
            ///////////Фонарик
            if(ligt_on && battery>0)
            {
                battery -=0.5f * Time.deltaTime;
                if (battery <= 100 && battery >= 75)
                {
                    img_battery.sprite = battery_pr[0];
                    lantern.range = 45f;
                    lantern.spotAngle = 60f;
                }
                if(battery < 75 && battery>=50)
                {
                    img_battery.sprite = battery_pr[1];
                    lantern.range = 35f;
                    lantern.spotAngle = 50f;
                }
                if (battery < 50 && battery >= 25)
                {
                    img_battery.sprite = battery_pr[2];
                    lantern.range = 25f;
                    lantern.spotAngle = 40f;
                }
                if (battery < 25 && battery >= 1)
                {
                    img_battery.sprite = battery_pr[3];
                    lantern.range = 15f;
                    lantern.spotAngle = 30f;
                }
                if (battery <=0)
                {
                    img_battery.sprite = battery_pr[4];
                    ligh.SetActive(false);
                    ligt_on = false;
                }
            }
        }
    }
}
