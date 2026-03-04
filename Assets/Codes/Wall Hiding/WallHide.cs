using UnityEngine;

public class WallHide : MonoBehaviour
{
    public GameObject[] Walls;       // Assign all wall objects in Inspector
    public float checkDistance = 4f;

    private bool[] isHidden;         // Track each wall's visibility state

    void Start()
    {
        isHidden = new bool[Walls.Length]; // Initialize tracking array
    }

    void Update()
    {
        for (int i = 0; i < Walls.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, Walls[i].transform.position);

            if (dist <= checkDistance && !isHidden[i])
            {
                Walls[i].SetActive(false);
                isHidden[i] = true;
            }
            else if (dist > checkDistance && isHidden[i])
            {
                Walls[i].SetActive(true);
                isHidden[i] = false;
            }
        }
    }
}
