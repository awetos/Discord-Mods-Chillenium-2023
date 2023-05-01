using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class ControllerHandler: MonoBehaviour{

	Playercontrols controls;

	Vector2 rotation;

    // Start is called before the first frame update
    void Awake(){
        controls = new Playercontrols();
    }

    // Update is called once per frame
    void Update(){
    }


}
