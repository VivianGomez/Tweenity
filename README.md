<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![MIT License][license-shield]][license-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO 
<br />
<div align="center">
  <a href="https://github.com/VivianGomez/Tweenity">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>
-->
<h3 align="center">Tweenity plugin</h3>

  <p align="center">
    The Tweenity plugin streamlines the connection between Twine and Unity3D tools by reading and interpreting Twine graphs in Unity. Specifically, this plugin was designed to support a methodology aimed at enabling the rapid prototyping of simulators in Virtual Reality.
    <br />
    <br />
    ·
    <a href="https://github.com/VivianGomez/Tweenity/issues">Report Bug</a>
    ·
    <a href="https://github.com/VivianGomez/Tweenity/issues">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Tecnologies</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Features</a></li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#samples">VR projects builded with Tweenity</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

https://github.com/VivianGomez/Tweenity/assets/30846540/1b574f50-b155-4a57-97b3-69efba540f9e

The Tweenity plugin streamlines the connection between Twine and Unity3D tools by reading and interpreting Twine graphs in Unity. Specifically, this plugin was designed to support a methodology aimed at enabling the rapid prototyping of simulators in Virtual Reality.

To facilitate creation, this plugin allows the loading of a simulation using Twine graphs, where each node can have special behavior or functionality depending on the Node Type (selecting options from dialogues with different consequences, reproducing random events in the simulation, defining specific consequences based on user interactions, defining simulator responses when the user does not execute an expected action, or reminding them of the expected action in the simulation).

Additionally, Tweenity allows for some extra features in sample scenes, such as the execution of sample graphs, sample dialogues, simulator reactions (e.g., animations), movement of Mixamo characters along predefined paths, and the playback of audio instructions with a human guide (also from Mixamo).
<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Technologies

* [![Unity3D][Unity3D]][Unity-url] (Unity version 2021.1.13f)
* [![Twine][Twine]][Twine-url] (Twine version 2)
* [![Entwee][Entwee]][Entwee-url] (version 2021)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

You can use this plugin by importing it into your Unity project or by cloning this code repository.

### Prerequisites

Make sure you have Unity (and/or Unity Hub) installed, and you can open the project using Unity version 2021.1.13f1 or any higher version.

### Installation

#### Using unity package

1. Download Tweenity plugin as [unity package](https://github.com/VivianGomez/Tweenity/releases/download/latest/TweenityVR_v2.0.unitypackage)
2. Open your Unity project
4. Go to Assets > Import package > Custom package
   
![image](https://github.com/VivianGomez/Tweenity/assets/30846540/88f6ba52-6da2-4e2e-a116-cdcaa5ed7e2a)

6. Open the package previously downloaded.


#### Cloning repository

1. Open your terminal or command prompt.

2. Navigate to the directory where you want to clone the repository.
    ```bash
    cd path/to/your/directory
    ```
3. Run the following command in your terminal to clone the repository:
   ```bash
   git clone https://github.com/VivianGomez/Tweenity.git
   ```
4. Open the project in Unity Editor or Unity Hub (Open > Add project from disk)
5. Select the folder called Tweenity 

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- Features -->
## Features

#### Node types
To enhance the utilization of the graph in the creation of VR simulators, a series of node types have been defined, derived from the requirements identified in real multidisciplinary projects. Each node type offers specific functionality for the simulation, and it is possible to combine up to two node types in a single step.

![image](https://github.com/VivianGomez/Tweenity/assets/30846540/143f92e6-8fdf-4984-85bb-768d3b8c6f25)
![image](https://github.com/VivianGomez/Tweenity/assets/30846540/ebfdb39d-3d26-404a-a275-08b8843961cd)

- **START Node:** This type of node allows modeling the initial step of the simulation. Therefore, there should only be one per graph.

- **END Node:** This type of node allows modeling the final step of the simulation. Multiple END nodes can exist because several endings could be modeled in a simulation.

- **RANDOM Node:** It allows modeling the scenario in which we want to randomly select the next path. This makes simulations more dynamic, and the story is not the same all the time. Thus, after the player performs the current node action, the next path will be randomly selected.

- **MultipleChoice Node:** Allows modeling the scenario in which the user can take one of several actions, but each one has a different consequence.

- **REMINDER Node:** With this type of node, a reminder (blue arrow) will be placed above the specified interactive object in the current step.

- **TIMEOUT Node:** Allows modeling the case in which there is a time limit to perform a step. If the user’s action has not been performed after a certain amount of time, it automatically advances to another step of the simulation (specified in the body of this node).

- **DIALOGUE Node:** It models the scenario in which creators wish to include a dialogue allowing the player to choose from several response options, each with different consequences.

- **Typeless Node:** The Tweenity plugin also includes the possibility of having a node without a type in case a step doesn’t fit into the previous types.

 
#### Additional features
![image](https://github.com/VivianGomez/Tweenity/assets/30846540/daa06c7e-fd4e-4fde-96a5-8ce2a49fc079)
a. Dialogue boxes associated with graph reading and response options available at each node.

https://github.com/VivianGomez/Tweenity/assets/30846540/8cac8b41-071f-497c-960b-3ffbd1b988bd
![videoframe_5120](https://github.com/user-attachments/assets/29e00376-ed5b-47e4-af51-513d2a0ffe7b)


b. A Human guide, combining Mixamo rigging and Oculus LipSync, to facilitate audio instructions in the simulation.<br>
c. The movement of NPCs with Mixamo skeletons along predefined paths.<br>
d. The possibility of use a blue arrow to remind the user which object to interact with during training.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->
## Usage

To understand how to use Tweenity, the plugin provides a couple of example scenes—one for PC and one for VR. These scenes are located in [Assets > Scenes > Tweenity](https://github.com/VivianGomez/Tweenity/tree/main/Assets/Scenes/Tweenity)

The VR example scene loads a [sample graph](https://drive.google.com/file/d/1GoW0AMhCHp8nUwxPM9_aQFV-ILBpV8Vm/view) and allows to visualize all the different types of nodes, user event waiting, visual debugger, and simulator response actions.

![image](https://github.com/VivianGomez/Tweenity/assets/30846540/f182c8ee-fc3c-45b9-a743-544024c97856)


### Visualize sample graph on Twine

First, you need to import the Entwee format, which is the syntax used and interpreted within Unity by the Tweenity plugin. To do this, follow the steps:
1. Go to Twine editor and select the opction Twine > Story Formats
![image](https://github.com/user-attachments/assets/5a5753cf-79ae-4c8e-98a3-9b7800bcae70)

2. Then, press the 'Add' button to import the Entwee format.

![image](https://github.com/user-attachments/assets/7e000026-15df-4be6-8d64-c59d4706bfec)

3.That option will open a small input field where you need to paste the following link [https://mcdemarco.net/tools/entwee/format.js](https://mcdemarco.net/tools/entwee/format.js), and then click the 'Add' button.

![image](https://github.com/user-attachments/assets/96dbb95e-144e-464d-86fd-c75e5e84d679)

4. This action will add the Entwee format and yo will see it as your "Proofing format"
![image](https://github.com/user-attachments/assets/15d91fe9-ec0b-490c-808b-c3167a98c588)

To visualize the example graph in the Twine editor:

1. (recommended) Download and use [Twine](https://twinery.org/) locally as a desktop application.
   *Note: This option keeps the files locally, ensuring information privacy.*

2. Download the [sample graph](https://drive.google.com/file/d/1GoW0AMhCHp8nUwxPM9_aQFV-ILBpV8Vm/view).

3. Open the Twine application.

4. Go to the `Library > Import` option.<br>
   ![image](https://github.com/VivianGomez/Tweenity/assets/30846540/f08e1536-da85-4d7b-84a0-829d7610aadb)

5. Select the downloaded file and make sure to check the box with the graph's name. Then, Click on `Import Selected Files`.
   ![image](https://github.com/VivianGomez/Tweenity/assets/30846540/09836e1b-6aec-4957-a951-2e5acf3847e2)

6. This wiil open the complete graph
   ![image](https://github.com/VivianGomez/Tweenity/assets/30846540/816e900f-78ae-47a6-a893-d1a8a76b13fa)

*Note: Twine may encounter issues with imports. If you receive any error messages in step 6, simply restart the application.*

### Create your own graph on Twine
When building your own simulations or games, you will need to create your own graphs. Follow these steps to do so:  
1.   Open Twimne editor, and click on "New" option (this create a new graph)
![image](https://github.com/user-attachments/assets/695f8f19-c436-4f27-8d58-1cf4a59de64a)

2.   That will open a small input field, write there the name of your graph, then click on "Create"
![image](https://github.com/user-attachments/assets/85d8a836-83a5-4be7-94c3-4a5c79e45ec4)


3.   Then this will open the graph editor
![image](https://github.com/user-attachments/assets/d3c3094c-45df-46f4-b8da-b4d541587111)


4.   There you can create your story/simulation script using format described on paper of [Tweenity and ProcoColVR methodology](https://ieeexplore.ieee.org/document/10458365) or using same format of [sample graph](url)


### Export Twine graph and import into Unity

Once you've finished your graph, you can export it for use in Unity. Follow these steps to do so:
1. First, ensure that you import the Entwee format as a Proofing format (as described in (as is described on [Step 4 of this section](https://github.com/VivianGomez/Tweenity/tree/main?tab=readme-ov-file#visualize-sample-graph-on-twine))
**Note**: If this format is not tagged as 'Use for proofing,' you can mark it by clicking on Entwee and selecting 'Use to Proof Stories.'
![image](https://github.com/user-attachments/assets/eef15244-9fde-4954-a06b-e55b50564918)


2. Then, go to ypur graph and select Build > Proof

![image](https://github.com/user-attachments/assets/63a337dd-64d1-4cff-aeec-a0014ef3f40d)

3. This action will download the text file. Then, go to Unity and navigate to the folder Assets/Resources/Tweenity/TwineTXTs, and create a new empty .txt file with the name of your story or simulation graph.

4. Then, copy the text from the downloaded file in Step 3 into the new .txt file.

**Note: This step is very important because the proofing format can manage different character encodings that may not be understood by the Tweenity plugin**


<p align="right">(<a href="#readme-top">back to top</a>)</p>


## VR projects builded with Tweenity

Tweenity originated as a result of developing two virtual reality training simulators. These simulators aimed to replicate a procedure involving a series of steps with varying sequences, hence the proposal to use graphs for defining simulations. Below, we showcase the projects created with Tweenity.

### Videos Showcase
[<img src="https://github.com/VivianGomez/Tweenity/assets/30846540/abe41807-2f04-423c-8f30-eb2259ab309d" width="550" height="350">](https://youtu.be/FQgroO2jkbw)

**P1 - Navy Simulator**
A simulator of the ship’s control room in our Navy. The purpose of this simulator is to facilitate training in both normal and emergency situations that
an Engineering Officer could encounter. In this project, we had a team composed of six Navy officials, two designers, and two developers. ([Video](https://youtu.be/ScdDqaJFiPg))


[<img src="https://github.com/VivianGomez/Tweenity/assets/30846540/42542a58-06de-497d-9cad-53dd51814b93" width="550" height="350">](https://youtu.be/ScdDqaJFiPg)

**P - Birth simulator**
A simulator to provide a training environment for physicians in which they can make decisions about the first minute of a baby’s life after delivery. In this project, we had a team composed of two physicians, two developers, and one designer. ([Video](https://youtu.be/FQgroO2jkbw))

### Some begginers projects builded in a Jam called JamVR:
Here are the three standout projects from our JamVR event. These simulators were built within a one-month timeframe, with the collaboration of multidisciplinary teams that included designers, developers, and experts in the specific domain of each simulator. All samples can be reviewed on the Padlet of Created Simulators (including videos, descriptions ans source code): [Padlet Link](https://padlet.com/vn_gomez1/curso-jam-vr-los-simuladores-construidos-ajzvnzgrhx9dgy7x)

![image](https://github.com/VivianGomez/Tweenity/assets/30846540/1f4b255f-d9a9-47a4-bf47-8339fa79a686)

1. A human rights simulator prototype in a war context for personnel in the Colombian Navy. ([Video](https://www.youtube.com/watch?v=Q7iF5_rI6X8&ab_channel=MOLEINTERACTIVE), [Final submission](https://drive.google.com/drive/folders/1z4MfRGilbKBVyE1raca5VEbydpC8hlEz?usp=drive_link), [Graph](https://docs.google.com/document/d/1uKPAiTecDcWt8_ckSldLW-m14QkbJ_gn/edit?usp=drive_link&ouid=111987359593681185749&rtpof=true&sd=true))
2. A risk simulator prototype for workers in the cement sector. ([Video](https://padlet.com/redirect?url=https%3A%2F%2Fyoutu.be%2FBFuFu-pA1lE), [Final Submission](https://drive.google.com/file/d/1-kP5B8_RErcdU7b1gRIkAD7cqp7o0SN8/view), [Graph](https://drive.google.com/file/d/1-kP5B8_RErcdU7b1gRIkAD7cqp7o0SN8/view?usp=sharing))
3. A simulator prototype for diagnosing heart failure in a virtual patient. ([Video](https://youtu.be/YvazfQ4JNuc), [Final Submission](https://github.com/baperezg/SimFallaCardiacaVRjam))

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## References and Sources

This software is based on the **Entwee** text format, a structure used to represent interactive stories in Twine. For more details about Entwee, you can check the following link:

[Entwee Format - Entweedle](https://www.maximumverbosity.net/twine/Entweedle/format.js)

Additionally, the analysis and processing of this format in Unity were inspired by **Matthew Ventures' tutorial**, which explains how to convert a Twine story to Unity. You can access the tutorial here:

[Converting a Twine Story to Unity - Matthew Ventures](https://www.mrventures.net/all-tutorials/converting-a-twine-story-to-unity)

This project references both resources to facilitate the integration of interactive stories into Unity, but we add several nodes and a particular format, created specially for VR Simulators for Procedural Training, as is explained in the Master Thesis Document [here](https://repositorio.uniandes.edu.co/entities/publication/85b48b5f-7d37-456e-8cfc-67c39b6e301a) and the Journal Article of this work [here](https://ieeexplore.ieee.org/document/10458365).

<!-- LICENSE -->
## License

Distributed under the GPLv3 License. See [`LICENSE`](https://github.com/VivianGomez/Tweenity/blob/main/LICENSE) for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

* Vivian Gómez  - vn.gomez@uniandes.edu.co
* Pablo Figueroa - pfiguero@uniandes.edu.co


<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/VivianGomez/Tweenity.svg?style=for-the-badge
[contributors-url]: https://github.com/VivianGomez/Tweenity/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/VivianGomez/Tweenity.svg?style=for-the-badge
[forks-url]: https://github.com/VivianGomez/Tweenity/network/members
[stars-shield]: https://img.shields.io/github/stars/VivianGomez/Tweenity.svg?style=for-the-badge
[stars-url]: https://github.com/VivianGomez/Tweenity/stargazers
[issues-shield]: https://img.shields.io/github/issues/VivianGomez/Tweenity.svg?style=for-the-badge
[issues-url]: https://github.com/VivianGomez/Tweenity/issues
[license-shield]: https://img.shields.io/github/license/VivianGomez/Tweenity.svg?style=for-the-badge
[license-url]: https://github.com/VivianGomez/Tweenity/blob/main/LICENSE
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/vivian-g%C3%B3mez-cubillos-79499b18a/
[product-screenshot]: images/screenshot.png
[Twine]: https://img.shields.io/badge/Twine-000000?style=for-the-badge&logo=&logoColor=129A76
[Twine-url]: https://twinery.org/
[Unity3D]: https://img.shields.io/badge/Unity3D-20232A?style=for-the-badge&logo=&logoColor=61DAFB
[Unity-url]: https://reactjs.org/
[Entwee]: https://img.shields.io/badge/Entwee-20232A?style=for-the-badge&logo=&logoColor=1370D8
[Entwee-url]: https://www.maximumverbosity.net/twine/Entweedle/
