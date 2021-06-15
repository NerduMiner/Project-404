using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] float _TimeTillShake = 2.5f;

	float _ShakeTimer = 0;
	EnemyDamageScript _Damage = null;

	private void Awake()
	{
		_Damage = GetComponent<EnemyDamageScript>();
	}

	private void Update()
	{
		if (_Damage._AttachedPikmin.Count != 0)
		{
			_ShakeTimer += Time.deltaTime;

			if (_ShakeTimer >= _TimeTillShake)
			{
				// Shake the pikmin off

				int i = _Damage._AttachedPikmin.Count;
				while (i > 0)
				{
					var pikmin = _Damage._AttachedPikmin[i - 1];
					pikmin.ChangeState(PikminStates.Idle);
					var rb = pikmin.GetComponent<Rigidbody>();
					rb.isKinematic = false;
					rb.AddForce(-pikmin.transform.forward * 10000);
					i--;
				}

				_ShakeTimer = 0;
			}
		}
	}
}
