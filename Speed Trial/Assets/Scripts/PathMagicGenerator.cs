using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jacovone;

public class PathMagicGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstacles;

    private GameObject pathGO;
    private PathMagic path;
    private List<Waypoint> Waypoints;

    private GameObject dog;

    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.FindGameObjectWithTag("Dog");
        CreatePath();
    }

    void CreatePath()
    {
        pathGO = new GameObject();
        path = pathGO.AddComponent<PathMagic>();
        path.Target = dog.transform;

        Waypoint[] temp = new Waypoint[obstacles.Length + 1];

        Waypoint startingPoint = new Waypoint();
        temp[0] = startingPoint;
        startingPoint.Position = path.Target.position;
        startingPoint.SetLocalYPosition(0);

        path.presampledPath = true;
        path.samplesNum = 300;

        for (var i=1; i < obstacles.Length + 1; i++)
        {
            Waypoint w = new Waypoint();
            temp[i] = w;
            w.Position = obstacles[i - 1].transform.position;
            w.SetLocalYPosition(0);
        }

        path.Waypoints = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
