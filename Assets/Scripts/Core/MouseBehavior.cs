using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehavior : MonoBehaviour
{
    public float clickDamage;
    public float clickRange;
    public float clickCooldown;
    private List<Collectible> objectsPickedUp;
    private List<Collectible> objectsInGrasp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            Vector3 clickedPos = mousePos;
            print(clickedPos);
        }
    }
}
