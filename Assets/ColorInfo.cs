using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorInfo : MonoBehaviour
{
	internal void SetUp(string givenBy) {
		transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = givenBy;
		transform.GetChild(0).GetComponent<Image>().color = Colors.GetColor(givenBy);
	}
}
