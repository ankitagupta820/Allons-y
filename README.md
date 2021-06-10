
# CSCI526

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
  
