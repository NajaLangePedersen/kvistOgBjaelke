Compulsory assignment


**2.1 VLAN plan**

<img width="733" height="988" alt="billede" src="https://github.com/user-attachments/assets/3c7988c4-906d-4840-9580-ac582257218d" />

We have decided to make room for growth, by adding 50% on top of the amount of devices the company needs now. 
Since the client VLAN needs to be 570, and the server VLAN needs to be 571, we have decided that all VLANs are numbered in accordance with them. This means that the subnets are in the 10.5.x.0 network range.
The Network Equipment VLAN (99) is 10.5.99.0/28, to maintain a dedicated and easily identifiable subnet for network infrastructure devices. 



**2.2**

ubuntu server install

<img width="731" height="240" alt="Skærmbillede 2026-10-28 kl  12 24 16" src="https://github.com/user-attachments/assets/bbd4120f-46ee-4cf7-a4f0-aad6577c8e2b" />

Ubuntu: Show a picture that ubuntu is installed and is running on version 26.04.1

<img width="1151" height="510" alt="Skærmbillede 2026-10-28 kl  11 56 28" src="https://github.com/user-attachments/assets/b414f08f-5722-4a09-a1d4-7a02f687cdee" />

IP config has the adress 10.5.71.19/24 and subnet 10.5.71.0/24
Docker: "Docker lists the kvist-server image and the running container"
Container "the kvist-server contaier is running with status Up."
Port mapping "Docker maps server port 8000 to container port 8080."

**2.3**

Client ip adresses:

<img width="973" height="346" alt="billede" src="https://github.com/user-attachments/assets/adeb8a82-5984-4009-bf3b-1479449b6094" />

IP config: shows the ip adress is on 10.5.70.136/24 and subnet 10.5.70.0/24

Client connection to server:
<img width="1320" height="837" alt="billede" src="https://github.com/user-attachments/assets/47fa789a-7454-4e71-9d77-cac9612752bb" />

Showing that we can log in from our client vm:
<img width="712" height="391" alt="billede" src="https://github.com/user-attachments/assets/62cceb09-6ebe-4d2e-8357-0123cb88c9e5" />

**Firewall and communication between VLANs**
VLAN 570 and VLAN 571 are separate networks. This is to increase the security, because users can't directly communicate with every device on the server network. It also creates a logical separation, since 571 is for the server, while 570 is for client devices like laptops. 

Communication between VLANs require routing, since a device on one VLAN, only knows how to communicate with other devices on that VLAN. When a client on VLAN 570 wants to connect to the login server on 571, the traffic must be routed between the two networks. 

The firewall controls which traffic is allowed between networks. In this project, the firewall has been configured to allow communication between VLANs 570 and 571. 
The firewall helps protect the server network from unauthorized access and limits communication to only the services that are required.

Allowing to specific ports is beneficial in this project, since clients need access to the server to log in. However, they don't need access to all ports belonging to the server. This is why we had to choose a port between 8000-8100, to expose a port that the client can communicate with. 

**Reflections:**

We learned how security and infrastructure work together: VLANs help separate clients and servers, while firewall rules control which traffic is allowed between them.

One problem we ran into was an incorrect file path in our Dockerfile, which caused the Docker build to fail. We corrected the path (and downloaded buildx), and then it worked. This taught us the importance of checking configurations.

We also gained experience setting up a server, deploying an application with Docker, and making it accessible from a client on another VLAN. We learned that several things need to work together for the application to be accessible. If we were to deploy the solution in a real company, we would focus more on security by using HTTPS, keeping the system updated, and making regular backups.
