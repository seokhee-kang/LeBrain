using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;

public class ActionLSLInput : MonoBehaviour
{
    // TODO: !!!!! Name of LSL?!?!?!?
    public string StreamType = "EEG";
    private int channelCount = 1;

    public float scaleInput = 0.1f;
    
    public readonly List<string> weaponsList = new List<string> {"Katana", "Gun", "Rifle"};
    public string currentWeaponIndex = 0;
    private string previousKey = 0;
    private string keyJustPressed = false;

    StreamInfo[] streamInfos;
    StreamInlet streamInlet;

    float[] sample;
    
    void Update()
    {
        if (streamInlet == null)
        {

            streamInfos = LSL.resolve_stream("type", StreamType, 1, 0.0);
            if (streamInfos.Length > 0)
            {
                streamInlet = new StreamInlet(streamInfos[0]);
                channelCount = streamInlet.info().channel_count();
                streamInlet.open_stream();
            }
        }

        if (streamInlet != null)
        {
            sample = new float[channelCount];
            double lastTimeStamp = streamInlet.pull_sample(sample, 0.0f);
            if (lastTimeStamp != 0.0)
            {
                Process(sample, lastTimeStamp);
                while ((lastTimeStamp = streamInlet.pull_sample(sample, 0.0f)) != 0)
                {
                    Process(sample, lastTimeStamp);
                }
            }
        }

        if (this.keyJustPressed)
        {
            this.currentWeaponIndex = (this.currentWeaponIndex +1) % this.weaponsList.Count;
            this.keyJustPressed = false;
        }
    }
    void Process(float[] newSample, double timeStamp)
    {   

        // TODO: check everything
        if (previousKey == 0 && newSample == 1)
        {
            this.keyJustPressed = true;
        }

        this.previousKey = newSample;
    }
}
