using UnityEngine;
using System.Collections;
using ProtoBuf;
using System.IO;
using System.Collections.Generic;

class RankManager
{
	public static int MAX_RANK_COUNT = 20;

	private static Rank rank;

	public static void Load ()
	{
		string file = Application.persistentDataPath + "/rank";
		if (File.Exists (file)) {
			var f = File.OpenRead (file);
			rank = Serializer.Deserialize<Rank> (f);
			f.Close ();
		} else {
			rank = new Rank ();
			rank.examineList = new List<Examine> ();
		}
	}

	public static void Save ()
	{
		var f = File.Create (Application.persistentDataPath + "/rank");
		Serializer.Serialize (f, rank);
		f.Close ();
	}

	public static void AddExamine (Examine examine)
	{
		// 根据正确率排名次
		bool hasInsert = false;
		for (int i = 0; i < rank.examineList.Count; i++) {
			if (examine.ElapseSeconds() < rank.examineList[i].ElapseSeconds()) {
				rank.examineList.Insert (i, examine);
				hasInsert = true;
				break;
			}
		}
		if (!hasInsert) {
			rank.examineList.Add (examine);
		}
		if (rank.examineList.Count > MAX_RANK_COUNT) {
			rank.examineList.RemoveAt (rank.examineList.Count - 1);
		}
	}

	public static int Count ()
	{
		return rank.examineList.Count;
	}

	public static Examine Item (int index)
	{
		return rank.examineList [index];
	}
}
