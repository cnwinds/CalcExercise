using UnityEngine;
using System.Collections;
using System.IO;
using ProtoBuf;
using System.Collections.Generic;

public class WrongManager
{
	private static Wrong wrong;

	public static void Load ()
	{
		string file = Application.persistentDataPath + "/wrong";
		if (File.Exists (file)) {
			var f = File.OpenRead (file);
			wrong = Serializer.Deserialize<Wrong> (f);
			f.Close ();
		} else {
			wrong = new Wrong ();
			wrong.questionList = new List<Question> ();
		}
	}

	public static void Save ()
	{
		var f = File.Create (Application.persistentDataPath + "/wrong");
		Serializer.Serialize (f, wrong);
		f.Close ();
	}

	public static void AddWrongQuestion (Examine examine)
	{
		// 错题登记
		for (int i = 0; i < examine.currentQuests.Count; i++) {
			Question q = examine.currentQuests [i];
			bool isProcess = false;
			for (int j = 0; j < wrong.questionList.Count; j++) {
				Question qt = wrong.questionList [j];
				if (q.IsSame (qt)) {
					qt.errorCount++;
					isProcess = true;
					break;
				}
			}
			if (!isProcess) {
				q.errorCount += 3;
				wrong.questionList.Add (q);
			}
		}
	}

	public static List<Question> TakeQuestion (int count)
	{
		// Debug.LogFormat ("takequestion {0}, {1}", count, wrong.questionList.Count);
		List<Question> result = new List<Question> ();
		int takeCount = Mathf.Min (count, wrong.questionList.Count);
		for (int j = 0; j < takeCount; j++) {
			Question q = wrong.questionList [j];
			q.errorCount--;
			result.Add (q);
		}

		for (int i = 0; i < result.Count; i++) {
			Question q = result [i];
			if (q.errorCount <= 0) {
				wrong.questionList.Remove (q);
			}
		}
		return result;
	}

	public static int Count ()
	{
		return wrong.questionList.Count;
	}

	public static Question Item (int index)
	{
		return wrong.questionList [index];
	}
}