Was not able to upload in schedule due to lack of internet connection. But I kept a general timeline of development milestones.
###Devlog timeline:
**Day 1-2:** Wheel rotation controller and Zone management base implemented.
**Day 3-4:** ItemSO creation automated and Random asset creation and RewardSelection was handled
**Day 5-6:** UI has been adjusted to work in different aspects and Inventory management was handled. 
**Day 7:** Uploaded the progress and created the github repository.

###Architecture and Implementation
**Event-Driven Systems:** Utilized C# Action events to create a decoupled, modular gameplay loop.
**Centralized State:** Implemented a GameFlowManager to orchestrate various independent scripts and manage the overarching game state.
**Automated Scripting:** Successfully automated the ItemSO generation process to parse and construct data entries directly from the provided asset folders.
**Asset Data Quirk:** The provided art folder contained visually identical sprites saved under different filenames. Because the automation tool correctly reads different filenames as distinct items, it generated separate ItemSO files for them. Consequently, they populate as separate unique entries in the inventory rather than stacking. The core inventory logic functions perfectly; this simply reflects the duplicate data in the raw assets.

###Polishable Functions: 
Using JSON infrastructure for save data.
