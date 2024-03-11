import numpy as np

from physiolabxr.scripting.RenaScript import RenaScript

import tensorflow as tf
#from keras.models import load_model
from keras.models import Sequential
from keras.layers import LSTM, Dense, Dropout


class eeg_decoder(RenaScript):
    def __init__(self, *args, **kwargs):
        """
        Please do not edit this function
        """
        super().__init__(*args, **kwargs)

        print("just about to load the model")
        # Prepare sequences of 2500 rows each
        self.sequence_length = 2500
        self.num_features = 8

        self.num_classes = 4
        # 0: no activity    1: katana    2: pistol    3: rifle

        self.model = Sequential([
            LSTM(128, return_sequences=True, input_shape=(self.sequence_length, self.num_features)),
            Dropout(0.2),
            LSTM(64, return_sequences=True),
            Dropout(0.2),
            LSTM(32),
            Dense(32, activation='relu'),
            Dropout(0.2),
            Dense(self.num_classes, activation='sigmoid')
        ])
        self.model.load_weights("./model/emg_weights.h5")
        print('model loaded!')

    # Start will be called once when the run button is hit.
    def init(self):
        print('init function is called')
        pass

    # The sampling frequency is 250 Hz. So it takes 10s to get 2,500 samples.
    # With a Run Frequency of 1 run/s, we should have 250 samples each time
    
    # loop is called <Run Frequency> times per second
    def loop(self):
        
        data = self.inputs.get_data('obci_emg')
        timestamps = self.inputs.get_timestamps('obci_emg')

        # number of channels to keep
        nb_channels = self.params.get('nb_channels', 8)

        # keep the first nb_channels channels, by default 8
        data = data[:nb_channels]

        data = np.expand_dims(data.transpose(1,0), 0)

        data = self.model.predict(tf.convert_to_tensor(data), verbose=0)

        data = data.reshape(1)
        #print(data)

        if data >= .7:
            data = [1.0]
        elif data <= .3:
            data = [0.0]
        else:
            data = [0.5]

        # output data is float
        self.set_output(stream_name="emg_bin", data=data, timestamp=timestamps[-1:])

        self.inputs.clear_buffer_data()

    # cleanup is called when the stop button is hit
    def cleanup(self):
        print('Cleanup function is called')
