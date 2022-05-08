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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePos, clickRadius);
            print(colliders);
        }
    }
}
