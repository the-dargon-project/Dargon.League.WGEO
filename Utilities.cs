using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dargon.League.WGEO {
   public class Float2 {
      public Float2(float x, float y) {
         this.x = x;
         this.y = y;
      }
      public Float2(Float2 rhs) {
         this.x = rhs.x;
         this.y = rhs.y;
      }

      public float x;
      public float y;

      public override string ToString() {
         return "[" + x + ", " + y + "]";
      }
   }

   public class Float3 {
      public Float3(float x, float y, float z) {
         this.x = x;
         this.y = y;
         this.z = z;
      }
      public Float3(Float3 rhs) {
         this.x = rhs.x;
         this.y = rhs.y;
         this.z = rhs.z;
      }

      public float x;
      public float y;
      public float z;

      public override string ToString() {
         return "[" + x + ", " + y + ", " + z + "]";
      }
   }

   public class Float4 {
      public Float4(float x, float y, float z, float w) {
         this.x = x;
         this.y = y;
         this.z = z;
         this.w = w;
      }
      public Float4(Float4 rhs) {
         this.x = rhs.x;
         this.y = rhs.y;
         this.z = rhs.z;
         this.w = rhs.w;
      }

      public float x;
      public float y;
      public float z;
      public float w;

      public override string ToString() {
         return "[" + x + ", " + y + ", " + z + ", " + w + "]";
      }
   }

   public static class Extensions {
      public static Float2 ReadFloat2(this BinaryReader reader) {
         return new Float2(reader.ReadSingle(), reader.ReadSingle());
      }

      public static Float3 ReadFloat3(this BinaryReader reader) {
         return new Float3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
      }

      public static Float4 ReadFloat4(this BinaryReader reader) {
         return new Float4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
      }

      public static WGEOVertex ReadWGEOVertex(this BinaryReader reader) {
         return new WGEOVertex(reader.ReadFloat3(), reader.ReadFloat2());
      }
   }
}
