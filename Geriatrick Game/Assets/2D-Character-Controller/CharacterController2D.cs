using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CharacterController2D : MonoBehaviour
{
    [System.NonSerialized] private float m_JumpForce;
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;	
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
	[SerializeField] private bool m_AirControl = false;
	[SerializeField] private LayerMask m_WhatIsGround;
	[SerializeField] private Transform m_GroundCheck;
	[SerializeField] private Transform m_CeilingCheck;
	[SerializeField] private Collider2D m_CrouchDisableCollider;

	const float k_GroundedRadius = .2f;
	private bool m_Grounded;
	const float k_CeilingRadius = .2f;
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true; 
	private Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private BoxCollider2D boxColl;
    [SerializeField] private CircleCollider2D circleColl;


	private Vector2 circleOffset1 = new Vector2(-0.006f, -0.08f);
    private float circleRadius1 = 0.075f;

 
    private Vector2 circleOffset2 = new Vector2(-0.006f, -0.14f);
    private float circleRadius2 = 0.02f;
    public PlayerMovement playerMovement;

	private List<BoxCollider2D> boxColliders = new List<BoxCollider2D>();
    private Vector3 spawnLocation;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
        spawnLocation = transform.position;

        m_JumpForce = 500f;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
		GetComponents(boxColliders);


		boxColliders[0].enabled = true;
		boxColliders[1].enabled = false;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}



	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

    public void respawn()
    {
        transform.position = spawnLocation;
    }

    public void setUpRat(bool isRat)
    {
        if(!isRat)
        {
            m_JumpForce = 500f;
			boxColliders[0].enabled = true;
			boxColliders[1].enabled = false;
			
            circleColl.offset = circleOffset1;
            circleColl.radius = circleRadius1;
        }
        else
        {
            m_JumpForce = 250f;
			boxColliders[1].enabled = true;
			boxColliders[0].enabled = false;

			circleColl.offset = circleOffset2;
            circleColl.radius = circleRadius2;
        }
    }


	public void Move(float move, bool crouch, bool jump)
	{

        if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
        {
            playerMovement.ceilingAbove = true;
        }
        else playerMovement.ceilingAbove = false;


        if (m_Grounded || m_AirControl)
		{

			//if (crouch)
			//{
			//	if (!m_wasCrouching)
			//	{
			//		m_wasCrouching = true;
			//		OnCrouchEvent.Invoke(true);
			//	}

			//	move *= m_CrouchSpeed;

			//	if (m_CrouchDisableCollider != null)
			//		m_CrouchDisableCollider.enabled = false;
			//} else
			//{
   //             playerMovement.ceilingAbove = false;
			//	if (m_CrouchDisableCollider != null)
			//		m_CrouchDisableCollider.enabled = true;

			//	if (m_wasCrouching)
			//	{
			//		m_wasCrouching = false;
			//		OnCrouchEvent.Invoke(false);
			//	}
			//}

			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		if (m_Grounded && jump)
		{
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	public void Flip()
	{
		m_FacingRight = !m_FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
