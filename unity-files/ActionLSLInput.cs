using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;
public class ActionLSLInput : MonoBehaviour
{
    
    // TODO: where do we provide the name of the LSL?!?
    // I think we just add two parameters to resolve_stream method: "name" and <name-of-lsl>

    public string StreamName = "emg_bin";
    // public string StreamType = "EEG";
    private int channelCount = 1;

    ContinuousResolver resolver;

    double max_chunk_duration = 0.2;  // Duration, in seconds, of buffer passed to pull_chunk. This must be > than average frame interval.

    // public float scaleInput = 0.1f;
    private StreamInlet inlet;

    // StreamInfo[] streamInfos;
    public readonly List<string> weaponsList = new List<string> { "Katana", "Gun", "Rifle" };
    public int currentWeaponIndex = 0;
    private int previousKey = 0;
    private bool keyJustPressed = false;

    // We need buffers to pass to LSL when pulling data.
    private float[,] data_buffer;  // Note it's a 2D Array, not array of arrays. Each element has to be indexed specifically, no frames/columns.
    private double[] timestamp_buffer;
    
    // int[] sample;

    void Start()
        {
            if (!StreamName.Equals(""))
            {
                Debug.Log("stream name " + StreamName);
                resolver = new ContinuousResolver("name", StreamName);
            }
            else
            {
                Debug.LogError("Object must specify a name for resolver to lookup a stream.");
                this.enabled = false;
                return;
            }
            StartCoroutine(ResolveExpectedStream());
        }
    
    IEnumerator ResolveExpectedStream()
    {

        var results = resolver.results();
        while (results.Length == 0)
        {
            yield return new WaitForSeconds(.1f);
            results = resolver.results();
        }

        inlet = new StreamInlet(results[0]);

        // Prepare pull_chunk buffer
        int buf_samples = (int)Mathf.Ceil((float)(inlet.info().nominal_srate() * max_chunk_duration));
        Debug.Log("Allocating buffers to receive " + buf_samples + " samples.");
        int n_channels = inlet.info().channel_count();
        data_buffer = new float[buf_samples, n_channels];
        timestamp_buffer = new double[buf_samples];
    }

    // Update is called once per frame
    void Update()
    {
        if (inlet != null)
        {
            int samples_returned = inlet.pull_chunk(data_buffer, timestamp_buffer);
            Debug.Log("Samples returned: " + samples_returned);
            if (samples_returned > 0)
            {
                // There are many things you can do with the incoming chunk to make it more palatable for Unity.
                // Note that if you are going to do significant processing and feature extraction on your signal,
                // it makes much more sense to do that in an external process then have that process output its
                // result to yet another stream that you capture in Unity.
                // Most of the time we only care about the latest sample to get a visual representation of the latest
                // state, so that's what we do here: take the last sample only and use it to udpate the object scale.
                float newSample = data_buffer[samples_returned - 1, 0];
                // float channel1 = data_buffer[samples_returned - 1, 1];
                // float channel2 = data_buffer[samples_returned - 1, 2];
                
                    if (this.previousKey == 0 && newSample == 1)
                    {
                        this.keyJustPressed = true;
                    }
                
            }
        }
    }


/*    void Update()
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
            if (this.previousKey == 0 && x == 1)
            {
                this.keyJustPressed = true;
            }
        this.previousKey = x;
        }

    }
*/
    
}
