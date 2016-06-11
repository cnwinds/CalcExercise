using UnityEngine;
using System.Collections;
using ProtoBuf;
using System.Collections.Generic;

[ProtoContract]
class Examine {
	[ProtoMember(1)]
	public int totalQuestionCount;
	[ProtoMember(2)]
	public int correctAnswerCount;
	[ProtoMember(3)]
	public double elapseSeconds;
	[ProtoMember(4)]
	public System.DateTime startTime;
	[ProtoMember(5)]
	public System.DateTime endTime;
	[ProtoMember(6)]
	public List<Question> currentQuests;

	public bool isErrorQuestionMode;
	public int doneQuestionCount;
	private System.DateTime start;
	private System.DateTime end;
	private bool isFirstNext;

	public void InitExamine(int total) {
		isErrorQuestionMode = false;

		if (currentQuests == null) {
			currentQuests = new List<Question> ();
		} else {
			currentQuests.Clear ();
		}
		totalQuestionCount = total;

		for (int i = 0; i < total; i++) {
			Question q = new Question ();
			q.CreateQuestion ();
			currentQuests.Add (q);
		}
	}

	public void InitExamine(Examine examine, bool isErrorQuestion) {
		isErrorQuestionMode = isErrorQuestion;

		if (currentQuests == null) {
			currentQuests = new List<Question> ();
		} else {
			currentQuests.Clear ();
		}
		totalQuestionCount = 0;

		foreach (Question q in examine.currentQuests) {
			if (isErrorQuestion) {
				if (!q.IsUserAnswerCorrect ()) {
					currentQuests.Add (q);
					totalQuestionCount++;
				}
			} else {
				currentQuests.Add (q);
				totalQuestionCount++;
			}
		}
	}

	void StartElapseTime() {
		elapseSeconds = 0;
		start = System.DateTime.Now;
		//InvokeRepeating ("UpdateElapseTime", 1, 1);
	}

	void UpdateElapseTime() {
		elapseSeconds = (int)((System.DateTime.Now - start).TotalSeconds);
	}

	void StopElapseTime() {
		end = System.DateTime.Now;
		startTime = start;
		endTime = end;
		elapseSeconds = (end - start).TotalSeconds;
	}

	public double GetCurrentElapseTime() {
		return (System.DateTime.Now - start).TotalSeconds;
	}

	public void StartExamine() {
		isFirstNext = true;
		doneQuestionCount = 0;
		StartElapseTime ();
	}

	public Question GetQuestion() {
		return currentQuests [doneQuestionCount];
	}

	public bool NextQuestion() {
		if (doneQuestionCount >= totalQuestionCount) {
			return false;
		}

		if (isFirstNext) {
			isFirstNext = false;
		} else {
			doneQuestionCount++;
		}

		if (doneQuestionCount < totalQuestionCount) {
			return true;
		} else {
			StopElapseTime ();
			return false;
		}
	}

	public void SetAnswer(int answer) {
		Question q = GetQuestion ();
		q.SetAnswer (answer);
		if (q.IsUserAnswerCorrect()) {
			correctAnswerCount++;
		}
	}

    public string Rate()
    {
        double score = Score();
        if (score > 99.99999)
            return "A+++";
        else if (score > 99.00)
            return "A++";
        else if (score > 95.00)
            return "A+";
        else if (score > 90)
            return "A";
        else if (score > 80)
            return "B";
        else if (score > 70)
            return "C";
        else if (score > 60)
            return "D";
        else if (score > 50)
            return "E";
        else
            return "F";
    }

    public double Score()
    {
        return Mathf.RoundToInt((correctAnswerCount / totalQuestionCount) * 10000) / 100.0f;
    }

};