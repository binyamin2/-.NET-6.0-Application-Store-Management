using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation
{
    public static class BLFactory
    {
        public static IBl getBL(string typeOfObj = "")
        {
            switch (typeOfObj.ToLower())
            {
                case "simple":
                    return new Bl(); //class AObject : IMyInterface;
                //case "nonexistant":
                //    return new BObject(); //class BObject : IMyInterface;
                default:
                    return new Bl(); //class AObject : IMyInterface;
            }
        }
    }
}
