"""Example program to demonstrate how to send a multi-channel time series to
LSL."""
import random
import sys
import string
import numpy as np
import time
from random import random as rand

from pylsl import StreamInfo, StreamOutlet, local_clock

import pandas as pd


def main(argv):
    srate = 250
    name = 'unicorn_eeg'
    print('Stream name is ' + name)
    type = 'EEG'
    n_channels = 8
    info = StreamInfo(name, type, n_channels, srate, 'float32', 'someuuid1234')

    df = pd.read_csv("model/test_data.csv")

    data = df.iloc[:,:-1].to_numpy().reshape(-1,8)


    # next make an outlet
    outlet = StreamOutlet(info)

    print("now sending data...")
    start_time = local_clock()
    sent_samples = 0
    while True:
        elapsed_time = local_clock() - start_time
        required_samples = int(srate * elapsed_time) - sent_samples
        
        for sample_ix in range(required_samples):
            # this is converted into a pylsl.vectorf (the data type that is expected by push_sample)
           
            mysample = data[(sent_samples+sample_ix) % len(data)]
            
            # now send it
            outlet.push_sample(mysample)
        sent_samples += required_samples
        # now send it and wait for a bit before trying again.
        time.sleep(0.01)


if __name__ == '__main__':
    main(sys.argv[1:])
