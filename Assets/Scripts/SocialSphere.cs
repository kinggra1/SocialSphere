using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SocialSphere : MonoBehaviour {

    public GameObject previewPrefab;
    public GameObject socialPrefab;
    public GameObject tagPrefab;

    private List<PreviewBox> previewBoxes = new List<PreviewBox>();
	private List<TweetSearchTwitterData> tweets = new List<TweetSearchTwitterData>();
    private List<TweetTopTrendsData> trends;
    private int tweetIndex = 0;

    private static string[] stopWords = {"", "-", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "about", "above", "above", "across", "after", "afterwards", "again", "against", "all", "almost", "alone", "along", "already", "also", "although", "always", "am", "among", "amongst", "amoungst", "amount", "an", "and", "another", "any", "anyhow", "anyone", "anything", "anyway", "anywhere", "are", "around", "as", "at", "back", "be", "became", "because", "become", "becomes", "becoming", "been", "before", "beforehand", "behind", "being", "below", "beside", "besides", "between", "beyond", "bill", "both", "bottom", "but", "by", "call", "can", "cannot", "cant", "co", "con", "could", "couldnt", "cry", "de", "describe", "detail", "do", "done", "down", "due", "during", "did", "didnt", "does", "doesnt", "dont", "each", "eg", "eight", "either", "eleven", "else", "elsewhere", "empty", "enough", "etc", "even", "ever", "every", "everyone", "everything", "everywhere", "except", "few", "fifteen", "fify", "fill", "find", "fire", "first", "five", "for", "former", "formerly", "forty", "found", "four", "from", "front", "full", "further", "get", "give", "go", "got", "had", "has", "hasnt", "have", "he", "hence", "her", "here", "hereafter", "hereby", "herein", "hereupon", "hers", "herself", "him", "himself", "his", "how", "however", "hundred", "ie", "if", "in", "im", "inc", "indeed", "interest", "into", "is", "it", "its", "itself", "keep", "last", "latter", "latterly", "least", "less", "like", "ltd", "made", "many", "may", "me", "meanwhile", "might", "mill", "mine", "more", "moreover", "most", "mostly", "move", "much", "must", "my", "myself", "name", "namely", "neither", "never", "nevertheless", "next", "nine", "no", "nobody", "none", "noone", "nor", "not", "nothing", "now", "nowhere", "of", "ok", "okay", "off", "often", "on", "once", "one", "only", "onto", "or", "other", "others", "otherwise", "our", "ours", "ourselves", "out", "over", "own", "part", "per", "perhaps", "please", "put", "rather", "re", "really", "same", "say", "see", "seem", "seemed", "seeming", "seems", "serious", "several", "she", "should", "show", "side", "since", "sincere", "six", "sixty", "so", "some", "somehow", "someone", "something", "sometime", "sometimes", "somewhere", "still", "such", "system", "take", "ten", "than", "that", "the", "their", "them", "themselves", "then", "thence", "there", "that", "thank", "thanks", "thereafter", "thereby", "therefore", "therein", "thereupon", "these", "they", "thickv", "thin", "third", "this", "those", "though", "three", "through", "throughout", "thru", "thus", "to", "together", "too", "top", "toward", "towards", "twelve", "twenty", "two", "un", "uh", "under", "until", "up", "upon", "us", "very", "via", "was", "we", "well", "were", "what", "whatever", "when", "whence", "whenever", "where", "whereafter", "whereas", "want", "whereby", "wherein", "whereupon", "wherever", "whether", "which", "while", "whither", "who", "whoever", "whole", "whom", "whose", "why", "will", "with", "within", "without", "would", "wont", "yea", "yeah", "yet", "you", "your", "youre", "you're", "yours", "yourself", "yourselves", "the", "lol", "tbt", "rofl", "brb", "rotfl", "rotflmao", "lmfao", "wtf", "omg", "asap", "totally", "aint", "ain't", "ily", "ily2", "ive", "nbd", "yes", "no", "oh", "just", "amp", "shit", "people", "wanna", "right", "fuck", "fucking", "thats", "make", "come", "gonna", "que", "bad", "nigga", "ya", "lmao", "bitch", "yo", "damn", "la", "let", "yall", "rt", "gotta", "talk", "lets", "haha", "big", "doing", "tho", "bae", "el", "rn", "hes", "whats", "sure", "en", "hell", "niggas", "wow", "bc", "ur", "bout", "bro", "da", "bruh", "dude", "idk", "shes", "af", "id", "10", "probably", "havent", "isnt", "isn't", "i'm", "she's", "he's", "aren't", "can't", "won't", "shits", "tits", "theres", "there's", "smh", "se", "lil", "mi", "bitches", "gets", "te", "ima", "es", "lo", "los", "wasn't", "wasnt", "hahaha", "hi", "theyre", "they're", "till", "nah", "tf", "por", "ugh", "tryna", "em", "fucked", "cuz", "ppl", "20", "pic", "whos", "who's", "si", "vs", "fuckin", "las", "para", "wouldn't", "wouldnt", "san", "bye", "12", "hoes", "tu", "tbh", "dick", "100", "hoe", "aren't", "arent", "ha", "youll", "you'll", "oomf", "ma", "st", "15", "dm", "30", "ima", "i'ma", "fav", "al", "finna", "le", "gt", "como", "pussy", "1st", "fam", "una", "boo", "del", "dat", "pls", "booty", "hella", "lt3", "youve", "you've", "bday", "soo", "na", "til", "fr", "sooo", "ye", "retweet", "outta", "11", "50", "quiero", "kno", "ni", "shouldn't", "shouldnt", "avi", "toda", "homie", "freaking", "gon", "goin", "tha", "soooo", "2nd", "fb", "ft", "aw", "pero", "21", "asf", "omfg", "mas", "ah", "3rd", "esta", "plz", "idc", "tl", "todo", "ex", "18", "jk", "iu2019m", "wheres", "where's", "yor", "40", "jus", "luv", "cuando", "nope", "shitty", "13", "thx", "stfu", "14", "i", "I"};


	// Use this for initialization
	void Start () {

		float minPhi = 60f*Mathf.PI/180;
		float maxPhi = 120f*Mathf.PI/180;


		float distance = 25f;

		for (int r = 0; r < 5; r++) {
			float theta = minPhi + r*(maxPhi-minPhi)/4;

			int iMax = 10-Mathf.Abs(r-2)*1;
			for (int i = 0; i < iMax; i++) {
				float phi = (i*2*Mathf.PI)/iMax;

				float xPos = Mathf.Sin(theta) * Mathf.Cos(phi) * distance + Random.value-0.5f;
				float yPos = Mathf.Cos(theta) * distance + Random.value-0.5f;
				float zPos = Mathf.Sin(theta) * Mathf.Sin(phi) * distance + Random.value-0.5f;

				Vector3 previewPos = new Vector3(xPos, yPos, zPos);
				GameObject preview = Instantiate(
					previewPrefab,
					new Vector3(xPos, yPos, zPos),
					Quaternion.LookRotation(previewPos - Camera.main.transform.position)
				) as GameObject;

				PreviewBox pBox = preview.GetComponent<PreviewBox>();
				pBox.SetSphere(this);
				previewBoxes.Add(pBox);


			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SearchAndFill(string query) {
		TwitterAPI.instance.SearchTwitter(query, PopulateTweets);
		tweetIndex = 0;
	}

	public void PopulateTweets(List<TweetSearchTwitterData> newTweets) {
		StartCoroutine(AsyncPopulateTweets(newTweets));
	}

	IEnumerator AsyncPopulateTweets(List<TweetSearchTwitterData> newTweets) {
		Debug.Log(newTweets.Count + " tweets pulled");
		tweets = newTweets;

		foreach (PreviewBox box in previewBoxes) {
			box.SetTweet(NextTweet());
			yield return null;
		}
	}

	public IEnumerator Apperate() {
		foreach (PreviewBox box in previewBoxes) {
			box.transform.localScale = Vector3.one*0.01f;
			box.LookedAway();
			yield return null;
		}
	}

	public TweetSearchTwitterData NextTweet() {
		tweetIndex++;
		tweetIndex%=tweets.Count;
		TweetSearchTwitterData result = tweets[tweetIndex];
		return result;
	}

	public bool HasTweets() {
		return tweets.Count != 0;
	}

	static public bool StopWord(string word) {
		if(stopWords.Contains(word.ToLower()))
        {
            return true;
        }
        else
        {
            return false;
        }
	}

	public void HideFam() {
		foreach (PreviewBox box in previewBoxes) {
			box.Disappear();
			//box.gameObject.SetActive(false);
		}
	}

	public void ShowFam() {
		foreach (PreviewBox box in previewBoxes) {
			box.Reappear();
			//box.gameObject.SetActive(true);
		}
	}
}
