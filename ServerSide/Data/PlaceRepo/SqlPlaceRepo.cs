using System.Collections.Generic;
using System.Linq;
using Refrigerator.Models;

namespace Refrigerator.Data
{
    public class SqlPlaceRepo : IPlaceRepo
    {
        private readonly RefrigeratorContext _context;

        public SqlPlaceRepo(RefrigeratorContext context)
        {
            this._context = context;
        }

        public Dictionary<string, List<Place>> GetPlaceRefrigerator()
        {
            var placeRefrDict = new Dictionary<string,List<Place>>(); 
            var places = this._context.Places;

            foreach (var place in places)
            {
                if (!placeRefrDict.Keys.Contains(place.name))
                {
                    placeRefrDict.Add(place.name, 
                                      new List<Place> { place });
                }
                else 
                {
                    var placeLst = placeRefrDict[place.name];
                    if (placeLst.FirstOrDefault(item => item.name == place.name) == null)
                    {
                        placeLst.Add(place);
                    }
                }
            }
        
            return placeRefrDict;
        }
    }
}