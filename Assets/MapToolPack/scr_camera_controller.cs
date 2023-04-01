using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class scr_camera_controller : MonoBehaviour
{
    //public Button Town;
    Camera camera;
    private UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();

    }
    
    float ZoomSpeed = 50f;
    float MoveSpeed = 25f;
    Vector3 mousea;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, MoveSpeed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-MoveSpeed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -MoveSpeed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(MoveSpeed, 0, 0) * Time.deltaTime;
        }
        Vector3 mdelta = mousea - Input.mousePosition;
        mousea = Input.mousePosition;
        
        if (Input.GetMouseButton(0) && uIManager.GetMarkerWindows().Count == 0)
        {
            transform.position += new Vector3(mdelta.x*2, mdelta.y*2, 0) * Time.deltaTime;
        }
        if(uIManager.GetMarkerWindows().Count == 0){
            camera.orthographicSize += (ZoomSpeed * -Input.mouseScrollDelta.y) * Time.deltaTime;
        }

        if(camera.orthographicSize >= 15){
            camera.orthographicSize = 15;
        } else if (camera.orthographicSize <= 2){
            camera.orthographicSize = 2;
        }

        if(transform.position.x >= 20){
            transform.position = new Vector3(20, transform.position.y, transform.position.z);
        }
        if(transform.position.x <= -20){
            transform.position = new Vector3(-20, transform.position.y, transform.position.z);
        }
        if(transform.position.y >= 25){
            transform.position = new Vector3(transform.position.x, 25, transform.position.z);
        }
        if(transform.position.y <= -25){
            transform.position = new Vector3(transform.position.x, -25, transform.position.z);
        }
    }
    
}
