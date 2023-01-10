using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField][Tooltip("���� �ӵ�")]
	private float forwardSpeed;
    [SerializeField][Tooltip("�¿� �̵� �ִ�ӵ�")]
    private float sidewardMaxSpeed;
	[SerializeField][Tooltip("���ӵ�")]
	private float acceleration = 1f;

	[Header("Reference")]
	[SerializeField] 
	private Slider sidewardSlider;
	[SerializeField]
	private ParticleSystem deathParticle;

	public bool move = false;
    public Vector3 curVelocity;
	public bool isDie = false;

	[Header("Event")]
	public UnityEvent OnDie;

	private void Update()
	{
		if (move)
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (sidewardSlider.value != 0)
				{
					sidewardSlider.value = sidewardSlider.value > 0 ? -1f : 1f;
				}
				else
				{
					sidewardSlider.value = Camera.main.ScreenToViewportPoint(Input.mousePosition).x > 0.5f ? 1f : -1f;
				}
			}
			SidewardVelocity(sidewardSlider.value);
			Move();
		}
	}

	private void SidewardVelocity(float input)
	{
		float addVelocity = 0;
		if (input < 0)
		{
			addVelocity = input;
		}
		else if (input > 0)
		{
			addVelocity = input;
		}
		else
		{
			addVelocity = 0;
		}
		curVelocity = new Vector3(curVelocity.x += addVelocity * Time.deltaTime * acceleration, forwardSpeed, 0);
		curVelocity = new Vector3(Mathf.Clamp(curVelocity.x, -sidewardMaxSpeed, sidewardMaxSpeed), forwardSpeed);
	}

	private void Move()
	{
		transform.Translate(curVelocity * Time.deltaTime);
	}

	public void Die()
	{
		if (!isDie)
		{
			isDie = true;
			move = false;
			curVelocity = Vector3.zero;
			GetComponent<SpriteRenderer>().enabled = false;
			deathParticle.Play();
			OnDie?.Invoke();
		}
	}

	public void Active()
	{
		move = true;
	}

	public void Initialize()
	{
		isDie = false;
		move = false;
		curVelocity = Vector3.zero;
		sidewardSlider.value = 0;
		transform.position = new Vector3(0, -2.5f);
		GetComponent<SpriteRenderer>().enabled = true;
		deathParticle.Clear();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Wall"))
			GameManager.instance?.StopGame();
	}
}
