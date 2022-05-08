using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehavior : MonoBehaviour
{
    public float clickDamage;
    public float clickRadius;
    public float clickCooldown;
    private List<Collectible> objectsPickedUp;
    private List<Collectible> objectsInGrasp;

    // References
    private CircleCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            Vector3 clickedPos = mousePos;
            if (collider.IsTouchingLayers()) {
                print("colliding with something");
            }
        }
    }
}
