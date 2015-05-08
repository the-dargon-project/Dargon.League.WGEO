using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dargon.League.WGEO {
   public class WGEOFile {
      public WGEOFile() {
         models = new List<WGEOModel>();
      }

      public List<WGEOModel> models;
   }

   public class WGEOModel {
      public WGEOVertex[] vertices;
      public ushort[] indices;
      public string textureName;
   }

   public class WGEOVertex {
      public Float3 position;
      public Float2 uv;

      public WGEOVertex(Float3 position, Float2 uv) {
         this.position = position;
         this.uv = uv;
      }
   }
}
