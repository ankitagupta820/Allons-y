
# CSCI526

---------- 06/10/2021 Updated ----------


I'm trying to work on this bug and found this approach, hope it works well.

What I did:

(1) In "CM vcam1" settings, change "Follow" to "Player"

<img width="399" alt="Screen Shot 2021-06-10 at 13 14 55" src="https://user-images.githubusercontent.com/60083841/121591003-dbfaa200-c9ed-11eb-9c47-2c795701f26d.png">

(2) Change "Screen Y" to a value smaller than 0.5, e.g. "0.35", to place the player at the top of the screen

<img width="374" alt="Screen Shot 2021-06-10 at 13 15 24" src="https://user-images.githubusercontent.com/60083841/121591068-eb79eb00-c9ed-11eb-94d1-be812a47af8d.png">

(3) Change "Dead Zone Width" to a value close to 1, e.g. "0.78", to limit the camera following (i.e. the camera will not follow the player until the player reaches the left/right side)

(4) Change "Dead Zone Height" to "0"

<img width="373" alt="Screen Shot 2021-06-10 at 13 15 43" src="https://user-images.githubusercontent.com/60083841/121591114-f765ad00-c9ed-11eb-959d-c10a70e99f5f.png">


There are still some bugs:

(1) when the player reaches the right side, the camera will move a little bit. (This might be related to the bound we set)


---------- 06/20/2021 Updated ----------
Sorry I haven't set up GitHub, I will first write the steps here and do set-up thing, then I will finish pushing ASAP.

Steps:


(1) Create a new transparent material, e.g. "CloudTransparent"

  a. change Rendering Mode to "Transparent"
  
  <img width="414" alt="Screen Shot 2021-06-09 at 18 45 12" src="https://user-images.githubusercontent.com/60083841/121451675-dac86700-c952-11eb-89d4-6a3d557c6cd2.png">
  
  b. change Alpha value, e.g. 0
  
(2) Modify CloudType Prefab

  a. Tag CloudType and all its children to "Cloud" tag
  
  <img width="384" alt="Screen Shot 2021-06-09 at 18 40 24" src="https://user-images.githubusercontent.com/60083841/121451280-27f80900-c952-11eb-8f50-708547ea3804.png">

  b. For each child, add collider and click "isTrigger"
  
  <img width="391" alt="Screen Shot 2021-06-09 at 18 40 12" src="https://user-images.githubusercontent.com/60083841/121451268-20d0fb00-c952-11eb-99b5-50c4cb354dda.png">

(3) Link script to Player
  
