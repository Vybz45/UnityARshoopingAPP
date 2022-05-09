using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImaneger : MonoBehaviour
{
    public GameObject singnIn_UIpanel;
    public GameObject registration_UIpanel;
    public GameObject profile_UIpanel;
    public GameObject home_UIpanel;
    public GameObject fileBrowser_UIpanel;

    public enum uiPanels{
        signIn,
        registration,
        home,
        profile,
        fileBrowser,
        none
    }
    public static uiPanels currentUIpanel;
    uiPanels previousUIpan;
    // Start is called before the first frame update

    void Start()
    {
        currentUIpanel=uiPanels.profile;
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
    }
    public void SetUIpanel_register(){
        currentUIpanel=uiPanels.registration;
        UpdateUIpanels();
    }
    void ReturnToPrevUIpanel(){
        if(Input.GetButtonDown("Cancel")){
            currentUIpanel=previousUIpan;
           // UpdateUIpanels();
        }
    }
    public void SetUiPanel_fileBrowser(){

    }
}
