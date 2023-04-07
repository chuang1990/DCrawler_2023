using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle
{
	public Action OnStanceChanged;
	public Action<Button> OnButtonSmashed;
	public Action OnSideChanged;
	public Action<BattleResult> OnBattleFinished;
	public EnemyController Enemy;
	public Button Stance;
	public Side Side;
	// A value between -1 and 1
	public float HeartMeter;
	public float ChangeStanceTime;
}
