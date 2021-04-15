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

    void Reset()
    {
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        timerEnd = DateTime.Now.AddSeconds(Game_Manager.time);
        TimeSpan delta = timerEnd - DateTime.Now;
        time_txt.text = delta.Minutes.ToString("00") + ":" + delta.Seconds.ToString("00");
    }
     public static void  TimerStop()
    {
         timerEnd = DateTime.Now.AddSeconds(Game_Manager.time);
    }
    void Update()
    {
        if (!FirstPersonMovement.pause)
        {
            // Get smooth mouse look.
            Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
            appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
            currentMouseLook += appliedMouseDelta;
            currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -70, 90); // ограничение по повороту камеры

            // Rotate camera and controller.
            transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);

            //Вывод времени
            TimeSpan delta = timerEnd - DateTime.Now;
            time_txt.text = delta.Minutes.ToString("00") + ":" + delta.Seconds.ToString("00");
        }
    }
}
