using UnityEngine;

public class Wisp : MonoBehaviour 
{
    Camera cam = null;
    GameObject sphere = null;
    Rotate rotate = null;
    AudioSource pop = null;

    void Start () 
	{
        cam = this.GetComponentInParent<Camera>();
        sphere = GameObject.FindGameObjectsWithTag("Wisp")[0];
        if (sphere != null)
        {
            rotate = sphere.GetComponentInChildren<Rotate>();
            var sounds = sphere.GetComponents<AudioSource>();
            foreach (var sound in sounds)
            {
                if (sound.clip.name.Contains("pop"))
                {
                    pop = sound;
                    break;
                }
            }
        }
	}

	void Update () 
	{
        if (cam != null && sphere != null)
        {
            if (Vector3.Distance(cam.transform.position, sphere.transform.position) < 1)
            {
                if (rotate != null)
                    rotate.speed = 6;
                if (pop != null)
                    pop.Play();
                if (sphere != null)
                    sphere.transform.Translate((Random.value - 0.5f) * 4f, 0f, (Random.value - 0.5f) * 4f);
            }
            else
            {
                rotate.speed = 3;
            }
        }
	}
}
