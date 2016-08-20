using UnityEngine;

public class Wisp : MonoBehaviour 
{
    Camera cam = null;
    GameObject sphere = null;
    Rotate rotate = null;
    AudioSource pop = null;

    public float journeyTime = 1.0F;
    private float startTime = 0F;

    private Vector3 startPos;
    private Vector3 endPos;

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
        startTime = 0F;
	}

	void Update () 
	{
        if (cam != null && sphere != null)
        {
            if (startTime > 0F)
            {
                float fracComplete = (Time.time - startTime) / journeyTime;
                sphere.transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
                if (Vector3.Distance(sphere.transform.position, endPos) < 0.0001F)
                {
                    startTime = 0F;
                }
            }
            else
            {
                var dist = Vector3.Distance(cam.transform.position, sphere.transform.position);
                if (dist < 1)
                {
                    if (pop != null)
                        pop.Play();
                    if (sphere != null)
                    {
                        startTime = Time.time;

                        bool hit1, hit2;
                        Vector3 vec;
                        startPos = sphere.transform.position;
                        do
                        {
                            var half = new Vector3(startPos.x, startPos.y * 0.4f, startPos.z);
                            vec = new Vector3((Random.value - 0.5f) * 4f, 0f, (Random.value - 0.5f) * 4f);
                            hit1 = Physics.Raycast(startPos, vec, vec.magnitude * 2, SpatialMapping.PhysicsRaycastMask);
                            hit2 = Physics.Raycast(half, vec, vec.magnitude * 2, SpatialMapping.PhysicsRaycastMask);
                        } while (hit1 || hit2);

                        //sphere.transform.Translate(vec);

                        endPos = startPos + vec;

                        float fracComplete = (Time.time - startTime) / journeyTime;
                        sphere.transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
                        //sphere.transform.position = Vector3.Lerp(startPos, endPos, fracComplete);
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
}
