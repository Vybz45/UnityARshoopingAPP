using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARManager : MonoBehaviour
{
    public Text out_objEuler;
    public Text out_attitudDevice;
    public Text out_position;
    public Text out_accelDistance;
    public Text out_distance;
    public Text out_ADA;
    public Text out_noiseGravity;
    public Text out_GravMagn;
    public InputField sensetiveetyValueInp;
    public InputField assignAxisUI;
    public GameObject cameraref;
    public Transform ref_xy_targ;
    public Transform ref_z_targ;
    public Vector3 priv_accl;
    Vector3 vi;
    Vector3 vi_ada;
    Vector3 distanceMoved;
    Gyroscope gyro;
    bool stopGlobalUpdate;
    // Start is called before the first frame update
    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
        priv_accl=Vector3.zero;
        distanceMoved=Vector3.zero;
        vi=Vector3.zero;
        vi_ada=Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        ARcamRotaion();
        if(!stopGlobalUpdate){
             ARMovement2();
        }
       
    }
    public void ARcamRotaion(){
    // we assume that the device is held parallel to the ground
    // and the Home button is in the right hand

    // remap the device acceleration axis to game coordinates:
    // 1) XY plane of the device is mapped onto XZ plane
    // 2) rotated 90 degrees around Y axis
        string logAttitudOrint="attitudeOrient: "+gyro.attitude.eulerAngles.ToString();
        string logObgEulerRot="camEulerAng: "+cameraref.transform.eulerAngles.ToString();
        

        out_attitudDevice.text=logAttitudOrint;
        out_objEuler.text=logObgEulerRot;

        cameraref.transform.eulerAngles=DeviceRotToGameRot(gyro.attitude.eulerAngles);
        //ref_xy_targ.eulerAngles=DeviceRotToGameRot(gyro.attitude.eulerAngles);
        //ref_z_targ.localEulerAngles=DeviceRotToGameRot_z(gyro.attitude.eulerAngles);

    }
    public Vector3 DeviceRotToGameRot(Vector3 _deviceRotaion){
        Vector3 deviceToGameRot=Vector3.zero;
        //assignAxisUI.text=="x"
        //float dx=(_deviceRotaion.x+90)-450;
        deviceToGameRot=new Vector3(-_deviceRotaion.x,-_deviceRotaion.y,_deviceRotaion.z);
        //deviceToGameRot=new Vector3((-_deviceRotaion.x)-(_deviceRotaion.x/4f),-_deviceRotaion.y,_deviceRotaion.z);
        return deviceToGameRot;
    }
     public Vector3 DeviceRotToGameRot_z(Vector3 _deviceRotaion){
        Vector3 deviceToGameRot=Vector3.zero;
        //assignAxisUI.text=="x"
        float dx=(_deviceRotaion.x+90)-450;
        deviceToGameRot=new Vector3(90,0,-_deviceRotaion.y);
        //deviceToGameRot=new Vector3((-_deviceRotaion.x)-(_deviceRotaion.x/4f),-_deviceRotaion.y,_deviceRotaion.z);
        return deviceToGameRot;
    }
    public void ARMovement(){
        AccelerationEvent[] accelerations= Input.accelerationEvents;
        AccelerationEvent accelerationEvent;
        string logObgPosit;
        string logAccl;
            if(accelerations!=null){
              accelerationEvent = accelerations[Input.accelerationEventCount-1];
              Vector3 dist;
              float t_pow2=Mathf.Pow(accelerationEvent.deltaTime,2);
              //d=accl*t^2
               // dist=(accelerationEvent.acceleration*9.8f)*t_pow2;
               Vector3 accelrDiffer=new Vector3();
               //accelrDiffer=accl_differance(accelerations);
                accelrDiffer=difreance(accelerations,avrActualDaccel_ADA(accelerations));

               dist=(accelrDiffer*9.8f)*t_pow2;
                dist=new Vector3(dist.x,-dist.y,-dist.z);
               cameraref.gameObject.transform.position+=dist*10;
               logObgPosit ="camPosition:eventCount:("+Input.accelerationEventCount+")... "+cameraref.transform.position.ToString();
               // logAccl="accel x 9.8f,-y ::"+dist+" ogAccel:"+accelerationEvent.acceleration;
               logAccl="accel x 9.8f,-y ::"+dist+" gyroUogAccel:"+accelrDiffer;
            }else{
                logObgPosit ="accEventNull:eventCount:("+Input.accelerationEventCount+") "+cameraref.transform.position.ToString();
                logAccl="accel not set..";
            }
            out_accelDistance.text=logAccl;
            out_position.text=logObgPosit;
    }
    public void resat_position(){
        cameraref.transform.position=new Vector3(0,0,0);
        distanceMoved=Vector3.zero;
        vi=Vector3.zero;
        stopGlobalUpdate=!stopGlobalUpdate;
    }
    public Vector3 difreance(AccelerationEvent[] _accsls,Vector3 _curr){
        Vector3 dif=new Vector3();
        int length=Input.accelerationEventCount;
        Vector3 lastAcc=priv_accl;
        Vector3 curentAcc=_curr;
        if(priv_accl!=Vector3.zero){
            //calc dif
            dif=lastAcc-curentAcc;
            priv_accl=curentAcc;
           // if(dif.sqrMagnitude>1)dif.Normalize();
        }else{
            priv_accl=avrActualDaccel_ADA(_accsls);
            dif=Vector3.zero;
        }
        
        
        return dif;

    }
    Vector3 accel_Jerk_noise(AccelerationEvent[] _acelrEvents){
       
        Vector3 accelrAverage=Vector3.zero;
        float period=0.0f;
        foreach (AccelerationEvent item in _acelrEvents)
        {
            accelrAverage+=item.acceleration;
            period+=item.deltaTime;
            
        }
        if(period>0)accelrAverage*=1.0f/period;
        /*
        if(_acelrEvents.Length>2){
            accelrAverage=accelrAverage/_acelrEvents.Length;
        }
        else{
            accelrAverage=accelrAverage/(_acelrEvents.Length+1);
        }
        */
        return accelrAverage*Time.deltaTime;

    }
    Vector3 avrActualDaccel_ADA(AccelerationEvent[] _acelrEvents){//returnes (jerk) avr of actual divice accelration ADA
        Vector3 accelrAverage=Vector3.zero;
        /*
        float period=0.0f;
        foreach (AccelerationEvent item in _acelrEvents)
        {
            //1)add all acelleration and oprate to find average acellration 
            //then subtract jerk to find clen accel value
            accelrAverage+=item.acceleration;
            //period+=item.deltaTime;
            
        }
        //if(period>0)accelrAverage*=1.0f/period;
        //2) get user acceleration subtract jerk
        */
        /*
        if(_acelrEvents.Length>2){
            accelrAverage=accelrAverage/_acelrEvents.Length;
        }
        else{
            accelrAverage=accelrAverage/(_acelrEvents.Length+1);
        }
        */
        //accelrAverage=gyro.userAcceleration;
        accelrAverage=gyro.userAcceleration;
       // accelrAverage*=Time.deltaTime;
       //Input.acceleration
        return accelrAverage;
    }

    Vector3 normalizeADA(Vector3 _actualDeAcc_ADA,out string sqrMagnitude){
        Vector3 dir=_actualDeAcc_ADA;
        string sqrMa;
        sqrMa=_actualDeAcc_ADA.magnitude.ToString();
        //returne normalized actual device accelration
        // clamp acceleration vector to the unit sphere
        if (dir.sqrMagnitude > 1){
           // dir.Normalize();
           // sqrMa=dir.sqrMagnitude.ToString();
        }
            
        else{
            //dir.Normalize();
            //sqrMa=dir.sqrMagnitude.ToString();
        }
        sqrMagnitude=sqrMa;
        return dir;
    }
    Vector3 noiseByGravity(Vector3 _normalADA){
        //returne normalized ADA multiply by one gravity(9.8)
        Vector3 noiseGravity=Vector3.zero;
        noiseGravity=_normalADA*9.8f;
        return noiseGravity;
    }
    Vector3 distanceAcceldInMS(Vector3 _actualDeviceAc_ACA){
        //ADA X gravity(9.8) on orignal device accelration
        //actual device accel is = total acelration
        //Ta=a-g/x  x=g/-Ta..taking a=0 ta=0.1 g=1 x=?
        //output a_localAccel is in term of actual/ta units

        Vector3 gravity_g=gyro.gravity;
        Vector3 ta_inTotalUserAccel=_actualDeviceAc_ACA;
        Vector3 a_localAccel=Vector3.zero;
        Vector3 xg=Vector3.zero;

        xg=-gravity_g*Vector3.Magnitude(ta_inTotalUserAccel);
        a_localAccel=ta_inTotalUserAccel-xg;
        //to increse accel unit mult by 10 for base 10
        Vector3 dis=a_localAccel;
        return dis;
    }
    Vector3 distMovedByDevice(Vector3 _accel){
        //total aceel = noise by gravity + distance acceld m/s
        //distance moved = total accel-dist
        float time=Time.deltaTime;//change of time
        float ta=Mathf.Pow(time,2);
        Vector3 vf=(_accel*time)+vi;
        
        Vector3 disp=(vi*time)+(0.5f*(_accel*ta));
        Vector3 dma=disp;
        vi=vf;//for next frame
        //dma*=1.0f/Time.deltaTime;
        float t;
        float.TryParse(sensetiveetyValueInp.text,out t );
        if(dma.magnitude>t){
            return dma;
        }else{
            dma=Vector3.zero;
        }
       // dma.Normalize();
        return dma;
    }
      Vector3 distMovedByDevice_relativADA(Vector3 _accel){
        //total aceel = noise by gravity + distance acceld m/s
        //distance moved = total accel-dist
        float time=Time.deltaTime;//change of time
        float ta=Mathf.Pow(time,2);
        Vector3 vf=(_accel*time)+vi_ada;
        
        Vector3 disp=(vi_ada*time)+(0.5f*(_accel*ta));
        Vector3 dma=disp;
        vi_ada=vf;//for next frame
        //dma*=1.0f/Time.deltaTime;
        float t;
       // dma*=time;
        float.TryParse(sensetiveetyValueInp.text,out t );
        if(dma.magnitude>t){
            return dma;
        }else{
            dma=Vector3.zero;
        }
       // dma.Normalize();
        return dma;
    }
        public void ARMovement2(){
        AccelerationEvent[] accelerations= Input.accelerationEvents;
        AccelerationEvent accelerationEvent;
        string logObgPosit;
        string logAcclDist;
        string logDistance;
        string logADA;
        string logNoise;
        string logGravMag=" ";
        string logJerk;
        
            if(accelerations!=null){
              
              Vector3 dist=Vector3.zero;
              Vector3 _dist_in_ADA=Vector3.zero;
              Vector3 displacemnt=Vector3.zero;
              //accelration due gravity and accelration due movement
               Vector3 accelrDiffer=new Vector3();
               Vector3 ADA=avrActualDaccel_ADA(accelerations);
               // Vector3 jerk=accel_Jerk_noise(accelerations);
              // Vector3 jerk=avrActualDaccel_ADA(accelerations);
              //  logJerk=jerk.ToString();
               // Vector3 resultant;
                
                
                
                //Vector3 normalAccel=normalizeADA(res2,out logGravMag);
                Vector3 noiseGrav=noiseByGravity(ADA);
                Vector3 distanceAcel=distanceAcceldInMS(ADA);
                //displacement subtract displace due gravity from disp dow move
                dist=distMovedByDevice(distanceAcel);
                _dist_in_ADA=distMovedByDevice_relativADA(distanceAcel);
                displacemnt=_dist_in_ADA;
               distanceMoved+=displacemnt;
                //displacemnt-=dist;
               
                cameraref.gameObject.transform.position=distanceMoved;
               logObgPosit ="eventCount:("+Input.accelerationEventCount+")...dist: "+dist;
               // logAccl="accel x 9.8f,-y ::"+dist+" ogAccel:"+accelerationEvent.acceleration;
               logAcclDist="distAcell:"+distanceAcel;
               logDistance="Distance::"+distanceMoved+"ada-dist:"+_dist_in_ADA;
               logADA=" ActualAccel:"+ADA+"rescrossJerGrav:";
                
               logNoise="finalDisplace:"+displacemnt+"getGravity: "+gyro.gravity;
               displacemnt=Vector3.zero;
            }else{
                logObgPosit ="accEventNull:eventCount:("+Input.accelerationEventCount+") "+cameraref.transform.position.ToString();
                logAcclDist="accel not set..";
                logDistance="Distance::------";
               logADA=" ActualAccel:------";
               logNoise="gravity:-------";
            }
            out_ADA.text=logADA;
            out_GravMagn.text=logGravMag;
            out_noiseGravity.text=logNoise;
            out_accelDistance.text=logAcclDist;
            out_distance.text=logDistance;
            out_position.text=logObgPosit;

    }
}
