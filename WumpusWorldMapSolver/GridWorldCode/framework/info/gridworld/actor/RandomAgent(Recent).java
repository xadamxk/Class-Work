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
   import java.util.Stack;

/**
 * A <code>Bug</code> is an actor that can move and turn. It drops flowers as
 * it moves. <br />
 * The implementation of this class is testable on the AP CS A and AB exams.
 */
   public class RandomAgent extends Actor
   {
   
      boolean VisitedMap[][];
      ArrayList<Location> possDangerPit = new ArrayList<Location>();
      ArrayList<Location> possDangerWum = new ArrayList<Location>();
      Stack<Location> possHistory = new Stack<Location>();
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
            // GOAL: Mark possible dangers
            ArrayList<Location> adjLocs = gr.getValidTrueAdjacentLocations(getLocation());
            // Loop through Abj options
            for (Location choice : adjLocs){
               boolean isBreeze = false;
               boolean isStench = false;
               // Pit
               if(gr.getBreeze(getLocation())){
                  isBreeze = true;
               }
               // Wump
               if(gr.getStench(getLocation())){
                  isStench = true;
               }
               // ------------------------------ (Add Possible Pits) ------------------------------
               // Loop Possible Pits
               if(isBreeze && possDangerPit.size() == 0){
                  // Add value to possible pits
                  possDangerPit.add(choice);
               } else if(isBreeze){
                  // Loop Pits - to add new pits
                  boolean addPit = true;
                  for (Location possPits : possDangerPit){
                  
                  // Compare new vals(choice) to possible vals(possPits) - add if no match
                     if(choice != possPits){
                        addPit = true;
                     }
                  }
                  // Add new pit
                  if(addPit){
                     possDangerPit.add(choice);
                  }
               }
               // ------------------------------ (Add Possible Wumps) ------------------------------
               // Loop Possible Wumps
               if(isStench && possDangerWum.size() == 0){
                  // Add value to possible wumpus
                  possDangerWum.add(choice);
               } else if(isStench){
                  // Loop Wumpus - to add new wumpus
                  boolean addWump = true;
                  for (Location possWums : possDangerWum){
                  // Compare new vals(choice) to possible vals(possWums) - add if no match
                     if(choice != possWums){
                        addWump = true;
                     }
                  }
                  // Add new pit
                  if(addWump){
                     possDangerWum.add(choice);
                  }

               }
               // ------------------------------ (Compare possible pits + possible wumps + current choice) ------------------------------
               // Loop Possible Pits
               for (Location possPit : possDangerPit){
                  // Loop Possible Wumps
                  for (Location possWum : possDangerWum){
                  System.out.println(possPit + "-" + possWum + "-" + choice);
                     // Compare with current choice
                     if(possPit.equals(choice)){
                        if(possWum.equals(choice)){
                        System.out.println("Goal: "+ choice);
                        System.out.println("Current: "+ getLocation());
                           // Unvisited
                           if (VisitedMap[choice.getRow()][choice.getCol()] == false){
                              System.out.println("???");
                              possHistory.push(choice);
                              moveTo(choice);
                              break;
                           }
                        }
                     }
                  }
               }
               // Backtrack
               backtrack();
               break;
            }
            // GOAL: Loop current adj with possible dangers, if they aren't dangerous and don't match, move to it
         }
         // Safe
         else{
            // find unvisited location
            //get adjacent squares
            ArrayList<Location> adjLocs2 = gr.getValidTrueAdjacentLocations(getLocation());
            // Loop Safe Choices
            for (Location choice2 : adjLocs){
            System.out.println(choice2);
               // Check if all visited
               if (VisitedMap[choice2.getRow()][choice2.getCol()]){
                  possHistory.push(choice2);
                  System.out.println(getLocation() + "-" +choice2);
                  moveTo(choice2);
                  break;
               } else{
               }
            }
            System.out.println("SAFE BACKTRACK!");
            backtrack();
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
      
      public void backtrack(){
         // Check for position history
         if(possHistory.empty()){
            // then empty??
         }else{
            // Remove most recent
            possHistory.pop();
            // Move to previous
            moveTo(possHistory.peek());
         }
      }
     
   }
