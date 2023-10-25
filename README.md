# CSCI_4126: Ubicomp - A2

## Git-Hub link: https://github.com/Santi-Rijal/CSCI_4126/tree/Assignment2

#### ASSIGNMENT1 folder is Client and Assignment2 folder is Server

## Instructions

### *MAKE SURE YOU ARE RUNNING BOTH THE CLIENT AND SERVER ON THE SAME NETWORK*

### Setup
1) Have the Assignment2.exe file running (Included in the Zip submission, it's inside the Server folder).
2) Clone the repo to your local machine.
3) Open ASSIGNMENT1 folder in unity
4) If it doesn't open the Client scene directly open it through the project directory under Scenes.
5) Click on the NetworkManager object on the hierarchy.
6) On the Inspector in the Network Manager script, enter your IP in the Ip field.
7) Save
8) Build and run the Client.

### IP on Windows
1) Select Start > Settings > Network & internet > Wi-Fi and then select the Wi-Fi network you're connected to.
2) Under Properties, look for your IP address listed next to IPv4 address.

### IP on MAC
1) Open the Apple menu and click System Settings. 
2) Click Network in the left panel and then select Wi-Fi or Ethernet (for wired connections). Click Details next to the network you're connected to. 
3) Scroll down to see your Mac's local IP address

#### If Scripts Missing Warning in Client (Sometimes it removes it for some reasons)
1) Click on the NetworkManager object on the hierarchy.
2) If missing script warning, click on the circle with a dot in the middle and select the NetworkManager Script.
3) For port enter, 7777, this is the port the server will be running on, then your IP
4) Click on the Canvas object on the hierarchy.
5) If missing script warning for UIManager, click on the circle with a dot in the middle and select the UIManager Script.
6) For Spawn Point: click on the circle with a dot in the middle and type spawn, it should show up, select it.
7) For Player: Under Prefab folder, there's a player prefab, drag and drop it in the player field.
8) For the Connect UI: Drag and drop the Canvas object from hierarchy.
9) Build and run.

### Character Movement:
1) Tilt the phone forward/backward to move the character forward/backward.
2) Tilt the phone left/right to move the character left/right.
3) Doing this will display "Accelerometer" on the server screen.

### Camera Rotation
1) Turn the phone left/right to rotate or your body while facing the phone.
2) Doing this will display "Gyroscope" on the server screen.

### Zoom
1) Do a pinch gesture to zoom-in/zoom-out.
2) Doing this will display "Zoom" on the server screen.

### Step Counter
1) With the phone move around for a while.
2) The "Steps" text in red should get updated.
3) Doing this will display "Step Counter" on the server screen.
4) It gets overridden very fast my other sensors so you might not notice it.
5) You can check by looking at the logs, i am also printing what gets displayed on server.

## Sources

### Assets:
1) https://assetstore.unity.com/packages/2d/textures-materials/floors/outdoor-ground-textures-12555
2) https://assetstore.unity.com/packages/3d/environments/historic/3d-pirates-lowpoly-pack-233903
### Code:
1) https://stackoverflow.com/questions/59030399/zooming-in-unity-mobile
2) https://www.reddit.com/r/Unity3D/comments/wjrg5q/how_to_use_the_pedometerstepcounter_sensor/
3) https://www.youtube.com/watch?v=6kWNZOFcFQw&ab_channel=TomWeiland
