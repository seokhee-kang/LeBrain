# Team LeBrain - Neureality 2024

Nikhil Kumar Kuppa \
Srikanth Chandar \
Seokhee Kang \
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
Use PhysioLabXR and the Python script `binarize_emg.py` to have an LSL with only one channel taking binary values (binary classification).

- Option 2: Raw signal
From the library PhysioLabXR, run `PhysioLabXR_P300Speller_Demo/PhysioLabXRP300SpellerDemoScript.py` to acquire the raw electromyography (EMG) signal straight into a Laboratory Stream Layer (LSL).
Use PhysioLabXR and the Python script `emg_decoder.py` to have an LSL with only one channel with the decoded classes.

### EEG signal
Use Unicorn to broadcast the EEG signal into an LSL.
Use PhysioLabXR and the Python script `eeg_decoder.py` to produce an LSL with only one channel with the decoded classes.

### SSVEP signal
Use Unicorn to broadcast the EEG signal into an LSL.
Use PhysioLabXR and the Python script `SSVEP_CCARNN.ipynb` and `ssvep_decoder.py` (not in the repository) to produce an LSL with the identified frequency.

### Training decoders
We used MATLAB to acquire the training data and stored it in csv files. We implemented the models in Tensorflow Keras and used Jupyter notebooks for the training process.

### Unity
Add `ActionLSLInput.cs` to the game scripts. It leverages the LSL4Unity library to read LSL streams and stores the relevant information in the class attributes. The script `PlayerControl.cs` reads the attributes of ActionLSLInput to switch the weapon. For now, we implemented the weapon switching through the binary EMG signal. Eventually, all modalities would have been integrated.



