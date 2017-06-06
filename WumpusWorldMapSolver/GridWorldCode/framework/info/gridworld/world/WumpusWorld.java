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

   package info.gridworld.world;

   import info.gridworld.grid.Grid;
   import info.gridworld.grid.Location;
   import info.gridworld.actor.*;

   import java.util.ArrayList;

//added for sound
   import java.io.File;
   import javax.sound.sampled.AudioFormat;
   import javax.sound.sampled.AudioInputStream;
   import javax.sound.sampled.AudioSystem;
   import javax.sound.sampled.Clip;
   import javax.sound.sampled.DataLine;


/**
 * An <code>ActorWorld</code> is occupied by actors. <br />
 * This class is not tested on the AP CS A and AB exams.
 */

   public class WumpusWorld extends World<Actor>
   {    
      private static final String DEFAULT_MESSAGE = "Current Score = 0";
      private int AgentCount = 0;
   
    
    /**
     * Constructs an actor world with a default grid.
     */
      public WumpusWorld()
      {
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
      }
   
    /**
     * Constructs an actor world with a given grid.
     * @param grid the grid for this world.
     */
      public WumpusWorld(Grid<Actor> grid)
      {
         super(grid);
      }
   
      public void show()
      {
         if (getMessage() == null)
            setMessage(DEFAULT_MESSAGE);
         super.show();
      }
    
      public void sound(String file)
      {
         try
         {      
            AudioInputStream stream = AudioSystem.getAudioInputStream(new File("info\\gridworld\\actor\\"+file+".wav"));      
            AudioFormat format = stream.getFormat();      
            DataLine.Info info = new DataLine.Info(Clip.class, stream.getFormat());      
            Clip clip = (Clip) AudioSystem.getLine(info);         
            clip.open(stream);      
            clip.start();
         }
            catch (Exception e)
            {      
               e.printStackTrace();    
            }
      }
   
      public void step()
      {
         Grid<Actor> gr = getGrid();
         ArrayList<Actor> actors = new ArrayList<Actor>();
         
      	       // Look at all grid locations.
         for (int r = 0; r < gr.getNumRows(); r++)
         {
            for (int c = 0; c < gr.getNumCols(); c++)
            {
                // If there's an object at this location, put it in the array.
               Location loc = new Location(r, c);
               if (gr.get(loc) != null)
                  actors.add(gr.get(loc));
            }
         }
      	
         //for (Location loc : gr.getOccupiedLocations())
         //actors.add(gr.get(loc));
      
         for (Actor a : actors)
         {
            // only act if another actor hasn't removed a
            if (a.getGrid() == gr)
               a.act();
            if (a instanceof Agent || a instanceof RandomAgent) AgentCount=a.getAgentCount();					
         }
      	
         setMessage("Current Score = " + ((Integer)AgentCount).toString());
      //         sound("ouch");
      }
   
    /**
     * Adds an actor to this world at a given location.
     * @param loc the location at which to add the actor
     * @param occupant the actor to add
     */
      public void add(Location loc, Actor occupant)
      {
         occupant.putSelfInGrid(getGrid(), loc);
      }
   
    /**
     * Adds an occupant at a random empty location.
     * @param occupant the occupant to add
     */
      public void add(Actor occupant)
      {
         Location loc = getRandomEmptyLocation();
         if (loc != null)
            add(loc, occupant);
      }
   
    /**
     * Removes an actor from this world.
     * @param loc the location from which to remove an actor
     * @return the removed actor, or null if there was no actor at the given
     * location.
     */
      public Actor remove(Location loc)
      {
         Actor occupant = getGrid().get(loc);
         if (occupant == null)
            return null;
         occupant.removeSelfFromGrid();
         return occupant;
      }
   }