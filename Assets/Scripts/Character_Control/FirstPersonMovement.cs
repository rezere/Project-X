using UnityEngine;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    [Range(1, 10)] public float speed;
    public GameObject paus; // Меню паузы
    private Animator anim;
    Vector2 velocity;
    public bool kiss = false;
    public static bool pause = false;

    public static bool openDoor;
    [Header("Фонарик")]
    public  static float battery;
    public GameObject ligh;
    public Light lantern;
    private bool ligt_on;
    public Image img_battery;
    public Sprite[] battery_pr;
    
    [Header("Предметы для поиска")]

    public  Image[] items_search;
    public Sprite[] items_img;
    public GameObject menu_items;
    private bool ismenu;
    private Rigidbody rb;
 
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        openDoor = false;
        ismenu = false;
        battery = 100f;
        ligt_on = false;
        anim = GetComponent<Animator>();
        anim.SetBool("walk", false);
        lantern.range = 45f;
        lantern.spotAngle = 60f;
        for(int i = 0; i<Game_Manager.items_spawn.Length;i++)
        {
            items_search[i].sprite = items_img[Game_Manager.items_spawn[i]];
            items_search[i].enabled = true;
        }
        for(int i = Game_Manager.items_spawn.Length; i<items_search.Length;i++)
        {
            items_search[i].enabled = false;
        }
        Debug.Log(Game_Manager.items_spawn.Length);
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

        if (Input.GetKeyUp(KeyCode.T) && !ismenu) // открытие меню предметов
        {
            ismenu = true;
            anim.SetBool("walk", false);
            Cursor.lockState = CursorLockMode.Confined;
            menu_items.SetActive(ismenu);
        }
        else if (Input.GetKeyUp(KeyCode.T) && ismenu) // Закрытие меню предметов
        {
            ismenu = false;
            Cursor.lockState = CursorLockMode.Locked;
            FirstPersonLook.TimerStop();
            menu_items.SetActive(ismenu);
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
            if(!openDoor)
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
}
