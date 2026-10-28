Compulsory assignment

**1.2 Implementation**

To protect user passwords, the system doesn't store passwords in plain text. 
We are using Argon2id to hash passwords, before they are stored in the database. We have chosen Argon2id, because it is a modern password-hashing algorithm, designed to resist brute-force attacks. 
When a user attempts to log in, the entered password is hashed using the same settings, and are then compared to the stored hash. If the hashes match, the user is authenticated. 

<img width="958" height="500" alt="image" src="https://github.com/user-attachments/assets/9187d8e9-d9b9-4db6-aee5-fde665a8c0dc" />
<img width="672" height="340" alt="image" src="https://github.com/user-attachments/assets/3480c6d7-95a5-403d-82fe-d6e184d3ce40" />

These images show how the passwords are hashed using Argon2id. We have decided to create the method ComputeHash to avoid duplicate code, since it also has to be used when verifying passwords. 


---------------------------------------------------------------------------------------------------------------------------------------------------------------


**2.1.1 VLAN plan**


<img width="733" height="988" alt="billede" src="https://github.com/user-attachments/assets/3c7988c4-906d-4840-9580-ac582257218d" />

We have decided to make room for growth, by adding 50% on top of the amount of devices the company needs now. 
Since the client VLAN needs to be 570, and the server VLAN needs to be 571, we have decided that all VLANs are numbered in accordance with them. This means that the subnets are in the 10.5.x.0 network range.
The Network Equipment VLAN (99) is 10.5.99.0/28, to maintain a dedicated and easily identifiable subnet for network infrastructure devices. 

**2.1.2**

Subnetting examples:

VLAN 572 - Network printers

VLAN 572 is used for the company's network printers. We chose the subnet 10.5.72.0/28 because there are currently 3 printers, with a possible increase to 5 in the future. A /28 subnet provides 16 IP addresses in total, of which 14 can be assigned to devices. This gives us enough addresses for the printers, the gateway, and some additional devices without wasting too many addresses.
The subnet mask is 255.255.255.240. The network address is 10.5.72.0, the usable host addresses range from 10.5.72.1 to 10.5.72.14, and the broadcast address is 10.5.72.15. We use 10.5.72.1 as the default gateway and static IP addresses for the printers, so their addresses remain predictable. The printers have their own VLAN to separate them from employee devices and reduce security risks if a printer is compromised.

VLAN 573 - IP Phones

VLAN 573 is used for the company's IP phones. We chose the subnet 10.5.73.0/27 because there are currently 12 phones, with a possible increase to 18. A /27 subnet provides 32 IP addresses in total, with 30 usable host addresses. This is enough for the phones, the gateway, and future expansion.
The subnet mask is 255.255.255.224. The network address is 10.5.73.0, the usable host addresses range from 10.5.73.1 to 10.5.73.30, and the broadcast address is 10.5.73.31. We use 10.5.73.1 as the default gateway and DHCP to assign IP addresses automatically to the phones. Keeping IP phones in a separate VLAN separates voice traffic from regular data traffic and can make the network easier to manage.

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

This picture shows that the client VM on VLAN 570 can access the login platform’s Swagger UI on the server at 10.5.71.19:8000.

Showing that we can log in from our client vm:

<img width="1633" height="921" alt="billede" src="https://github.com/user-attachments/assets/1c84fac1-884d-46d9-bd8e-d838e360e0f3" />

<img width="712" height="391" alt="billede" src="https://github.com/user-attachments/assets/62cceb09-6ebe-4d2e-8357-0123cb88c9e5" />

From the Ubuntu client on VLAN 570, the login endpoint on the server returned HTTP 200 and issued a JWT. The token was then added to Swagger for authenticated API requests.

This picture shows that the client VM on VLAN 570 can access the login platform’s Swagger UI on the server at 10.5.71.19:8000, and use the login method in swagger to login.

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
