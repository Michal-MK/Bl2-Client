using PacketNS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateColorInfo : MonoBehaviour {

	public GameObject prefab;

	internal bool isFirstLoad = false;

	public static PopulateColorInfo instance;

	private List<GameObject> objects = new List<GameObject>();

	private void Awake() {
		isFirstLoad = false;
		instance = this;
	}

	public void OnDataReceived(Quest[] data) {
		if (!isFirstLoad) {
			List<Quest> processed = new List<Quest>();
			foreach (Quest q in data) {
				if (!ProcessedYet(processed, q)) {
					processed.Add(q);
					GameObject g = Instantiate(prefab, transform.GetChild(1));
					g.GetComponent<ColorInfo>().SetUp(q.givenBy);
					g.name = q.givenBy;
					objects.Add(g);
				}
			}
			isFirstLoad = true;
		}
		else {
			UpdateShownColours(data);
		}
	}

	private void UpdateShownColours(Quest[] data) {
		objects.ForEach((g) => g.SetActive(false));
		Dictionary<string, int> actives = new Dictionary<string, int>();

		for (int i = 0; i < data.Length; i++) {
			string current = data[i].givenBy;
			if (data[i].status == QuestStatus.Accepted || data[i].status == QuestStatus.NotAccepted) {
				if (!actives.ContainsKey(current)) {
					actives.Add(current, 1);
				}
				else {
					actives[current]++;
				}
			}
		}

		foreach (KeyValuePair<string, int> kvp in actives) {
			GameObject refr = objects.Find((g) => { return g.name == kvp.Key; });
			refr.SetActive(true);
		}
	}

	private bool ProcessedYet(List<Quest> processed, Quest quest) {
		foreach (Quest q in processed) {
			if (q.givenBy == quest.givenBy) {
				return true;
			}
		}
		return false;
	}
}
