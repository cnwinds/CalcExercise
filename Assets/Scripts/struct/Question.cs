using UnityEngine;
using System.Collections;
using ProtoBuf;

[ProtoContract]
public class Question
{
	[ProtoMember (1)]
	public int a;
	[ProtoMember (2)]
	public int b;
	[ProtoMember (3)]
	public CalcMode mode;
	[ProtoMember (4)]
	public int errorCount;
	[ProtoMember (5)]
	public int userAnswer;

	public bool IsSame (Question q)
	{
		if (q.a == a && q.b == b && q.mode == mode) {
			return true;
		}
		return false;
	}

	public bool IsUserAnswerCorrect ()
	{
		return GetAnswer () == userAnswer;
	}

	public void CreateQuestion ()
	{
		a = Random.Range (10, 100);
		b = Random.Range (10, 100);
		mode = (CalcMode)Random.Range (0, 2);
		int answer = GetAnswer ();
		if ((answer < 0) || (answer > 110))
			CreateQuestion ();
	}

	public string GetQuestionDesc ()
	{
		string[] ModeStr = new string[] { "+", "-", "*", "/" };
		return a.ToString () + " " + ModeStr [(int)(mode)] + " " + b.ToString () + " = ";
	}

	public void SetAnswer (int answer)
	{
		userAnswer = answer;
	}

	public int GetAnswer ()
	{
		switch (mode) {
		case CalcMode.CM_ADD:
			return a + b;
		case CalcMode.CM_SUB:
			return a - b;
		case CalcMode.CM_MUL:
			return a * b;
		case CalcMode.CM_DIV:
			return a / b;
		}
		return 0;
	}

};