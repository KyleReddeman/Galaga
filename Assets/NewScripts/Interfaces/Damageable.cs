﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {
	void Damage(int damageTaken);
	int Health();
	bool Dead();
}
