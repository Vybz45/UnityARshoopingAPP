using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileBr_compRefTracker : MonoBehaviour
{
    public static string currentFIlePath;
    public Text show_filePath_fb_ref;
    // Start is called before the first frame update
    void Start()
    {
         update_FB_pathUI();//------>>>temporary multiple udates in use
    }

    // Update is called once per frame
    void Update()
    {
        update_FB_pathUI();//------>>>temporary multiple udates in use
    }
    public void update_FB_pathUI(){
        if(currentFIlePath!=show_filePath_fb_ref.text){
            show_filePath_fb_ref.text=currentFIlePath;
        }
    }
    public void closeFB_ui(){
        fileBrowserManeger.BrowserCleanUI();
        UImaneger.currentUIpanel=UImaneger.uiPanels.addProduct;
    }
}
