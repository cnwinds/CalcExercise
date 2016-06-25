using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum CalcMode
{
	CM_ADD = 0,
	CM_SUB = 1,
	CM_MUL = 2,
	CM_DIV = 3}
;

public class GameMain : MonoBehaviour
{

	public Text Question;
	public Text Answer;
	public Text UsedTime;
	public Text QuestionProgress;
	public Transform Report;

	private string inputAnswer;
	private Examine examine;

	public object SceneManger { get; private set; }

	// Use this for initialization
	void Start ()
	{
		if (FindObjectOfType<MenuMain> ().isNewExceise) {
			StartNewQuestion ();
		} else {
			StartWrongQuestion ();
		}

		Report.gameObject.SetActive (false);
	}

	public void StartNewQuestion ()
	{
		examine = new Examine ();
		#if UNITY_EDITOR
		examine.InitExamine (5);
		#else
		examine.InitExamine (20);
		#endif
		examine.StartExamine ();

		examine.NextQuestion ();
		UpdateQuestion ();
	}

	public void StartWrongQuestion ()
	{
		examine = new Examine ();
		#if UNITY_EDITOR
		examine.InitExamineWithWrongQuestion (5);
		#else
		examine.InitExamineWithWrongQuestion (20);
		#endif

		examine.NextQuestion ();
		UpdateQuestion ();
	}

	void UpdateQuestion ()
	{
		Question q = examine.GetQuestion ();
		Question.text = q.GetQuestionDesc ();
		inputAnswer = "";
	}

	// Update is called once per frame
	void Update ()
	{
		Answer.text = inputAnswer;

		QuestionProgress.text = string.Format ("完成题目: {0} / {1}", examine.doneQuestionCount, examine.totalQuestionCount);
		UsedTime.text = string.Format ("用时: {0:F1} 秒", examine.GetCurrentElapseTime ());
	}

	public void OnPressNum (int value)
	{
		inputAnswer += value.ToString ();
	}

	public void OnPressEqual ()
	{
		if (inputAnswer.Length <= 0)
			return;

		examine.SetAnswer (int.Parse (inputAnswer));

		if (examine.NextQuestion ()) {
			UpdateQuestion ();
		} else {
			if (examine.isErrorQuestionMode) {
				ShowReReport ();
			} else {
				ShowReport ();
			}
			if (!examine.isErrorQuestionMode) {
				// save score
			}
			return;
		}
	}

	public void OnPressClear ()
	{
		inputAnswer = "";
	}

	public void OnPressClose ()
	{
		Report.gameObject.SetActive (false);
		SceneManager.LoadScene ("Menu");
	}

	public void OnPressRetry ()
	{
		Report.gameObject.SetActive (false);
		Examine e = new Examine ();
		e.InitExamine (examine, true);
		examine = e;

		examine.StartExamine ();
		examine.NextQuestion ();
		UpdateQuestion ();
	}

	void ShowReport ()
	{
		Text total = Report.Find ("Total").GetComponent<Text> ();
		Text correct = Report.Find ("Correct").GetComponent<Text> ();
		Text accuracy = Report.Find ("Accuracy").GetComponent<Text> ();
		Text useTime = Report.Find ("UseTime").GetComponent<Text> ();
		Transform ReTryButton = Report.Find ("ReTryButton");

		bool isAllRight = (examine.correctAnswerCount == examine.totalQuestionCount);

		total.text = examine.totalQuestionCount.ToString ();
		correct.text = examine.correctAnswerCount.ToString ();
		accuracy.text = (examine.totalQuestionCount - examine.correctAnswerCount).ToString ();
		useTime.text = string.Format ("{0:F1}秒", examine.elapseSeconds);
		ReTryButton.gameObject.SetActive (!isAllRight);

		if (isAllRight) {
			RankManager.AddExamine (examine);
			RankManager.Save ();
		} else {
			WrongManager.AddWrongQuestion (examine);
			WrongManager.Save ();
		}

		Report.gameObject.SetActive (true);
	}

	void ShowReReport ()
	{
		Text total = Report.Find ("Total").GetComponent<Text> ();
		Text correct = Report.Find ("Correct").GetComponent<Text> ();
		Text accuracy = Report.Find ("Accuracy").GetComponent<Text> ();
		Text useTime = Report.Find ("UseTime").GetComponent<Text> ();
		Transform ReTryButton = Report.Find ("ReTryButton");

		bool isAllRight = (examine.correctAnswerCount == examine.totalQuestionCount);

		total.text = examine.totalQuestionCount.ToString ();
		correct.text = examine.correctAnswerCount.ToString ();
		accuracy.text = (examine.totalQuestionCount - examine.correctAnswerCount).ToString ();
		// useTime.text = string.Format("{0:F1}秒", examine.elapseSeconds);
		ReTryButton.gameObject.SetActive (examine.correctAnswerCount != examine.totalQuestionCount);

		if (!isAllRight) {
			WrongManager.AddWrongQuestion (examine);
			WrongManager.Save ();
		}

		Report.gameObject.SetActive (true);
	}
}
