import numpy as np

from physiolabxr.scripting.RenaScript import RenaScript

from physiolabxr.utils.buffers import DataBuffer

import tensorflow as tf
#from keras.models import load_model
from keras.models import Sequential
from keras.layers import LSTM, Dense, Dropout


class merge_models(RenaScript):
    def __init__(self, *args, **kwargs):
        """
        Please do not edit this function
        """
        super().__init__(*args, **kwargs)

        self.data_buffer = DataBuffer()

    # Start will be called once when the run button is hit.
    def init(self):
        print('init function is called')
        pass

    # The sampling frequency is 250 Hz. So it takes 10s to get 2,500 samples.
    # With a Run Frequency of 1 run/s, we should have 250 samples each time
    
    # loop is called <Run Frequency> times per second
    def loop(self):
        
        weapon_data = self.inputs.get_data('emg_bin')
        weapon_timestamps = self.inputs.get_timestamps('emg_bin')

        LR_data = self.inputs.get_data('eeg_bin')
        LR_timestamps = self.inputs.get_timestamps('eeg_bin')

        ssvep_data = self.inputs.get_data('ssvep_bin')
        ssvep_timestamps = self.inputs.get_timestamps('ssvep_bin')

        # TODO: downsample/upsample to have the same number of timestamps for each signal source
        # stream them into different channels
        data = [0]
        timestamps = [0]

        self.set_output(stream_name="", data=data, timestamp=timestamps)

        self.inputs.clear_buffer_data()

    # cleanup is called when the stop button is hit
    def cleanup(self):
        print('Cleanup function is called')
