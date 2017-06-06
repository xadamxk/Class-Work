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
 * @modified 2011 Scott Anderson

 */
	
   package info.gridworld.actor;

   import info.gridworld.grid.Grid;
   import info.gridworld.grid.Location;
   import info.gridworld.actor.*;

   import java.awt.Color;
   import java.util.ArrayList;

/**
 * A <code>Bug</code> is an actor that can move and turn. It drops flowers as
 * it moves. <br />
 * The implementation of this class is testable on the AP CS A and AB exams.
 */
   public class Agent extends Actor
   {
   
      boolean StenchMap[][];
      boolean BreezeMap[][];
      boolean VisitedMap[][];
      int numRows = 0;
      int numCols = 0;
      int numMoves = 0;
   	
    /**
     * Constructs a red Agent.
     */
      public Agent(int NumRows, int NumCols)
      {
         setColor(Color.RED);
      
         numRows = NumRows;
         numCols = NumCols;   
         Grid<Actor> gr = getGrid();
         StenchMap = new boolean[NumRows][NumCols];
         BreezeMap = new boolean[NumRows][NumCols];
         VisitedMap = new boolean[NumRows][NumCols];
         for (int r=0; r<NumRows; r++)
         {
            for (int c=0; c<NumCols; c++)
            {
               StenchMap[r][c] = false;
               BreezeMap[r][c] = false;
               VisitedMap[r][c] = false;
            }
         }
      }
   
    /**
     * Moves if it can move, turns otherwise.
     */
      public void act()
      {
         Grid<Actor> gr = getGrid();	
         StenchMap[getLocation().getRow()][getLocation().getCol()] = gr.getStench(getLocation());
         BreezeMap[getLocation().getRow()][getLocation().getCol()] = gr.getBreeze(getLocation());
         VisitedMap[getLocation().getRow()][getLocation().getCol()] = true;
                	
         boolean moved = false;
      	
      	// kill Wumpus?
         if (moved)
         {
            report();
         }
      }
   
      public void report()
      {   
         Grid<Actor> gr = getGrid();	
         System.out.println();
         System.out.println();   
         System.out.println("ATd ("+getLocation().getRow()+","+getLocation().getCol()+")");
         System.out.println("Stench="+gr.getStench(getLocation()));
         System.out.println("Breeze="+gr.getBreeze(getLocation())); 	
      }
   
   }
