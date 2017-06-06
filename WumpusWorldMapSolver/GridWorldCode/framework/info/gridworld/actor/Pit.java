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

import java.awt.Color;

/**
 * A <code>Rock</code> is an actor that does nothing. It is used for WumpusWorld
 */

public class Pit extends Actor
{
    
    
    private static final Color DEFAULT_COLOR = Color.BLACK;

    /**
     * Constructs a black rock.
     */
    public Pit()
    {
        setColor(DEFAULT_COLOR);
    }

    /**
     * Overrides the <code>act</code> method in the <code>Actor</code> class
     * to do nothing.
     */
    public void act()
    {
    }
}
