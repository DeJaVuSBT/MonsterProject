using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class FormChanger : MonoBehaviour
{
    private List<SpriteResolver> _resolvers;
    private UnitType _type;

    // Start is called before the first frame update
    private void Start()
    {
        _resolvers = GetComponentsInChildren<SpriteResolver>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _type = UnitType.E;
            ChangeForm();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _type = UnitType.NL;
            ChangeForm();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _type = UnitType.NR;
            ChangeForm();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _type = UnitType.G;
            ChangeForm();
        }
    }



    private void ChangeForm()
    {
        
        foreach (var resolver in _resolvers)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(), _type.ToString());
        }
    }

    private enum UnitType
    {
        NL,
        NR,
        G,
        E
    }
}
