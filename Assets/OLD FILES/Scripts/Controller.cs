using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class Controller: MonoBehaviour{

	public Playercontrols.PlayerActions control;
	public DualShockGamepad DSGamepad = (DualShockGamepad)Gamepad.current;
	public Color player1Color;
	public Color player2Color;

    // Start is called before the first frame update
    void Awake(){
        Playercontrols controls = new Playercontrols();
		control = controls.Player;
		Cursor.visible = false;
    }
	private void OnEnable() {
		control.Enable();
	}

	private void OnDisable() {
		control.Disable();
	}


}
