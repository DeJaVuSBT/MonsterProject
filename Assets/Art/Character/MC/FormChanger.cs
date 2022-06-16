using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class FormChanger : MonoBehaviour
{
    private List<SpriteResolver> _resolvers;
    private UnitType _type;
    private UnitType _oldtype;
    public UnitType SetType { get { return _type; } set { _type = value; } }
    private void Start()
    {
        _resolvers = GetComponentsInChildren<SpriteResolver>().ToList();
        _oldtype = _type;
    }

    // Update is called once per frame
    void Update()
    {
        if (_oldtype!=_type)
        {
            ChangeForm();
            _oldtype = _type;
        }
    }



    private void ChangeForm()
    {
        
        foreach (var resolver in _resolvers)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(), _type.ToString());
        }
    }

    public enum UnitType
    {
        NL,
        NR,
        G,
        E
    }
}
