﻿namespace warlight2_starterbot_csharp.Map
{
    using System;
    using System.Collections.Generic;

    public class Map
    {
        /// <summary>
        /// The list of all Regions in this map.
        /// </summary>
        public LinkedList<Region> Regions { get; private set; }
        
        /// <summary>
        /// The list of all SuperRegions in this map.
        /// </summary>
        public LinkedList<SuperRegion> SuperRegions { get; private set; }

        public Map()
        {
            Regions = new LinkedList<Region>();
            SuperRegions = new LinkedList<SuperRegion>();
        }

        public Map(LinkedList<Region> regions, LinkedList<SuperRegion> superRegions)
        {
            Regions = regions;
            SuperRegions = superRegions;
        }

        /// <summary>
        /// Add a Region to the map.
        /// </summary>
        /// <param name="region">Region to be added.</param>
        public void Add(Region region)
	    {
		    foreach (var r in Regions)
            {
                if (r.Id == region.Id)
			    {
				    Console.Error.WriteLine("Region cannot be added: id already exists.");
				    return;
			    }
            }

            Regions.AddLast(region);
	    }

        /// <summary>
        /// SuperRegion to the map
        /// </summary>
        /// <param name="superRegion">SuperRegion to be added.</param>
        public void Add(SuperRegion superRegion)
	    {
            foreach (var s in SuperRegions)
            {
                if (s.Id == superRegion.Id)
                {
                    Console.Error.WriteLine("SuperRegion cannot be added: id already exists.");
                    return;
                }
            }

            SuperRegions.AddLast(superRegion);
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A new Map object exactly the same as this one.</returns>
        public Map GetMapCopy() 
        {
		    var newMap = new Map();

		    foreach (var sr in SuperRegions) //copy superRegions
		    {
			    var newSuperRegion = new SuperRegion(sr.Id, sr.ArmiesReward);
			    newMap.Add(newSuperRegion);
		    }
		    foreach (var r in Regions) //copy regions
		    {
			    var newRegion = new Region(r.Id, newMap.GetSuperRegion(r.SuperRegion.Id), r.PlayerName, r.Armies);
			    newMap.Add(newRegion);
		    }
		    foreach (var r in Regions) //add neighbors to copied regions
		    {
			    var newRegion = newMap.GetRegion(r.Id);
		        
                foreach (var neighbor in r.Neighbors)
		        {
		            newRegion.AddNeighbour(newMap.GetRegion(neighbor.Id));
		        }
		    }

		    return newMap;
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">A Region ID number.</param>
        /// <returns>The matching Region object.</returns>
        public Region GetRegion(int id)
	    {
		    foreach (var region in Regions)
	        {
	            if (region.Id == id)
	            {
	                return region;
	            }
	        }

            return null;
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">A SuperRegion ID number.</param>
        /// <returns>The matching SuperRegion object.</returns>
        public SuperRegion GetSuperRegion(int id)
	    {
		    foreach (var superRegion in SuperRegions)
            {
                if (superRegion.Id == id)
                {
                    return superRegion;
                }
            }
		    return null;
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
	    {
		    var mapstring = string.Empty;
		    
            foreach (var region in Regions)
		    {
			    mapstring += region.Id + ";" + region.PlayerName + ";" + region.Armies + " ";
		    }

		    return mapstring;
	    }	
    }
}