using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    [SerializeField]
	protected Color InteractingColor = Color.yellow;

	[SerializeField]
	protected Color ActivatingColor = Color.green;

	[SerializeField]
	protected Color ObjectiveColor;

    [SerializeField]
    protected Outline Outline = null;

    public bool IsSuccessfullyCompleted = false;

    void Start() {
        ObjectiveColor = new Vector4(0.145098f, 0.7176471f, 1f, 1f);
        if(Outline==null)
        {
            Outline = GetComponent<Outline>();
        }
    }

    public void ShowInteractingcolor()
    {
        Outline.enabled = true;
        Outline.OutlineColor = InteractingColor;
    }

    public void ShowSuccessColor()
    {
        Outline.enabled = true;
        Outline.OutlineColor = ActivatingColor;
    }

    public void ShowObjectiveColor()
    {
        Outline.enabled = true;
        Outline.OutlineColor = ObjectiveColor;
    }

    public void DisableOutline()
    {
        Outline.enabled = false;
    }

}
