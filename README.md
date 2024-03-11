# Team LeBrain - Neureality 2024

Nikhil Kumar Kuppa
Srikanth Chandar
Seokhee Kang
Aymeric Degroote

## Requirements

### Hardware:
- Unicorn Hybrid Black
- OpenBCI Cyton Board
- Meta Quest 2
- Windows OS

 
### Software:
- Unity Unicorn
- OpenBCI GUI
- PhysioLabXR
- Unity
- Virtual Studio
- Google Colab
- MATLAB
- Meta Quest Mobile App


## Technical pipeline

### EMG signal
- Option 1: Binary signal
Use OpenBCI GUI to acquire the electromyography (EMG) signal, normalize it into values between 0 and 1, and broadcast the data into a Laboratory Stream Layer (LSL).
Use PhysioLabXR and the python script `binarize_emg.py` to have an LSL with only one channel taking binary values (binary classification).

- Option 1: Raw signal
From the library PhysioLabXR, run `PhysioLabXR_P300Speller_Demo/PhysioLabXRP300SpellerDemoScript.py` to acquire the raw electromyography (EMG) signal straight into a Laboratory Stream Layer (LSL).
Use PhysioLabXR and the python script `emg_decoder.py` to have an LSL with only one channel with the decoded classes.

### EEG signal
Use Unicorn to broadcast the EEG signal into an LSL.
Use PhysioLabXR and the python script `eeg_decoder.py` to produce an LSL with only one channel with the decoded classes.

### SSVEP signal
Use Unicorn to broadcast the EEG signal into an LSL.
Use PhysioLabXR and the python script `ssvep_decoder.py` (not in repository) to produce an LSL with the identified frequence.

### Training decoders
After storing the data from the LSLs into csv files, the models were trained in jupyter notebooks.

### Unity
Add `ActionLSLInput.cs` to the game scripts. It uses the LSL4Unity library to read LSL streams and store the information.
The script `PlayerControl.cs` is adapted to weapon switching information (binary EMG signal). Eventually, all modality would have been integrated.


