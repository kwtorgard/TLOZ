using UnityEngine;
using System.Collections;

public class GameFlow : MonoBehaviour {

	// Creating Singleton variables
	private static GameFlow _instance;

	public static GameFlow instance{
		get{
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<GameFlow>();
				// Tell Unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	[SerializeField]	float	deltaTimeModifier = 1.0f;
	[Range(0.0f, 1.0f)]
	[SerializeField]	float	dTimeTarget = 1.0f;
	public	float	dTimeTargetSpeed = 1.0f;
	public	float	dTimeModified; // This should be a property!
	[SerializeField]	bool	pause = false;


	public float readingSpeed = 2.0f;

	public InteractableRev playerInteractable;

	public ConversationUI conversationUI;

	public bool inputPaused; // Set to true to keep the player from moving the character, e.g. in a conversation

	void OnEnable(){
		// Listen for any broadcasts of the type 'event'
		Messenger<string>.AddListener("event", HandleEvent);
	}

	void OnDestroy(){
		Messenger<string>.RemoveListener("event", HandleEvent);
	}

	// Use this for initialization
	void Start () {
		//sayChar = GameObject.Find ("sayMaja").GetComponent<TextMesh> ();
		//sayCharShadow = GameObject.Find ("sayMajaShadow").GetComponent<TextMesh> ();
		playerInteractable = GameObject.Find ("Player").GetComponent<InteractableRev>();
		conversationUI = GameObject.Find("Conversation").GetComponent<ConversationUI>();
	}
	
	// Update is called once per frame
	void Update () {
	
		deltaTimeModifier = Mathf.Lerp (deltaTimeModifier, dTimeTarget, dTimeTargetSpeed * Time.deltaTime);
		
		Mathf.Clamp (deltaTimeModifier, 0.0f, 1.0f);
		
		dTimeModified = Time.deltaTime * deltaTimeModifier;

		if (Input.GetKeyDown(KeyCode.RightBracket)){
			readingSpeed += 1.0f;
			readingSpeed = Mathf.Clamp(readingSpeed, 0.5f, 8.0f);
			Debug.Log("ReadingSpeed: " + readingSpeed + ". Put this into GUI!");
		}

		if (Input.GetKeyDown(KeyCode.LeftBracket)){
			readingSpeed -= 1.0f;
			readingSpeed = Mathf.Clamp(readingSpeed, 0.5f, 8.0f);
			Debug.Log("ReadingSpeed: " + readingSpeed + ". Put this into GUI!");
		}


	}

	/**
	 * Super generic method, called whenever anything sends a broadcast with the name 'event'. Added here mainly to show
	 * how other scripts can subscribe to this broadcast if they need to. 
	 * 
	 * To send a broadcast use this line:
	 * 
	 * Messenger<string>.Broadcast("event", eventName);
	 * 
	 */
	private void HandleEvent(string eventName){
		Debug.Log("Detected event: " + eventName);
	}

	public void playerSay(string text){
		playerInteractable.say(text);
		//sayChar.text = text;
		//sayCharShadow.text = text;
		//ResetReadingTime();
	}

}
