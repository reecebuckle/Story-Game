/**
* Adapted from Parallax Tutorial
* https://www.youtube.com/watch?v=zit45k6CUMk&feature=emb_title&ab_channel=Dani
*/
using UnityEngine;

public class Parallax : MonoBehaviour {

private float length;
private float startpos;
public GameObject cam;
public float parallaxEffect; // how much parallax effect we wanna apply


    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x; // starting position of cam
        length = GetComponent<SpriteRenderer>().bounds.size.x; // length of sprite
    }

    // Update is called once per frame
    void Update()
    {
        //if player goes out of bounds, parallax effect repeats itself
        float temp = (cam.transform.position.x * (1-parallaxEffect));
        //get a temporary distance 
        float distance = (cam.transform.position.x * parallaxEffect);
        //transform position of backgrounds
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        // handles the repeating of parallax effect if nearing out of bounds
        if (temp > startpos + length) 
            startpos += length;
        else if (temp < startpos - length) 
            startpos -= length;
    }
}
