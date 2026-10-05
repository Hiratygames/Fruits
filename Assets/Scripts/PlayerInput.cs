using UnityEngine;

[DefaultExecutionOrder(-99)]
public class PlayerInput : MonoBehaviour
{
	public enum ButtonType
	{
		Decide,
		Cancel,
		Drop,
		Offer,
		Menu,

		Next,
		Prev,

		Max
	}
	public enum DirectionType
	{
		Right,
		Left,
		Up,
		Down,

		Max
	}
	public enum AxisType
	{
		Horizontal,
		Vertical,

		Max
	}
	private enum Sign
	{
		Positive,
		Negative
	}
	private readonly struct DirectionMap
	{
		public AxisType axis { get; }
		public Sign sign { get; }
		public DirectionMap(AxisType axis, Sign sign)
		{
			this.axis = axis;
			this.sign = sign;
		}
	}
	private DirectionMap[] directionMaps = new DirectionMap[(int)DirectionType.Max]
	{
		new DirectionMap( AxisType.Horizontal, Sign.Positive ),	// Right
		new DirectionMap( AxisType.Horizontal, Sign.Negative ),	// Left
		new DirectionMap( AxisType.Vertical  , Sign.Positive ),	// Up
		new DirectionMap( AxisType.Vertical  , Sign.Negative ),	// Down
	};
#if UNITY_SWITCH && !UNITY_EDITOR
	// Switch JoyCon 
	// B JoystickButton0
	// A JoystickButton1
	// Y JoystickButton2
	// X JoystickButton3
	// L JoystickButton4
	// R JoystickButton5
	// - JoystickButton6
	// + JoystickButton7
	// Lスティック JoystickButton8
	// Rスティック JoystickButton9
	// ZL JoystickButton10
	// ZR JoystickButton11
	// 下 JoystickButton12
	// 右 JoystickButton13
	// 左 JoystickButton14
	// 上 JoystickButton15
	private KeyCode[] keyCodes = new KeyCode[(int)ButtonType.Max]
	{
		KeyCode.Joystick1Button1,
		KeyCode.Joystick1Button0,
		KeyCode.Joystick1Button1,
		KeyCode.Joystick1Button2,
		KeyCode.Joystick1Button3,
		
		KeyCode.Joystick1Button5,
		KeyCode.Joystick1Button4,
	};
#else
	private KeyCode[] keyCodes = new KeyCode[(int)ButtonType.Max]
	{
		KeyCode.Space,
		KeyCode.C,
		KeyCode.Space,
		KeyCode.Return,
		KeyCode.E,

		KeyCode.RightShift,
		KeyCode.LeftShift,
	};
#endif

	private static PlayerInput instance = null;
	public static PlayerInput Instance { 
		get {
			if (instance == null)
			{
				GameObject gameObject = new GameObject("PlayerInput", typeof(PlayerInput));
				DontDestroyOnLoad(gameObject);
				instance = gameObject.GetComponent<PlayerInput>();
			}
			return instance;
		}
	}
	public static bool GetDown(ButtonType type) { return Instance.Down(type); }
	public static bool GetDown(DirectionType type) { return Instance.Down(type); }
	public static bool GetPress(ButtonType type) { return Instance.Press(type); }
	public static bool GetPress(DirectionType type) { return Instance.Press(type); }
	public static bool GetUp(ButtonType type) { return Instance.Up(type); }
	public static bool GetUp(DirectionType type) { return Instance.Up(type); }
	public static float GetAxis(AxisType type) { return Instance.Axis(type); }

	private bool[] keyDowns = new bool[(int)ButtonType.Max];
	private bool[] dirDowns = new bool[(int)DirectionType.Max];
	private bool[] keyPresses = new bool[(int)ButtonType.Max];
	private bool[] dirPresses = new bool[(int)DirectionType.Max];
	private bool[] keyUps = new bool[(int)ButtonType.Max];
	private bool[] dirUps = new bool[(int)DirectionType.Max];
	private float[] axis = new float[(int)AxisType.Max];
	public bool Down(ButtonType type) => keyDowns[(int)type];
	public bool Down(DirectionType type) => dirDowns[(int)type];
	public bool Press(ButtonType type) => keyPresses[(int)type];
	public bool Press(DirectionType type) => dirPresses[(int)type];
	public bool Up(ButtonType type) => keyUps[(int)type];
	public bool Up(DirectionType type) => dirUps[(int)type];
	public float Axis(AxisType type) => axis[(int)type];

	public float DeadZone { get; private set; } = 0.2f;
	void Awake()
	{
		ResetInput();
	}
	void Update()
	{
		UpdateButtonInput();
		UpdateAxisInput();
	}
	private void UpdateButtonInput()
	{
		for (ButtonType type = 0; type < ButtonType.Max; type++)
		{
			keyDowns[(int)type] = Input.GetKeyDown(keyCodes[(int)type]);
			keyPresses[(int)type] = Input.GetKey(keyCodes[(int)type]);
			keyUps[(int)type] = Input.GetKeyUp(keyCodes[(int)type]);
		}
	}
	private void UpdateAxisInput()
	{
		axis[(int)AxisType.Horizontal] = Input.GetAxisRaw("Horizontal");
		if (Mathf.Abs(axis[(int)AxisType.Horizontal]) <= DeadZone) { axis[(int)AxisType.Horizontal] = 0; }
		axis[(int)AxisType.Vertical] = Input.GetAxisRaw("Vertical");
		if (Mathf.Abs(axis[(int)AxisType.Vertical]) <= DeadZone) { axis[(int)AxisType.Vertical] = 0; }

		for (DirectionType type = 0; type < DirectionType.Max; type++)
		{
			bool prevPress = dirPresses[(int)type];
			DirectionMap dir = directionMaps[(int)type];
			dirPresses[(int)type] = (dir.sign == Sign.Positive && axis[(int)dir.axis] > 0) || (dir.sign == Sign.Negative && axis[(int)dir.axis] < 0);
			dirDowns[(int)type] = (prevPress == false && dirPresses[(int)type]);
			dirUps[(int)type] = (prevPress == true && !dirPresses[(int)type]);
		}
	}
	private void ResetInput()
	{
		for (ButtonType type = 0; type < ButtonType.Max; type++)
		{
			keyDowns[(int)type] = keyPresses[(int)type] = keyUps[(int)type] = false;
		}
		for (DirectionType type = 0; type < DirectionType.Max; type++)
		{
			dirDowns[(int)type] = dirPresses[(int)type] = dirUps[(int)type] = false;
		}
	}
}
