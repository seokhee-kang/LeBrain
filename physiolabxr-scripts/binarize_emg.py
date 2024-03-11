from physiolabxr.scripting.RenaScript import RenaScript


class emg_script(RenaScript):
    def __init__(self, *args, **kwargs):
        """
        Please do not edit this function
        """
        super().__init__(*args, **kwargs)

    # Start will be called once when the run button is hit.
    def init(self):
        print('init function is called')
        pass

    # loop is called <Run Frequency> times per second
    def loop(self):
        
        data = self.inputs.get_data('obci_emg')
        timestamps = self.inputs.get_timestamps('obci_emg')

        # number of channels to keep
        nb_channels = self.params.get('nb_channels', 1)

        # keep the first nb_channels channels 
        data = data[:nb_channels]

        # for each channel, binarize the value: 1 if above the given threshold
        for i in range(nb_channels):
            # there is a threshold for each channel
            data[i] = [1 * (x >= self.params.get(f'threshold_{i}', 0.5)) for x in data[i]]

        # output data is boolean
        self.set_output(stream_name="emg_bin", data=data, timestamp=timestamps)

        self.inputs.clear_buffer_data()

    # cleanup is called when the stop button is hit
    def cleanup(self):
        print('Cleanup function is called')
