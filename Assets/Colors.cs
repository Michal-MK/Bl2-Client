using System;
using System.Reflection;
using UnityEngine;

public static class Colors {
	public static readonly Color32 HAMMERLOCK = new Color32(10, 200, 240, 125); 
	public static readonly Color32 BOUNTY_BOARD = new Color32(255, 255, 255, 125); 
	public static readonly Color32 LILITH = new Color32(230, 0, 130, 125); 
	public static readonly Color32 MORDECAI = new Color32(255, 0, 0, 125); 
	public static readonly Color32 SCOOTER = new Color32(50, 100, 0, 125); 
	public static readonly Color32 TINY_TINA = new Color32(255, 255, 130, 125); 
	public static readonly Color32 TANNIS = new Color32(130, 0, 200, 125); 
	public static readonly Color32 BRICK = new Color32(100, 40, 20, 125); 
	public static readonly Color32 ELLIE = new Color32(200, 255, 0, 125); 
	public static readonly Color32 LOGGINS = new Color32(0, 0, 0, 125);

	public static Color32 GetColor(string name) {
		return (Color32)typeof(Colors).GetField(name.ToUpper()).GetValue(null);
	}
}
