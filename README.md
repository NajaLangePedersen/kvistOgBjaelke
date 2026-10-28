Compulsory assignment


**2.1 VLAN plan**

<img width="733" height="988" alt="billede" src="https://github.com/user-attachments/assets/3c7988c4-906d-4840-9580-ac582257218d" />

We have decided to make room for growth, by adding 50% on top of the amount of devices the company needs now. 
Since the client VLAN needs to be 570, and the server VLAN needs to be 571, we have decided that all VLANs are numbered in accordance with them. This means that the subnets are in the 10.5.x.0 network range.
The Network Equipment VLAN (99) is 10.5.99.0/28, to maintain a dedicated and easily identifiable subnet for network infrastructure devices. 



**2.2**

ubuntu server install
<img width="1151" height="510" alt="Skærmbillede 2026-10-28 kl  11 56 28" src="https://github.com/user-attachments/assets/b414f08f-5722-4a09-a1d4-7a02f687cdee" />

**2.3**

Client ip adresses:
<img width="973" height="346" alt="billede" src="https://github.com/user-attachments/assets/adeb8a82-5984-4009-bf3b-1479449b6094" />

Client connection to server:
<img width="1320" height="837" alt="billede" src="https://github.com/user-attachments/assets/47fa789a-7454-4e71-9d77-cac9612752bb" />

Showing that we can log in from our client vm:
<img width="712" height="391" alt="billede" src="https://github.com/user-attachments/assets/62cceb09-6ebe-4d2e-8357-0123cb88c9e5" />


**Reflections:**
Problems:
One problem we encountered was an incorrect file path in our Dockerfile. The Docker build failed because it could not find the project file at the specified location. We investigated the folder structure and discovered that the path in the Dockerfile did not match the actual location of the project. After correcting the path, (and downloading buildx) we were able to build the Docker image successfully.
final:
