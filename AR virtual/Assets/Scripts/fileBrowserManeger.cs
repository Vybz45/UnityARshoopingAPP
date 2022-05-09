using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class fileBrowserManeger : MonoBehaviour
{
    public static Transform uiComp_container;
    public static GameObject ui_component;
    public  Transform _init_uiComp_container;
    public  GameObject _init_ui_component;
    static string requestedFilePath_UI;
    static string filepath;
    string defualtDirPath;
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
    public void BrowserStartUI(){
        //initialize all filebrouser data hear
        //change ui panel context to FileBrowser
        UImaneger.currentUIpanel=UImaneger.uiPanels.fileBrowser;
        //get root or defalt dir path to start ui browser
        IntializeBrowserDIR();
        SwitchCurrentDir(defualtDirPath);
        BrowserDir();
        BrowserBuildUI();
        

    }
    public static void BrowserUpdateUI(){
        //clene previuse data del gameobjs
        //dile pathe is changerd---call broBuildUI() to rebuild
        BrowserBuildUI();
    }
    static void BrowserBuildUI(){
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
    void BrowserDir(){
        DirectoryInfo d=new DirectoryInfo(filepath);
       DirectoryInfo[] dirIn= d.GetDirectories();
        foreach(DirectoryInfo dir in d.GetDirectories()){
            print(dirIn.Length);
            print(dir.Name);
            GameObject _newUiComp;
            _newUiComp= Instantiate(ui_component,uiComp_container);
            FileBrowserUiComponent _tmpComp=_newUiComp.GetComponent<FileBrowserUiComponent>();
            _tmpComp.SetDirData(dir.Name,filepath);
           
        }
    }
    public static void SwitchCurrentDir(string _newDirContxt){
        filepath=_newDirContxt;
    }
    void IntializeBrowserDIR(){
        //set defalt dir pathe in run time ---canot be called on start and awake
        //must be called befor browser ui
        defualtDirPath=Application.dataPath;
        if(_init_ui_component!=null&&_init_uiComp_container!=null){
            ui_component=_init_ui_component;
            uiComp_container=_init_uiComp_container;
        }else{
            print("resorce not loaded to ininst..");
        }
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
}
