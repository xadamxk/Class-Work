# Wumpus World Solver
<p align="center">
GUI Application to solve Wumpus World Maps.
</p>

<h3><b><big>Description</big></b></h3>
Created in CS 461 (Artificial Intelligence), this application solves Wumpus World Maps. The goal of Wumpus World is to get the gold, kill the Wumpus, and return to the entry point (<a href="https://cis.temple.edu/~giorgio/cis587/readings/wumpus.shtml">More Info</a>). Cells adjacent to pits give a breeze, the Wumpus gives a stench, and the gold gives glitter. This application compares and maps these states throughout the game to determine the next move. Below are screenshots of various map difficulties: 

<details> 
  <summary>Hard</summary>
  <p align="center">
    <img src="https://raw.githubusercontent.com/xadamxk/Class-Work/master/WumpusWorldMapSolver/Example1.gif" title="WWS Screenshot1" />
  </p>
</details>

<details> 
  <summary>Intermediate</summary>
  <p align="center">
    <img src="https://raw.githubusercontent.com/xadamxk/Class-Work/master/WumpusWorldMapSolver/Example2.gif" title="WWS Screenshot2" />
  </p>
</details>

<details> 
  <summary>Easy</summary>
  <p align="center">
    <img src="https://raw.githubusercontent.com/xadamxk/Class-Work/master/WumpusWorldMapSolver/Example3.gif" title="WWS Screenshot3" />
  </p>
</details>
</p>


Code for runner agent: <a href="https://github.com/xadamxk/Class-Work/blob/master/WumpusWorldMapSolver/GridWorldCode/framework/info/gridworld/actor/RandomAgent.java">Here</a>
Code for Wumpus map: <a href="https://github.com/xadamxk/Class-Work/blob/master/WumpusWorldMapSolver/GridWorldCode/framework/info/gridworld/world/WumpusWorld.java">Here</a>

<ul><li><h3><b><big>Key Concepts</big></b></h3>
<ul><li><b>Recursion:</b> Used to navigate throughout the Wumpus World Map.</li></ul>
<ul><li><b>Backtracking:</b> Stack used to track movements incase of dangers on Wumpus Map.</li></ul>
<ul><li><b>Other Basic AI Concepts:</b> Several other methods are used throughout this project, feel free to look at the code.</li></ul>


</li></ul>

