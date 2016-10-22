using UnityEngine;
using System.Collections;

public class MonumentScript : MonoBehaviour {
    public string parentTag;
    public float distanceFromParent;
    public int fallingSpeed;
    public LayerMask layerMask;
    public GameObject beamPrefab;
    public float maxCast;

    GameObject beam;
    float spriteWidth, spriteHeight, xScale, yScale;
    bool locked;
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        spriteWidth = renderer.bounds.size.x;
        spriteHeight = renderer.bounds.size.y;
        beam =(GameObject) Instantiate(beamPrefab, transform.position, transform.rotation);
        beam.transform.localScale = new Vector3(spriteWidth, spriteWidth, 1.0f);
        beam.transform.Translate(-(spriteHeight * 0.5f + spriteWidth * 0.5f) * Vector3.up);
        beam.transform.parent = transform;
        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
        locked = false;
    }

    void Update()
    {
        if(!locked)
        {
            transform.Translate(-fallingSpeed * Time.deltaTime * Vector3.up);
            RaycastHit2D hit= Physics2D.Raycast(transform.position, -Vector2.up, maxCast, layerMask);
            if(hit)
            {

                beam.transform.localScale = new Vector3(spriteWidth / xScale, hit.distance / yScale, 1.0f);
                beam.transform.localPosition = - ((spriteHeight * 0.5f + hit.distance * 0.5f)/yScale) * Vector3.up;
            }
            else
            {
                beam.transform.localScale = new Vector3(spriteWidth / xScale, maxCast / yScale, 1.0f);
                beam.transform.localPosition = -((spriteHeight * 0.5f + maxCast * 0.5f)/yScale) * Vector3.up;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!locked && other.gameObject.tag == parentTag)
        {
            Destroy(beam);
            locked = true;
            transform.position = other.transform.position + distanceFromParent * Vector3.up;
            transform.parent = other.transform;
            GameManager.instance.AddAngle(other.transform.localEulerAngles.z);            
        }
        else if (!locked && other.gameObject.tag == "Monument")
        {
            GameManager.instance.YouLose();
        }
    }
}
