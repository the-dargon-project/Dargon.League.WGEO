using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dargon.Scene.Api;

namespace Dargon.League.WGEO {
   public class WGEOFile {
      public WGEOFile() {
         models = new List<Mesh>();
      }

      public List<Mesh> models;
   }
}
