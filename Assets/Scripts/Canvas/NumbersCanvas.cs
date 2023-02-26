using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NumbersCanvas : MonoBehaviour
{
    

    [SerializeField]
    GameObject AttackTextPrefab;
    [SerializeField]
    GameObject CoinTextPrefab;
    [SerializeField]
    GameObject TakeDamageTextPrefab;


    private void OnEnable()
    {
        EnemyHealth.OnEnemyTakeDamage += CreateAttackText;
    }
    private void OnDisable()
    {
        EnemyHealth.OnEnemyTakeDamage -= CreateAttackText;
    }
    // Start is called before the first frame update
    void Start()
    {
     if(_canvas == null)
        {
            _canvas = GetComponent<Canvas>();
        }   
     if(_cam == null)
        {
            _cam = GameObject.FindObjectOfType<Camera>();
        }
     if(AttackTextPrefab == null)
        {
            AttackTextPrefab = Resources.Load<GameObject>("Prefabs/UI/Attack_Text_Prefab");
        }
     if(CoinTextPrefab == null)
        {
            CoinTextPrefab = Resources.Load<GameObject>("Prefabs/UI/Coin_Text_Prefab");
        }
     if(TakeDamageTextPrefab == null)
        {
            TakeDamageTextPrefab = Resources.Load<GameObject>("Prefabs/UI/Take_Damage_Prefab");
        }
        SetSizes();
    }

    // Update is called once per frame
   
    [SerializeField]
    Camera _cam;
    [SerializeField]
    Vector2 RawPosition;

    [SerializeField]
    Vector2 camscreen;

    [SerializeField]
    float MAX_X;
    [SerializeField]
    float MAX_Y;
    [SerializeField]
    float MIN_X;
    [SerializeField]
    float MIN_Y;

    [SerializeField]
    Canvas _canvas;
    void SetSizes()
    {

        if (_canvas == null)
        {
            _canvas = GetComponent<Canvas>();
        }
        CANVAS_WIDTH = GetComponent<RectTransform>().sizeDelta.x;
        CANVAS_HEIGHT = GetComponent<RectTransform>().sizeDelta.y;

        /*
        //Clamp so that 80% of the viewport is viewable. 
        MAX_X = screen_width / 4f * 0.8f;
        MAX_Y = screen_height / 4f * 0.8f;
        MIN_X = (-1) * screen_width / 4f * 0.8f;
        MIN_Y = (-1) * screen_height / 4f * 0.8f;
        */

        MAX_X = CANVAS_WIDTH / 2f;
        MIN_X = (-1f) * MAX_X;
        MAX_Y = CANVAS_HEIGHT / 2f;
        MIN_Y = (-1f) * MAX_Y;
    }

    public void CreateAttackText(Vector3 _location, int damage)
    {
       GameObject go =  Instantiate(AttackTextPrefab, this.transform);
        go.GetComponent<AttackText>().SetLocation(Get2DPosition(_location));
        go.GetComponent<AttackText>().SetText(damage.ToString());
    }

    
    public void CreateCoinText(Transform _location, int coinsdropped)
    {
        GameObject go = Instantiate(CoinTextPrefab, this.transform);
        go.GetComponent<CoinsText>().SetLocation(Get2DPosition(_location.position));
        string coins = "+ " + coinsdropped.ToString() + " health";
        go.GetComponent<CoinsText>().SetText(coins);
    }
    
    //enemy calls this when it takes damage
    //it is the coins text animation but red.
    public void CreateTakeDamage(Transform _location, int damagedealt)
    {
        GameObject go = Instantiate(TakeDamageTextPrefab, this.transform);
        //add an offset to enemy damage
        Vector3 offsetLoc = new Vector3(_location.position.x, _location.position.y * 1.5f, _location.position.z);
        //  go.GetComponent<TakeDamageText>().SetLocation(Get2DPosition(_location.position));
        go.GetComponent<TakeDamageText>().SetLocation(Get2DPosition(offsetLoc));
        go.GetComponent<TakeDamageText>().SetText(damagedealt.ToString());
    }

    [SerializeField]
    float CANVAS_WIDTH;
    [SerializeField]
    float CANVAS_HEIGHT;
    Vector2 Get2DPosition(Vector3 _location)
    {

        float x = _cam.WorldToViewportPoint(_location).x;
        float y = _cam.WorldToViewportPoint(_location).y;

        RawPosition = new Vector2(x, y);

        float new_x = 0;
        float new_y = 0;

        new_x = RawPosition.x - 0.5f;
        new_x = new_x * CANVAS_WIDTH;

        new_y = RawPosition.y - 0.5f;
        new_y = new_y * CANVAS_HEIGHT;

        if (new_x > MAX_X)
        {
            new_x = MAX_X;
        }
        if (new_x < MIN_X)
        {
            new_x = MIN_X;
        }
        if (new_y > MAX_Y)
        {
            new_y = MAX_Y;
        }
        if (new_y < MIN_Y)
        {
            new_y = MIN_Y;
        }
        camscreen = new Vector2(new_x, new_y);
        return camscreen;
        //_target.GetComponent<RectTransform>().localPosition= camscreen;


    }
}
