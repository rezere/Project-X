using UnityEngine;
using System;
using UnityEngine.UI;
public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    public float sensitivity = 1;
    public float smoothing = 2;
    public Text time_txt;
    public static DateTime timerEnd;
    [Range(10, 100)] public float distance;
    private Camera cam;
    public RaycastHit hit;
    public GameObject keyhole;
    public Transform camer;


    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Awake()
    {
        cam = GetComponent<Camera>();
        camer = cam.GetComponent<Transform>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        timerEnd = DateTime.Now.AddSeconds(Game_Manager.time);
        TimeSpan delta = timerEnd - DateTime.Now;
        time_txt.text = delta.Minutes.ToString("00") + ":" + delta.Seconds.ToString("00");
        keyhole.GetComponent<Lockpicking>().enabled = false;
    }

    public static void  TimerStop()
    {
         timerEnd = DateTime.Now.AddSeconds(Game_Manager.time);
    }

    void Update()
    {
        if (!FirstPersonMovement.pause && !FirstPersonMovement.openDoor)
        {
            // Get smooth mouse look.
            Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
            appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
            currentMouseLook += appliedMouseDelta;
            currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -70, 90); // ограничение по повороту камеры

            // Rotate camera and controller.
            transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
        }

        if(!FirstPersonMovement.pause)
        {
            //Вывод времени
            TimeSpan delta = timerEnd - DateTime.Now;
            time_txt.text = delta.Minutes.ToString("00") + ":" + delta.Seconds.ToString("00");
            if(Input.GetKeyDown(KeyCode.E))
            {
                
                Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                if (Physics.Raycast(ray, out hit, distance))
			{
				if(hit.collider.tag == "light")
				{
                    bool act =  hit.collider.gameObject.GetComponent<Light_House>().active;
                    hit.collider.gameObject.GetComponent<Light_House>().active = !act;
                    hit.collider.gameObject.GetComponent<Light_House>().LightOff();
				}

                if(hit.collider.tag == "Items")
				{
                    Game_Manager.ItemsUp();
                     hit.collider.gameObject.GetComponent<Items_Spawn>().DestroyItems();
				}
                if(hit.collider.tag == "Door")
				{
                    if(!hit.transform.GetComponent<Door_Open>().isKey)
                    {
                        FirstPersonMovement.openDoor = true;
                        keyhole.SetActive(true);
                        //hit.transform.GetComponent<Door_Open>().isKey = true;
                        keyhole.GetComponent<Lockpicking>().enabled = true;
                        Lockpicking.ht = hit;
                        Debug.Log("EZ");
                    }
                    else if(hit.transform.GetComponent<Door_Open>().isKey)
                    {
                        hit.transform.GetComponent<Door_Open>().enabled = true;
					    hit.transform.GetComponent<Door_Open>().Invert(transform);
                    }
				}
                if(hit.collider.tag == "TV")
                {
                    bool onTv =  hit.collider.gameObject.GetComponent<TV>().OnTv;
                    hit.collider.gameObject.GetComponent<TV>().OnTv = !onTv;
                    hit.collider.gameObject.GetComponent<TV>().WatchTV();
                }

                
			}
            }

        }
    }
}
