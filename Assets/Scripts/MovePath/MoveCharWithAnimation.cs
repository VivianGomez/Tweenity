using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveCharWithAnimation : MonoBehaviour
{
    public GameObject pathBase;
    public Vector3 offset;
    public int speed;
    public bool cicle;
    public string moveAnimationName;
    public string idleAnimationName;
    public List<Vector3> path;

    private Hashtable moveHash = new System.Collections.Hashtable();
    void Start()
    {
        if(pathBase!=null)
        {
            moveHash.Add("easeType", iTween.EaseType.linear);
            moveHash.Add("speed", speed);
            moveHash.Add("onstart", "move");
            moveHash.Add("orienttopath", true);
            moveHash.Add("looktime", 1);
            path = new List<Vector3>();
            foreach (Transform child in pathBase.transform)
            {
                path.Add(child.transform.position + offset);
            }
            moveHash.Remove("path");
            moveHash.Add("path", path.ToArray());
        }
    }
    public async Task<bool> StartMove()
    {
        if (cicle)
        {
            moveHash.Remove("oncomplete");
            moveHash.Add("oncomplete", "idlecicle");
        }
        else
        {
            moveHash.Remove("oncomplete");
            moveHash.Add("oncomplete", "idle");
        }
        iTween.MoveTo(gameObject, moveHash);
        int sum = 0;
        foreach (Transform child in pathBase.transform)
        {
            sum += (int)(Vector3.Distance(child.transform.position, gameObject.transform.position) / speed * 1000);
        }
        await Task.Delay(sum);
        return true;
    }
    public void StopMove()
    {
        iTween.Stop(gameObject);
    }
    public void StopCicle()
    {
        iTween.Pause(gameObject);
    }
    void move()
    {
        gameObject.GetComponent<Animator>().SetBool(moveAnimationName, true);
    }
    void idle()
    {
        GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<Animator>().SetBool(moveAnimationName, false);
    }
    async void idlecicle()
    {
        await StartMove();
    }
    public void setPathBase(GameObject go)
    {
        pathBase = go;
        path = new List<Vector3>();
        foreach (Transform child in pathBase.transform)
        {
            path.Add(child.transform.position + offset);
        }
        moveHash.Remove("path");
        moveHash.Add("path", path.ToArray());
    }
}
