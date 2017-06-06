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

   package info.gridworld.grid;

   import java.util.ArrayList;
   import info.gridworld.actor.*;

/**
 * <code>AbstractGrid</code> contains the methods that are common to grid
 * implementations. <br />
 * The implementation of this class is testable on the AP CS AB exam.
 */
   public abstract class AbstractGrid<E> implements Grid<E>
   {
   
      private Location goldLocation;
      private boolean hasGold = false;
      private boolean heardScream = false;
   
      public boolean heardScream()
      {
         return heardScream;
      }
   	
      public boolean getStench(Location loc)
      {
      	
      //test to make sure me is really at loc
         E me = get(loc);
         if (!(me instanceof Agent || me instanceof RandomAgent)) 
            return false;
      	
         for (Location neighbor : getValidAdjacentLocations(loc))
         {
            if ((get(neighbor) instanceof Wumpus) && ((loc.getRow()==neighbor.getRow()) || (loc.getCol()==neighbor.getCol())) ) 
               return true;
         }
         return false;
      }
   
      public boolean getBreeze(Location loc)
      {
      	
      //test to make sure me is really at loc
         E me = get(loc);
         if (!(me instanceof Agent || me instanceof RandomAgent)) 
            return false;
      	
         for (Location neighbor : getValidAdjacentLocations(loc))
         {
            if ((get(neighbor) instanceof Pit) && ((loc.getRow()==neighbor.getRow()) || (loc.getCol()==neighbor.getCol())) ) 
               return true;
         }
         return false;
      }
   
      public boolean getGlitter(Location loc)
      {
      	
      //test to make sure me is really at loc
         E me = get(loc);
         if (!(me instanceof Agent || me instanceof RandomAgent)) 
            return false;
      	
         if (loc.equals(goldLocation)) 
            return true;
         else
            return false;
      }
   	
      public boolean grabGold(Location loc)
      {
         E me = get(loc);
         if (!(getGlitter(loc))) 
            return false;
      	
         hasGold = true;
         return true;
      }
   
      public boolean hasGold()
      {
         return hasGold;
      }
   
      public void setGoldLocation(Location loc)
      {
         goldLocation = loc;
      }
   	
      public void climb(Location loc)
      {
         if (hasGold)
         {
            E me = get(loc);
            ((Actor)me).putAgentCount(1000);
         }
      }
   	
      public void shoot(Location loc)
      {

         if (isValid(loc))
         {
            if (get(loc) instanceof Wumpus)
            {
            
            	//kill it
               ((Actor)get(loc)).removeSelfFromGrid();
            }
         }
      }
 
      public ArrayList<E> getNeighbors(Location loc)
      {
         ArrayList<E> neighbors = new ArrayList<E>();
         for (Location neighborLoc : getOccupiedAdjacentLocations(loc))
            neighbors.add(get(neighborLoc));
         return neighbors;
      }
   
      public ArrayList<Location> getValidAdjacentLocations(Location loc)
      {
         ArrayList<Location> locs = new ArrayList<Location>();
      
         int d = Location.NORTH;
         for (int i = 0; i < Location.FULL_CIRCLE / Location.HALF_RIGHT; i++)
         {
            Location neighborLoc = loc.getAdjacentLocation(d);
            if (isValid(neighborLoc))
               locs.add(neighborLoc);
            d = d + Location.HALF_RIGHT;
         }
         return locs;
      }
   	
      public ArrayList<Location> getValidTrueAdjacentLocations(Location loc)
      {
         ArrayList<Location> locs = new ArrayList<Location>();
      
         int d = Location.NORTH;
         for (int i = 0; i < Location.FULL_CIRCLE / Location.RIGHT; i++)
         {
            Location neighborLoc = loc.getAdjacentLocation(d);
            if (isValid(neighborLoc))
               locs.add(neighborLoc);
            d = d + Location.RIGHT;
         }
         return locs;
      }
   
      public ArrayList<Location> getEmptyAdjacentLocations(Location loc)
      {
         ArrayList<Location> locs = new ArrayList<Location>();
         for (Location neighborLoc : getValidAdjacentLocations(loc))
         {
            if (get(neighborLoc) == null)
               locs.add(neighborLoc);
         }
         return locs;
      }
   
      public ArrayList<Location> getOccupiedAdjacentLocations(Location loc)
      {
         ArrayList<Location> locs = new ArrayList<Location>();
         for (Location neighborLoc : getValidAdjacentLocations(loc))
         {
            if (get(neighborLoc) != null)
               locs.add(neighborLoc);
         }
         return locs;
      }
    
      public ArrayList<Location> getOccupiedLocations()
      {
         ArrayList<Location> theLocations = new ArrayList<Location>();
      
        // Look at all grid locations.
         for (int r = 0; r < getNumRows(); r++)
         {
            for (int c = 0; c < getNumCols(); c++)
            {
                // If there's an object at this location, put it in the array.
               Location loc = new Location(r, c);
               if (get(loc) != null)
                  theLocations.add(loc);
            }
         }
      
         return theLocations;
      }
   
   
   
    /**
     * Creates a string that describes this grid.
     * @return a string with descriptions of all objects in this grid (not
     * necessarily in any particular order), in the format {loc=obj, loc=obj,
     * ...}
     */
      public String toString()
      {
         String s = "{";
         for (Location loc : getOccupiedLocations())
         {
            if (s.length() > 1)
               s += ", ";
            s += loc + "=" + get(loc);
         }
         return s + "}";
      }
   }
