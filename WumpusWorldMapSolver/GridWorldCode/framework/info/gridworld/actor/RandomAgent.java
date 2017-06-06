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
 *
 * @last modified by: Adam Koewler
 *
 *
 * @notes: I tested this on several WumpusWorld(world/WumpusWorld.java) boards.
 * Below are the 3 I used
 *
 * Board 1 (Speed: Average) Score: 962
         add(new Location(9,2), new Wumpus());
         add(new Location(7,4), new Pit());
         add(new Location(0,0), new Pit());
         add(new Location(1,4), new Pit());
         add(new Location(2,5), new Pit());
         add(new Location(3,7), new Pit());
         add(new Location(7,0), new Pit());
         add(new Location(6,3), new Pit());
         add(new Location(6,6), new Pit());
         add(new Location(7,8), new Pit());
         add(new Location(7,7), new Gold());
         Grid<Actor> gr = getGrid();
         gr.setGoldLocation(new Location(7,7));		
 * Board 2 (Speed: Fast) Score: 992
         add(new Location(9,2), new Wumpus());
         add(new Location(7,2), new Pit());
         add(new Location(7,0), new Pit());
         add(new Location(6,3), new Pit());
         add(new Location(6,6), new Pit());
         add(new Location(7,8), new Pit());
         add(new Location(7,1), new Gold());
         Grid<Actor> gr = getGrid();
         gr.setGoldLocation(new Location(7,1));	
 * Board 3 (Speed: Long) Score: 962
         add(new Location(3,1), new Wumpus());
         add(new Location(1,1), new Pit());
         add(new Location(1,4), new Pit());
         add(new Location(2,5), new Pit());
         add(new Location(3,7), new Pit());
         add(new Location(5,4), new Pit());
         add(new Location(6,3), new Pit());
         add(new Location(2,1), new Gold());
         Grid<Actor> gr = getGrid();
         gr.setGoldLocation(new Location(2,1));	
 */
	
   package info.gridworld.actor;

   import info.gridworld.grid.Grid;
   import info.gridworld.grid.Location;
   import info.gridworld.actor.*;

   import java.awt.Color;
   import java.util.ArrayList;
   import java.util.Stack;
   import java.util.Set;
   import java.util.HashSet;

   public class RandomAgent extends Actor{
   
      boolean VisitedMap[][];
      ArrayList<Location> possDangerPit = new ArrayList<Location>();
      ArrayList<Location> possDangerWum = new ArrayList<Location>();
      ArrayList<Location> possAdjWumpus = new ArrayList<Location>();
      Stack<Location> possHistory = new Stack<Location>();
      boolean firstAct = true;
      boolean probSolved = false;
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
         if(firstAct){
            possHistory.push(getLocation());
            firstAct = false;
         }
         // Goal
         if (gr.getGlitter(getLocation()) || probSolved){
            // Puzzle solved - recurse backwards
            if (!probSolved){
               gr.grabGold(getLocation());
               probSolved = true;
            }
            // Recurse backwards
            if(probSolved){
                  // Backtrack
                  System.out.println("Gold Aquired - Walking back home!");
                  // Check for position history
                  if(possHistory.empty()){
                     // Isn't Possible
                  }else{
                     // Move to previous
                     System.out.println("Homebound: Stack before backtrack: "+possHistory);
                     possHistory.pop();
                     if(possHistory.empty()){
                        // OPERATION: KILL WUMPUS
                        // Check Wumpus Array(possDangerWum)
                        System.out.println("Hunting Wumpus...");
                        boolean wumpFound = false;
                        if(possAdjWumpus.size() > 0){
                           if(possAdjWumpus.size() > 2){
                              // Loop Adj Cords
                              HashSet hs = new HashSet();
                              hs.addAll(possAdjWumpus);
                              possAdjWumpus.clear();
                              possAdjWumpus.addAll(hs);
                              System.out.println("Wumpus Adj Cords: "+possAdjWumpus);
                              for (Location possWums1 : gr.getValidTrueAdjacentLocations(possAdjWumpus.get(0))){
                                 for (Location possWums2 : gr.getValidTrueAdjacentLocations(possAdjWumpus.get(1))){
                                    for (Location possWums3 : gr.getValidTrueAdjacentLocations(possAdjWumpus.get(2))){
                                       // Used to find wumpus location
                                       //System.out.println(possWums1 + "-" + possWums2 + "-" + possWums3);
                                       // If adj locations all match
                                       if(possWums1.equals(possWums2) && possWums3.equals(possWums2)){
                                          System.out.println("WUMPUS HAS BEEN FOUND!");
                                          gr.shoot(possWums1);
                                          wumpFound = true;
                                       }
                                       if(wumpFound){break;}
                                    }
                                    if(wumpFound){break;}
                                 }
                                 if(wumpFound){break;}
                              }
                           }
                        }
                        System.out.println("Heard Scream: "+wumpFound);
                        // Go Home
                        System.out.println("Made it home :)");
                        gr.climb(getLocation());
                        removeSelfFromGrid();
                     } else{
                        Location tempLoc = possHistory.peek();
                        moveTo(tempLoc);
                        // Remove most recent
                        System.out.println("Homebound: Stack after backtrack: "+possHistory);
                     }
                  }
               }
            // Done
         }
         // Danger
         else if(gr.getStench(getLocation()) || gr.getBreeze(getLocation())){
            // GOAL: Mark possible dangers
            ArrayList<Location> adjLocs = gr.getValidTrueAdjacentLocations(getLocation());
            boolean doBacktrack = false;
            // Track Wumpus Location - possAdjWumpus
            if(gr.getStench(getLocation())){
               possAdjWumpus.add(getLocation());
            }
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
                  System.out.println(possDangerWum);
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
               if(gr.getStench(getLocation()) || gr.getBreeze(getLocation())){
                  for (Location possPit : possDangerPit){
                  if(doBacktrack){
                     break;
                  }
                     // Loop Possible Wumps
                     for (Location possWum : possDangerWum){
                     
                     // Line below is used for debugging adv maps
                     //System.out.println(possPit + "-" + possWum + "-" + choice);
                     
                        // Compare with current choice
                        if(possPit.equals(choice)){
                           if(possWum.equals(choice)){
                              System.out.println("Safe location found.");
                              System.out.println("Destination: "+ choice);
                              System.out.println("Current: "+ getLocation());
                              // Unvisited
                              if (VisitedMap[choice.getRow()][choice.getCol()] == false){
                                 System.out.println("Moving(Risk): Stack before backtrack: "+possHistory);
                                 possHistory.push(choice);
                                 moveTo(choice);
                                 System.out.println("Moving(Risk): Stack after backtrack: "+possHistory);
                                 // Need seperate var for backtrack and for skipping this move
                                 doBacktrack = true;
                              }                           
                           }
                        }
                     }
                  }
               }
               // NOTE: I think this can be removed?
               if(doBacktrack && false == true){
                  // Backtrack
                  System.out.println("DANGER! Preforming backtrack.");
                  // Check for position history
                  if(possHistory.empty()){
                     // then empty??
                     System.out.println("Shit, stack is empty!");
                  }else{
                     // Move to previous
                     System.out.println("Looking(Danger): Stack before backtrack: "+possHistory);
                     possHistory.pop();
                     Location tempLoc = possHistory.peek();
                     moveTo(tempLoc);
                     // Remove most recent
                     System.out.println("Looking(Danger): Stack after backtrack: "+possHistory);
                     
                  }
                  break;
               }
            }
            // GOAL: Backtrack
            if(!doBacktrack){
                  // Backtrack
                  System.out.println("DANGER! Preforming backtrack.");
                  // Check for position history
                  if(possHistory.empty()){
                     // then empty??
                     System.out.println("Shit, stack is empty!");
                  }else{
                     // Move to previous
                     System.out.println("Looking(Danger): Stack before backtrack: "+possHistory);
                     possHistory.pop();
                     Location tempLoc = possHistory.peek();
                     moveTo(tempLoc);
                     // Remove most recent
                     System.out.println("Looking(Danger): Stack after backtrack: "+possHistory);
                     
                  }
               }
         }
         // Safe
         else{
            // find unvisited location
            //get adjacent squares
            ArrayList<Location> adjLocs = gr.getValidTrueAdjacentLocations(getLocation());
            ArrayList<Boolean> allVisitedList = new ArrayList<Boolean>();
            Boolean allVisitedBool = true;
            // Loop Safe Choices
            for (Location choice : adjLocs){
               // Check if all visited
               System.out.println("Visted: "+choice+":"+VisitedMap[choice.getRow()][choice.getCol()]);
               if (VisitedMap[choice.getRow()][choice.getCol()]){
                  allVisitedList.add(true);
               } else{
                  allVisitedList.add(false);
                  System.out.println("Looking(Safe): Stack before backtrack: "+possHistory);
                  possHistory.push(choice);
                  System.out.println("Looking(Safe): Stack after backtrack: "+possHistory);
                  moveTo(choice);
                  break;
               }
            }
            for (Boolean eachAdj : allVisitedList){
               if(eachAdj == false){
                  allVisitedBool = false;
               }
            }
            if(allVisitedBool){
               // Backtrack
               System.out.println("Preform All Visited Backtrack.");
               // Check for position history
               if(possHistory.empty()){
                  // then empty??
                  System.out.println("Shit, stack is empty!");
               }else{
                  // Move to previous
                  System.out.println("Stack All Visited before backtrack: "+possHistory);
                  possHistory.pop();
                  Location tempLoc = possHistory.peek();
                  moveTo(tempLoc);
                  // Remove most recent
                  System.out.println("Stack All Visited after backtrack: "+possHistory);
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
         if(getLocation()!= null){
            System.out.println("AT ("+getLocation().getRow()+","+getLocation().getCol()+")");
            System.out.println("Stench: "+gr.getStench(getLocation()));
            System.out.println("Breeze: "+gr.getBreeze(getLocation()));
            System.out.println("Glitter: "+gr.getGlitter(getLocation()));
         } else{
            System.out.println("Puzzle Done!");
         }
      }
      
   }
