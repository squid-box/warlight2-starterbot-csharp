namespace warlight2_starterbot_csharp.Map
{
    using System;
    using System.Collections.Generic;

    public class Map
    {
        #region Properties

        /// <summary>
        /// The list of all Regions in this map.
        /// </summary>
        public List<Region> Regions { get; private set; }
        
        /// <summary>
        /// The list of all SuperRegions in this map.
        /// </summary>
        public List<SuperRegion> SuperRegions { get; private set; }

        #endregion

        /// <summary>
        /// Creates an empty map.
        /// </summary>
        public Map()
        {
            Regions = new List<Region>();
            SuperRegions = new List<SuperRegion>();
        }

        /// <summary>
        /// Creates a populated map.
        /// </summary>
        /// <param name="regions">List of Regions.</param>
        /// <param name="superRegions">List of SuperRegions.</param>
        public Map(List<Region> regions, List<SuperRegion> superRegions)
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

            Regions.Add(region);
	    }

        /// <summary>
        /// Add a SuperRegion to the map
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

            SuperRegions.Add(superRegion);
	    }

        /// <summary>
        /// Make an exact copy of this map.
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
		            newRegion.AddNeighbor(newMap.GetRegion(neighbor.Id));
		        }
		    }

		    return newMap;
	    }

        /// <summary>
        /// Retrieve a specific Region object.
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
        /// Retrieve a specific SuperRegion object.
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
        /// Get a string representation of this Map.
        /// </summary>
        /// <returns>String with no line breaks, outlining the status of the Map.</returns>
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
