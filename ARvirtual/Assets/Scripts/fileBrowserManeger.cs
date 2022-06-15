using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class fileBrowserManeger : MonoBehaviour
{
    public static Transform uiComp_container;
    public static GameObject ui_component;
    public  Transform _init_uiComp_container;
    public  GameObject _init_ui_component;
    static string requestedFilePath_UI;
    internal static string filepath;
    internal static string defualtDirPath;
    public static bool targetIs_image;
    public static bool targetIs_model;
    public static string targetExtention_image=".jpg";         //".png/.jpg";
    public static string targetExtention_model=".gltf";        //".gltf/.glb";
    
     public void getFilePath(){
        //file explorer
        //get file path from browser
         string tmp=Application.dataPath;
        filepath=tmp;
        DirectoryInfo df=new DirectoryInfo(filepath);
        FileInfo[] info=df.GetFiles();
        foreach(FileInfo file in df.GetFiles()){
            print(info.Length);
            print(file.Name);
        }
    }
    public void BrowserStartUI_target_image(){
        //initialize all filebrouser data hear
        //change ui panel context to FileBrowser
        targetIs_model=false;
        targetIs_image=true;
        UImaneger.currentUIpanel=UImaneger.uiPanels.fileBrowser;
        //get root or defalt dir path to start ui browser
        IntializeBrowserDIR();
        SwitchCurrentDir(defualtDirPath);
        //BrowserBuildUI_dir();  not needed are called in switch current dir
       // BrowserBuildUI_fiels();
        

    }
    public void BrowserStartUI_target_model(){
        targetIs_model=true;
        targetIs_image=false;
        UImaneger.currentUIpanel=UImaneger.uiPanels.fileBrowser;
        IntializeBrowserDIR();
        SwitchCurrentDir(defualtDirPath);
    }
    public static void BrowserCleanUI(){
        //clene previuse data del gameobjs
        //dile pathe is changerd---call broBuildUI() to rebuild
        FileBrowserUiComponent[] brv_uiComps=uiComp_container.GetComponentsInChildren<FileBrowserUiComponent>();
        foreach(FileBrowserUiComponent ga in brv_uiComps){
            Destroy(ga.gameObject);
        }
        
    }
    internal static void BrowserBuildUI_fiels(){
         DirectoryInfo df=new DirectoryInfo(filepath);
        FileInfo[] info=df.GetFiles();
        foreach(FileInfo file in df.GetFiles()){
            print(info.Length);
            print(file.Name);
            GameObject _newUiComp;
            _newUiComp= Instantiate(ui_component,uiComp_container);
            FileBrowserUiComponent _tmpComp=_newUiComp.GetComponent<FileBrowserUiComponent>();
            _tmpComp.SeFiletData(file.Name,filepath);
            
        }
    }
    internal static void BrowserBuildUI_dir(){
        DirectoryInfo d=new DirectoryInfo(filepath);
        
       
       DirectoryInfo[] dirIn= d.GetDirectories();
        foreach(FileSystemInfo dir in d.GetFileSystemInfos()){
            print(dirIn.Length);
            print(dir.Name);
            
            GameObject _newUiComp;
            _newUiComp= Instantiate(ui_component,uiComp_container);
            FileBrowserUiComponent _tmpComp=_newUiComp.GetComponent<FileBrowserUiComponent>();
            _tmpComp.SetDirData(dir.Name,dir.FullName);
            
           
        }
    }
        internal static void BrowserBuildUI_sys(){
        DirectoryInfo d=new DirectoryInfo(filepath);
       DirectoryInfo[] dirIn= d.GetDirectories();
        foreach(DirectoryInfo dir in d.GetDirectories()){
            print(dirIn.Length);
            print(dir.Name);
        
            GameObject _newUiComp;
            _newUiComp= Instantiate(ui_component,uiComp_container);
            FileBrowserUiComponent _tmpComp=_newUiComp.GetComponent<FileBrowserUiComponent>();
            _tmpComp.SetDirData(dir.Name,dir.FullName);
            
           
        }
    }
     public static void SwitchCurrentDir(string _newDirContxt){
        
        BrowserCleanUI();
        filepath=_newDirContxt;
        FileBr_compRefTracker.currentFIlePath=filepath;//-------temp---use global refrence for fileBRTracker
        BrowserBuildUI_dir();
        BrowserBuildUI_fiels();
    }
    internal void IntializeBrowserDIR(){
        //set defalt dir pathe in run time ---canot be called on start and awake
        //must be called befor browser ui
        #if UNITY_EDITOR_WIN
            defualtDirPath=Application.dataPath;
        #endif
        #if UNITY_ANDROID
           //defualtDirPath="/storage/emulated/0";
           defualtDirPath=Application.persistentDataPath;
        #endif
        //defualtDirPath=Application.dataPath;
        if(_init_ui_component!=null&&_init_uiComp_container!=null){
            ui_component=_init_ui_component;
            uiComp_container=_init_uiComp_container;
            
        }else{
            print("resorce not loaded to ininst..");
        }
        FileBr_compRefTracker.currentFIlePath=filepath;//-------temp---use global refrence for fileBRTracker
    }
    public static void set_requestedFilePath(string _responseFilePath){
        requestedFilePath_UI=_responseFilePath;
        print("response...filepath : "+requestedFilePath_UI);
        //acction completterd return or goto new uiPanel
        UImaneger.currentUIpanel=UImaneger.uiPanels.profile;


    }
    public static string get_requestedFilePath(){
        return requestedFilePath_UI;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    internal static string DirPath_return(){
        //go to paarent directory stoping at root
        //method)->subtract characters from string until the character "/" then returne as the new path
        string _sample=filepath;
        string _result;
        int _ind;
        char c='\\';
        _ind=_sample.LastIndexOf('/');
        Debug.Log(_sample.LastIndexOf('/')) ;
        if(_ind<0)_ind=0;
        if(_ind==0)_ind=_sample.LastIndexOf(c);
        _result=_sample.Remove(_ind);
        Debug.Log(_result) ;
        
        return _result;
    }
    public static void fileBrowser_UI_state_handler(){
         if(Input.GetButtonDown("Cancel")){//------------>temporary condition
           //triger dir path return
           //DirPath_return();
           SwitchCurrentDir(DirPath_return());
        }
        
    }
}
