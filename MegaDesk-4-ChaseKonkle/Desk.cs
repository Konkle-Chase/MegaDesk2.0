using System;

namespace MegaDesk_4_ChaseKonkle
{
    public class Desk
    {
        //Declarations
        public int width { get; set; }
        public int depth { get; set; }
        public int numDrawers { get; set; }
        public SurfaceMaterial surface { get; set; }

        
        //Desk object constructor.
        public Desk(int inWidth, int inDepth, int inNumDrawers, string inSurface)
        {
            width = inWidth;
            depth = inDepth;
            numDrawers = inNumDrawers;
            surface = (SurfaceMaterial)Enum.Parse(typeof(SurfaceMaterial), inSurface);
        }
    }
    //Enumerated array consisting of the cost of the surface materials. 
    public enum SurfaceMaterial { Materials, Pine = 50, Laminate = 100, Oak = 200, Rosewood = 300, Veneer = 150 };
}
