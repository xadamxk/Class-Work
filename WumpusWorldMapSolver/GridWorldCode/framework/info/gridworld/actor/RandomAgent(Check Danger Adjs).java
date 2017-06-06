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
   public class RandomAgent extends Actor
   {
   
      boolean VisitedMap[][];
      int numRows = 0;
      int numCols = 0;
   	
    /**
     * Constructs a red Agent.
     */
      public RandomAgent(int NumRows, int NumCols)
      {
         setColor(Color.RED);
      
         numRows = NumRows;
         numCols = NumCols;   
         Grid<Actor> gr = getGrid();
         VisitedMap = new boolean[NumRows][NumCols];
         for (int r=0; r<NumRows; r++)
         {
            for (int c=0; c<NumCols; c++)
            {
               VisitedMap[r][c] = false;
            }
         }
      }
   
    /**
     * Moves if it can move.
     */
      public void act()
      {
         Grid<Actor> gr = getGrid();	
         VisitedMap[getLocation().getRow()][getLocation().getCol()] = true;
         
         // Goal
         System.out.println("Glitter: "+gr.getGlitter(getLocation()));
         if (gr.getGlitter(getLocation())){
            gr.grabGold(getLocation());
            removeSelfFromGrid();
         }
         // Danger
         else if(gr.getStench(getLocation()) || gr.getBreeze(getLocation())){
            boolean moved = false;
            // Array of Current's Adj
            ArrayList<Location> adjLocs = gr.getValidTrueAdjacentLocations(getLocation());
            // Adjacent Squares of danger
            for (Location choice : adjLocs){
               // Only check unvisited
               if(!VisitedMap[choice.getRow()][choice.getCol()]){
                  System.out.println("Possible Loc: "+choice);
                  // If Possible is safe, move
                  if(gr.getStench(choice) || gr.getBreeze(choice)){
                     moveTo(choice);
                     moved = true;
                     break;
                  }
                  // Array of Danger's Adjs
                  ArrayList<Location> adjLocsDanger = gr.getValidTrueAdjacentLocations(choice);
                  // List of Danger states
                  int dangerCount = 0;
                  Location choiceDangerFinal = null;
                  // Adjacent Squares of Danger's Adj(s)
                  for (Location choiceDanger : adjLocsDanger){
                  System.out.println("Choice Danger: "+choiceDanger);
                     //moveTo(choiceDanger);
                     System.out.println("Choice Danger Stench: "+gr.getStench(choiceDanger));
                     System.out.println("Choice Danger Breeze: "+gr.getBreeze(choiceDanger));
                     if(gr.getStench(choiceDanger) || gr.getBreeze(choiceDanger)){
                        System.out.println("DANGA!"+choiceDanger);
                        dangerCount++;
                     }
                     choiceDangerFinal = choiceDanger;
                  }
                  // If less than 3 true, move
                  if(dangerCount == 0){
                     System.out.println("Danger Count: "+dangerCount);
                     System.out.println("Next Safe: "+choiceDangerFinal);
                     moveTo(choice);
                     moved = true;
                     break;
                  }
               }
            }
         }
         // Safe
         else{
            
            boolean moved = false;
            
            // find unvisited location
            //get adjacent squares
            ArrayList<Location> adjLocs = gr.getValidTrueAdjacentLocations(getLocation());
            //if there is an OK, move there
            for (Location choice : adjLocs){
               // Unvisited Safe Spots
               if (!VisitedMap[choice.getRow()][choice.getCol()]){
                  moveTo(choice);
                  moved = true;
                  break;

               }
            }
         }
         report();
      }

      public void report()
      {   
         Grid<Actor> gr = getGrid();	
         System.out.println();
			System.out.println();   
         System.out.println("AT ("+getLocation().getRow()+","+getLocation().getCol()+")");
         System.out.println("Stench: "+gr.getStench(getLocation()));
         System.out.println("Breeze: "+gr.getBreeze(getLocation())); 	
      }
     
   }
