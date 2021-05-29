using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockpicking : MonoBehaviour 
{

	private enum Mode {easy, medium, hard}
	[SerializeField] private float sensitivityLockpick = 50;
	[SerializeField] private float sensitivityKeyhole = 35;
	[SerializeField] private float lockTime = 0.25f;
	[SerializeField] [Range(10, 30)] private int stepAngle = 20;
	[SerializeField] [Range(1, 10)] private int shakePower = 5;
	[SerializeField] private Mode lockMode;
	[SerializeField] [Range(2, 30)] private int easyMode;
	[SerializeField] [Range(2, 30)] public int mediumMode;
	[SerializeField] [Range(2, 30)] private int hardMode;
	[SerializeField] private Transform lockpick;
	[SerializeField] private Transform lockpickPivot;
	[SerializeField] private Transform keyhole;
	[SerializeField] private Transform keyholePivot;
	private float keyholeRotate, lockpickRotate, min, max, stepMin_A, stepMax_A, stepMax_B, stepMin_B, lockLimit, keyholeTime;
	private int targetAngle, offsetAngle;
	private bool isUnlock, stop;
	private Vector3 originalAngles;

	public static RaycastHit ht;

	public bool IsUnlock
	{
		get{ return isUnlock; }
	}

	void Awake()
	{
		lockpick.SetParent(lockpickPivot);
		keyhole.SetParent(keyholePivot);
		originalAngles = lockpickPivot.eulerAngles;
		CalculateAngles();
	}

	void CalculateAngles() // находим углы, определяем допустимые диапазоны
	{
		targetAngle = Random.Range(-90, 90);

		switch(lockMode)
		{
		case Mode.easy:
			offsetAngle = easyMode;
			break;
		case Mode.medium:
			offsetAngle = mediumMode;
			break;
		case Mode.hard:
			offsetAngle = hardMode;
			break;
		}

		if(targetAngle > 0)
		{
			offsetAngle = targetAngle - offsetAngle;
		}
		else
		{
			offsetAngle = targetAngle + offsetAngle;
		}

		min = Mathf.Min(offsetAngle, targetAngle);
		max = Mathf.Max(offsetAngle, targetAngle);

		if(max > 0)
		{
			stepMin_A = min - (stepAngle/2);
			stepMax_A = min;
			stepMin_B = stepMin_A - stepAngle;
			stepMax_B = stepMin_A;
		}
		else
		{
			stepMax_A = max + (stepAngle/2);
			stepMin_A = max;
			stepMax_B = stepMax_A + stepAngle;
			stepMin_B = stepMax_A;
		}
	}

	void ShakeLockpick() // дрожание отмычки
	{
		if(isUnlock) return;
		stop = true;
		Vector3 rnd = Random.insideUnitSphere * shakePower;
		lockpickPivot.eulerAngles = originalAngles + new Vector3(rnd.x, rnd.y, lockpickPivot.eulerAngles.z);
	}

	void ShakeLockpickReset()
	{
		stop = false;
		lockpickPivot.eulerAngles = new Vector3(originalAngles.x, originalAngles.y, lockpickPivot.eulerAngles.z);
	}

	void KeyholeControl() // контроль вращения замка
	{
		if(CheckRange() && Input.GetAxis("Horizontal") < 0)
		{
			if(!stop) keyholeRotate += sensitivityKeyhole * Time.deltaTime;

			if(keyholeRotate >= 90)
			{
				stop = true;
				isUnlock = true;
			}
			else if(keyholeRotate >= lockLimit)
			{
				ShakeLockpick();
			}
		}
		else if(!CheckRange() && Input.GetAxis("Horizontal") < 0)
		{
			keyholeTime += Time.deltaTime;

			if(keyholeTime < lockTime)
			{
				keyholeRotate += sensitivityKeyhole * Time.deltaTime;
			}

			ShakeLockpick();
		}
		else
		{
			keyholeTime = 0;
			lockLimit = 90;
			ShakeLockpickReset();
			keyholeRotate -= sensitivityKeyhole * Time.deltaTime;
		}

		keyholeRotate = Mathf.Clamp(keyholeRotate, 0, lockLimit);
		keyholePivot.eulerAngles = new Vector3(keyholePivot.eulerAngles.x, keyholePivot.eulerAngles.y, -keyholeRotate);
	}

	void LockpickControl() // управление отмычкой
	{
		if(Input.GetAxis("Mouse X") < 0)
		{
			lockpickRotate += sensitivityLockpick * Time.deltaTime;
		}
		else if(Input.GetAxis("Mouse X") > 0)
		{
			lockpickRotate -= sensitivityLockpick * Time.deltaTime;
		}

		lockpickRotate = Mathf.Clamp(lockpickRotate, -90, 90);
		lockpickPivot.eulerAngles = new Vector3(lockpickPivot.eulerAngles.x, lockpickPivot.eulerAngles.y, lockpickRotate);
	}

	bool CheckRange()
	{
		if(stop) return false;

		if(lockpickRotate < stepMax_B && lockpickRotate > stepMin_B)
		{
			lockLimit = Mathf.Abs(stepMin_B);
			return true;
		}
		else if(lockpickRotate < stepMax_A && lockpickRotate > stepMin_A)
		{
			lockLimit = Mathf.Abs(stepMin_A);
			return true;
		}
		else if(lockpickRotate < max && lockpickRotate > min)
		{
			lockLimit = 90;
			return true;
		}

		return false;
	}

	void LateUpdate()
	{
		if(isUnlock) // если замок открыт, выключаем скрипт
		{
			FirstPersonMovement.openDoor = false;
			this.gameObject.SetActive(false);
			ht.transform.GetComponent<Door_Open>().enabled = true;
			ht.transform.GetComponent<Door_Open>().AudioPlay();
			ht.transform.GetComponent<Door_Open>().Invert(transform);
			ht.transform.GetComponent<Door_Open>().isKey = true;
			enabled = false;
			isUnlock = false;
			lockpick.SetParent(lockpickPivot);
			keyhole.SetParent(keyholePivot);
			originalAngles = lockpickPivot.eulerAngles;
			CalculateAngles();
		}

		KeyholeControl();
		LockpickControl();
	}
}