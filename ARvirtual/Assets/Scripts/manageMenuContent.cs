using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageMenuContent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPanel_Selling(){
        UImaneger.currentUIpanel=UImaneger.uiPanels.selling;
    }
     public void SetPanel_addProduct(){
        UImaneger.currentUIpanel=UImaneger.uiPanels.addProduct;
    }
     public void SetPanel_menu(){
        UImaneger.currentUIpanel=UImaneger.uiPanels.menu;
    }
    public void SetPanel_signIn(){
        UImaneger.currentUIpanel=UImaneger.uiPanels.signIn;
    }
    public void SetPanel_register(){
        UImaneger.currentUIpanel=UImaneger.uiPanels.registration;
    }
}
