"""
Produce random data signal into an LSL that has the same format as EEG. Useful for debuging

"""
import sys
import time
from random import random as rand

from pylsl import StreamInfo, StreamOutlet, local_clock


def main(argv):
    srate = 250
    name = 'obci_emg'
    print('Stream name is ' + name)
    type = 'EEG'
    n_channels = 8
    info = StreamInfo(name, type, n_channels, srate, 'float32', 'someuuid1234')

    # next make an outlet
    outlet = StreamOutlet(info)

    print("now sending data...")
    start_time = local_clock()
    sent_samples = 0
    while True:
        elapsed_time = local_clock() - start_time
        required_samples = int(srate * elapsed_time) - sent_samples
        
        for _ in range(required_samples):
            mysample = [rand()*1 for _ in range(n_channels)]
            
            # now send it
            outlet.push_sample(mysample)
        
        sent_samples += required_samples
        # now send it and wait for a bit before trying again.
        time.sleep(0.01)


if __name__ == '__main__':
    main(sys.argv[1:])
