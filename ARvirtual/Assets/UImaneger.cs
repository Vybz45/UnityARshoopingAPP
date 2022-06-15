using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class UImaneger : MonoBehaviour
{
    
    public GameObject singnIn_UIpanel;
    public GameObject registration_UIpanel;
    public GameObject profile_UIpanel;
    public GameObject home_UIpanel;
    public GameObject fileBrowser_UIpanel;
    public GameObject menu_UIpanel;
    public GameObject selling_UIpanel;
    public GameObject addProduct_selling_uiPanel;

    public enum uiPanels{
        signIn,
        registration,
        home,menu,selling,addProduct,
        profile,
        fileBrowser,
        none
    }
    public static uiPanels currentUIpanel;
    uiPanels previousUIpan;
    // Start is called before the first frame update

    void Start()
    {
        HandelPermissions();
        currentUIpanel=uiPanels.home;
        //currentUIpanel=uiPanels.signIn;
        previousUIpan=uiPanels.none;
        UpdateUIpanels();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentUIpanel!=previousUIpan){
            UpdateUIpanels();
            previousUIpan=currentUIpanel;
        }
        ReturnToPrevUIpanel();
        HandelUIcallsFromPanels();
    }
    internal void HandelPermissions(){
        if(Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)){
            //procced to next phase
        }else{
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
        if(Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)){
            //procced to next phase
        }else{
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }
    public void UpdateUIpanels(){
       if(currentUIpanel==uiPanels.signIn){
           disablelALLpanels_temp();
           singnIn_UIpanel.SetActive(true);
           

       }else if(currentUIpanel==uiPanels.registration){
           disablelALLpanels_temp();
           registration_UIpanel.SetActive(true);
       }else if(currentUIpanel==uiPanels.fileBrowser){
           disablelALLpanels_temp();
           fileBrowser_UIpanel.SetActive(true);
           
       }else if(currentUIpanel==uiPanels.profile){
           disablelALLpanels_temp();
           profile_UIpanel.SetActive(true);

       }else if(currentUIpanel==uiPanels.home){
           disablelALLpanels_temp();
           home_UIpanel.SetActive(true);
       }else if(currentUIpanel==uiPanels.selling){
           disablelALLpanels_temp();
           selling_UIpanel.SetActive(true);
       }else if(currentUIpanel==uiPanels.menu){
           disablelALLpanels_temp();
           menu_UIpanel.SetActive(true);
       }else if(currentUIpanel==uiPanels.addProduct){
           disablelALLpanels_temp();
           addProduct_selling_uiPanel.SetActive(true);
       }else{
           disablelALLpanels_temp();
       }
    }
    void disablelALLpanels_temp(){
        bool _key=false;
        singnIn_UIpanel.SetActive(_key);
        registration_UIpanel.SetActive(_key);
        fileBrowser_UIpanel.SetActive(_key);
        profile_UIpanel.SetActive(_key);
        home_UIpanel.SetActive(_key);
        menu_UIpanel.SetActive(_key);
        selling_UIpanel.SetActive(_key);
        addProduct_selling_uiPanel.SetActive(_key);

    }
    public void SetUIpanel_register(){
        currentUIpanel=uiPanels.registration;
        UpdateUIpanels();
    }
    void ReturnToPrevUIpanel(){
        if(Input.GetButtonDown("Cancel")&&currentUIpanel!=uiPanels.fileBrowser){//------------>temporary condition
            currentUIpanel=uiPanels.home;
           // UpdateUIpanels();
        }
    }
    public void SetUiPanel_fileBrowser(){

    }
    public void HandelUIcallsFromPanels(){
        fileBrowserManeger.fileBrowser_UI_state_handler();
    }
}
