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
            var dist = Vector3.Distance(cam.transform.position, sphere.transform.position);
            if (dist < 1)
            {
                if (pop != null)
                    pop.Play();
                if (sphere != null)
                {
                    bool hit1, hit2;
                    Vector3 vec;
                    var pos = sphere.transform.position;
                    do
                    {
                        var half = new Vector3(pos.x, pos.y * 0.4f, pos.z);
                        vec = new Vector3((Random.value - 0.5f) * 4f, 0f, (Random.value - 0.5f) * 4f);
                        hit1 = Physics.Raycast(sphere.transform.position, vec, vec.magnitude * 2, SpatialMapping.PhysicsRaycastMask);
                        hit2 = Physics.Raycast(half, vec, vec.magnitude * 2, SpatialMapping.PhysicsRaycastMask);
                    } while (hit1 || hit2);
                    sphere.transform.position = Vector3.Lerp(pos, pos + vec, 5f);
                    //sphere.transform.Translate(vec);
                }
            }
            else if (dist < 2)
            {
                if (rotate != null)
                    rotate.speed = 6;
            }
            else
            {
                if (rotate != null)
                    rotate.speed = 3;
            }
        }
	}
}
