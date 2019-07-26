using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AgilityCourseManager : MonoBehaviour
{
    public Jacovone.PathMagic CoursePath;

    List<CourseWaypoint> waypoints = new List<CourseWaypoint>();

    private int currentWaypointIndex = 0;

    public Transform Dog;

    [SerializeField]
    private float minimumDistance = .01f;

    Vector3 _moveDir;
    Vector3 _moveVec;
    Quaternion normalizedLookQuat;
    Quaternion _lookRotation;

    [SerializeField]
    float _physics_cur_speed = 0.5f;

    float rotationSmoothness = 5f;

    // Start is called before the first frame update
    void Start()
    {
        for(float f=0; f <=1f; f+=.001f)
        {
            Vector3 curPos = CoursePath.computePositionAtPos(f);
            Quaternion curRot = CoursePath.computeRotationAtPos(f);

            GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            point.transform.position = curPos;
            point.transform.localScale = new Vector3(.1f, .1f, .1f);
            point.GetComponent<Collider>().enabled = false;
            point.GetComponent<Renderer>().material.color = Color.red;

            CourseWaypoint myWaypoint = new CourseWaypoint(curPos, curRot, point);
            waypoints.Add(myWaypoint);
        }

        Dog.position = waypoints.First().Position;
        Dog.rotation = waypoints.First().Rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetermineWaypoint();
        MoveTowardsWaypoint(Time.fixedDeltaTime);
    }

    void DetermineWaypoint()
    {
        if(Vector3.Distance(Dog.position, waypoints[currentWaypointIndex + 1].Position) < minimumDistance && currentWaypointIndex != waypoints.Count - 1)
        {
            waypoints[currentWaypointIndex].HidePreviewObject();

            currentWaypointIndex++;

            print("Changed Waypoint Index!");
        }
    }

    void MoveTowardsWaypoint(float delta)
    {
        // move the horse toward waypoints. 
        _moveDir = waypoints[currentWaypointIndex].Position - Dog.position;
        _moveVec = _moveDir.normalized * (_physics_cur_speed) * delta;

        Dog.position += _moveVec;

        // rotate our helper container, but only on the y axis
        if (Dog != null)
        {
            normalizedLookQuat = Quaternion.LookRotation(-_moveDir);
            normalizedLookQuat = Quaternion.Slerp(Dog.localRotation, normalizedLookQuat, rotationSmoothness * delta);
            Dog.localRotation = normalizedLookQuat; //Quaternion.AngleAxis(normalizedLookQuat.y, Vector3.up);
        } 
    }
}
