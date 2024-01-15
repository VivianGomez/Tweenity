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
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
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
    <a href="https://github.com/VivianGomez/Tweenity/wiki"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/VivianGomez/Tweenity">View Demo</a>
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



### Built With

* [![Unity3D][Unity3D]][Unity-url]
* [![Twine][Twine]][Twine-url]
* [![Entwee][Entwee]][Entwee-url]

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


<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->
## Usage

Use this space to show useful examples of how a project can be used. Additional screenshots, code examples and demos work well in this space. You may also link to more resources.

_For more examples, please refer to the [Documentation](https://example.com)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>


## VR projects builded with Tweenity

Tweenity originated as a result of developing two virtual reality training simulators. These simulators aimed to replicate a procedure involving a series of steps with varying sequences, hence the proposal to use graphs for defining simulations. Below, we showcase the projects created with Tweenity.

### Videos Showcase

[![Video 1](https://img.youtube.com/vi/FQgroO2jkbw/maxresdefault.jpg)](https://youtu.be/FQgroO2jkbw)

**Video 1 Title**
A generic description for Video 1.

---

[![Video 2](https://img.youtube.com/vi/ScdDqaJFiPg/maxresdefault.jpg)](https://youtu.be/ScdDqaJFiPg)

**Video 2 Title**
A generic description for Video 2.

### Some begginers projects builded in a Jam called JamVR:



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



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Your Name - [@twitter_handle](https://twitter.com/twitter_handle) - email@email_client.com

Project Link: [https://github.com/VivianGomez/Tweenity](https://github.com/VivianGomez/Tweenity)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* []()
* []()
* []()

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
[license-url]: https://github.com/VivianGomez/Tweenity/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/linkedin_username
[product-screenshot]: images/screenshot.png
[Twine]: https://img.shields.io/badge/Twine-000000?style=for-the-badge&logo=&logoColor=129A76
[Twine-url]: https://twinery.org/
[Unity3D]: https://img.shields.io/badge/Unity3D-20232A?style=for-the-badge&logo=&logoColor=61DAFB
[Unity-url]: https://reactjs.org/
[Entwee]: https://img.shields.io/badge/Entwee-20232A?style=for-the-badge&logo=&logoColor=1370D8
[Entwee-url]: https://www.maximumverbosity.net/twine/Entweedle/
