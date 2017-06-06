/* 
 * AP(r) Computer Science GridWorld Case Study:
 * Copyright(c) 2005-2006 Cay S. Horstmann (http://horstmann.com)
 *
 * This code is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation.
 *
 * This code is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * @author Cay Horstmann
 */

import info.gridworld.actor.*;
import info.gridworld.grid.Location;
import info.gridworld.world.*;
import info.gridworld.grid.*;


/**
 * This class runs a world that contains a bug and a rock, added at random
 * locations. Click on empty locations to add additional actors. Click on
 * populated locations to invoke methods on their occupants. <br />
 * To build your own worlds, define your own actors and a runner class. See the
 * BoxBugRunner (in the boxBug folder) for an example. <br />
 * This class is not tested on the AP CS A and AB exams.
 */
public class WumpusRunner
{
    public static void main(String[] args)
    {
        WumpusWorld world = new WumpusWorld();
        Grid<Actor> gr = world.getGrid();
        int rows = gr.getNumRows();
        int cols = gr.getNumCols();
		  RandomAgent fred = new RandomAgent(rows, cols);
		  world.add(new Location(rows-1, 0), fred);
		  fred.report();	
        world.show();
    }
}
