using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLSLInput : MonoBehaviour
{
    /*
    // TODO: where do we provide the name of the LSL?!?
    // I think we just add two parameters to resolve_stream method: "name" and <name-of-lsl>

    public string StreamType = "EEG";
    private int channelCount = 1;

    public float scaleInput = 0.1f;

    public readonly List<string> weaponsList = new List<string> { "Katana", "Gun", "Rifle" };
    public int currentWeaponIndex = 0;
    private int previousKey = 0;
    private bool keyJustPressed = false;

    StreamInfo[] streamInfos;
    StreamInlet streamInlet;

    int[] sample;

    void Update()
    {
        if (streamInlet == null)
        {

            // example: [DllImport("PluginName")] private static extern float ExamplePluginFunction();
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
            sample = new int[channelCount];
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
            this.currentWeaponIndex = (this.currentWeaponIndex + 1) % this.weaponsList.Count;
            this.keyJustPressed = false;
        }
    }
    void Process(int[] newSample, double timeStamp)
    {

        // TODO: check everything
        foreach (int x in newSample)
        {
            if (previousKey == 0 && x == 1)
            {
                this.keyJustPressed = true;
            }
        this.previousKey = x;
        }

    }
    */
}
